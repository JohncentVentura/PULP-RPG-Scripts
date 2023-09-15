using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("First Selected Button")]
    [SerializeField] public Button firstSelected;
    //private bool isJoystickInputted = false;

    protected virtual void OnEnable() //virtual means other scripts can override this
    {   
        SetFirstSelectedButton(firstSelected);
    }

    public void SetFirstSelectedButton(Button firstSelectedButton)
    {     
        //firstSelectedButton.Select();

        ///*
        if(GameManager.instance.playerStatsSO.gamepadCount == 0) //Player is using Keyboard & Mouse
        {  
            Debug.Log("A joystick is not Connected "+GameManager.instance.playerStatsSO.gamepadCount);
        }
        if(GameManager.instance.playerStatsSO.gamepadCount == 1) //Player is using Gamepad
        {   
            firstSelectedButton.Select();
            Debug.Log("A joystick is Connected "+GameManager.instance.playerStatsSO.gamepadCount); 
        }
        //*/
    }

    private void Update() 
    {   
        /*
        if(Input.GetButtonDown("JoystickTrigger"))
        {
            GameManager.instance.playerStatsSO.gamepadCount = 1;
            SetFirstSelectedButton(firstSelected);
            Debug.Log("Update JoystickTrigger");
        }
        */
    }
}