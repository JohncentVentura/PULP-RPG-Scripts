using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IDataPersistence //Singleton, or only 1 copy per scene
{   
    [Header("GameObjects")]
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private PlatformerHUD platformerHUD;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON; //Put load_globals.json file to this variable in Unity Inspector

    [Header("Dialogue Text UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueSpeakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueContinueImage;
    //[SerializeField] private Animator dialoguePortraitAnimator;
    
    [Header("Dialogue Choices UI")]
    [SerializeField] private GameObject[] choicesButtons;
    private TextMeshProUGUI[] choicesText;

    public static DialogueManager instance { get; private set;}
    private Story currentStory; //The compiled JSON file from an Ink file
    public bool isDialoguePlaying { get; private set; } 
    private bool isFirstDialogue = true;
    public bool isContinuePressedDown = false;
    private bool canDialogueContinue = false;
    private Coroutine displayTextCoroutine;
    private float typingSpeed = 0.04f;
    private const string SPEAKER_TAG = "speaker";
    private const string PORTAIT_TAG = "portrait";

    public DialogueVariablesManager dialogueVariablesManager;

    public AudioClip continueClip;

    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogWarning("DialogueManager duplicate found");
        }
        instance = this;

        dialogueVariablesManager = new DialogueVariablesManager(loadGlobalsJSON);
    }

    void Start()
    {   
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choicesButtons.Length]; //Count of choicesText is equal to choicesButtons.Length
        int index = 0;
        foreach (GameObject choice in choicesButtons)
        { //Iterate choicesText to assign its text
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    void Update()
    {   
        if (Input.GetButtonDown("Jump"))
        {   
            isContinuePressedDown = true; //When this variable is called & is true, set this false immediately
        }
        
        if (!isDialoguePlaying)
        {
            return;
        }

        //Handle continuing to the next line in the dialogue when pressing
        if (isContinuePressedDown && canDialogueContinue) 
        { 
            isContinuePressedDown = false; //Set this to false so that it doesn't also count as input for the Coroutine
            ContinueStory();
        }
        else if (isContinuePressedDown && isFirstDialogue) //Only called once, use this when an Input is called identical to the script that use the same Input to call this script
        {
            isFirstDialogue = false;
            isContinuePressedDown = false;
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {   
        platformerHUD.DeactivateMenu();
        playerControls.isInputActive = false;
        playerControls.moveInput = 0;

        currentStory = new Story(inkJSON.text); //Set the story of the DialogueManager
        isDialoguePlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariablesManager.StartListening(currentStory); //Start checking variables of the story

        dialogueSpeakerText.text = "???";
        //dialoguePortraitAnimator.Play("Male1_Normal");
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVariablesManager.StopListening(currentStory); //Stop checking variables of the story

        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        playerControls.isInputActive = true;
        platformerHUD.ActivateMenu();
        DataPersistenceManager.instance.SaveGame();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {   
            AudioManager.instance.PlaySFX(continueClip);
            
            //Stops any Coroutine that started to only set one Coroutine at a time
            if (displayTextCoroutine != null) StopCoroutine(displayTextCoroutine);

            //Set text for the current dialogue line
            displayTextCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
                                                                                         
            HandleTags(currentStory.currentTags);
        }
        else //if story can't continue
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {   
        //set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        //Hide some UI while text is typing
        dialogueContinueImage.SetActive(false);
        HideChoices();
        canDialogueContinue = false;

        bool isAddingRichTextTag = false;

        foreach (char letter in line.ToCharArray()) //display each letter one at a time
        {
            if (isContinuePressedDown) //if player skips the dialogue
            {
                isContinuePressedDown = false;
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            if (letter == '<' || isAddingRichTextTag) //if dialogue does have a RichTextTag 
            {
                isAddingRichTextTag = true;

                if (letter == '>') isAddingRichTextTag = false;
            }
            else //if dialogue doesn't have a RichTextTag 
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        
        //Show some UI after text is done typing
        dialogueContinueImage.SetActive(true);

        //Display choices if any for this dialogue line, originally called in Update()
        DisplayChoices();
        
        canDialogueContinue = true;
    }

    private void HandleTags(List<string> currentTags)
    {
        //Loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {   
            //parse the tag, this returns an array of length 2, where the 1st element is the key and the 2nd element is the value
            string[] splitTag = tag.Split(':');

            //in case of the splitTag returnin an array of length more than 2
            if (splitTag.Length != 2) Debug.LogError("Tag could not be appropriately parsed: " + tag);
        
            string tagKey = splitTag[0].Trim(); //gets the 1st element of the splitTag containing the key
            string tagValue = splitTag[1].Trim();  //gets the 2nd element of the splitTag containing the value

            //handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    dialogueSpeakerText.text = tagValue;
                    break;
                case PORTAIT_TAG:
                    //dialoguePortraitAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices; //Gets all choices from our story, if there is any
        
        //Checks if currentChoices is greater than choices UI, for example our choice button is only up to 2 but the choices of a question is 3
        if (currentChoices.Count > choicesButtons.Length) Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
    
        int index = 0;
        foreach (Choice choice in currentChoices) //Activates the choicesButtons base on the choices
        { 
            choicesButtons[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choicesButtons.Length; i++) //Deactivates the choicesButtons base on the choices
        {
            choicesButtons[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choicesButtons)
        {
            choiceButton.SetActive(false);
        }
    }

    private IEnumerator SelectFirstChoice() //Event System requires we clear it first, then wait for at least one frame before we set the current selected object
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choicesButtons[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;

        //Reference the dictionary in the dialogVariables object, assuming the variable exists
        dialogueVariablesManager.variables.TryGetValue(variableName, out variableValue);

        if (variableValue == null) 
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        
        return variableValue;
    }

    private void OnApplicationQuit()
    {   
        //dialogueVariablesManager.SaveData();
    }


    public void LoadData(GameData gameData)
    {   
        
    }

    public void SaveData(GameData gameData)
    {   
        if(dialogueVariablesManager.globalVariablesStory != null)
        {   
            //Load the current state of all our variables to the globals story
            dialogueVariablesManager.VariableToStory(dialogueVariablesManager.globalVariablesStory);
            
            //GameManager.instance.saveVariablesKey = dialogueVariablesManager.globalVariablesStory.state.ToJson()+"_"+DataPersistenceManager.instance.selectedProfileId;
        }
    }

}
