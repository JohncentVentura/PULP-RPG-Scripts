using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour, IDataPersistence //This class handles the input of the Player, apart from inputs from Menus & Dialogues
{
    //Components
    private Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    //Animations
    public RuntimeAnimatorController runtimeAnimatorController;
    [SerializeField] private RuntimeAnimatorController playerMaleRuntimeAnimator;
    [SerializeField] private RuntimeAnimatorController playerFemaleRuntimeAnimator;

    //Global Variables
    [HideInInspector] public float moveInput; //Gets Input.GetAxisRaw("Horizontal");
    [HideInInspector] public float playerDirection = 0.1f;
   
    [Header("Movement")]
    [HideInInspector] public bool isInputActive;
    public float moveSpeed;
    public float acceleration;
    public float deceleration;
    public float velPower;
    public float frictionAmount;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    public int jumpCounter = 0;

    [Header("Dashing")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashTime; //How many seconds the player is dashing
    private bool isDashing = false;
    [SerializeField] public float dashCooldown = 0;
    [HideInInspector] public float dashFill; //For PlatformerHUD UI
    

    [Header("Ground Collision")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [HideInInspector] public bool isCollidingGround = false;

    [Header("Wall Slide")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [HideInInspector] public bool isCollidingWall;
    public bool isWallSlippery;

    [Header("Items")]
    public Transform itemHolder;
    [SerializeField] private PlayerMenu playerMenu;
    public Item equippedItem;
    [SerializeField] private GameObject items;

    [Header("AudioClips")]
    //[SerializeField] private AudioSource jumpSoundEffect;
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip itemConsume;
    public AudioClip itemESDShoes;

    [Header("Objects Collision")]
    [SerializeField] private LayerMask objectLayer;
    [HideInInspector] public bool isCollidingObject = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();

        //animator.runtimeAnimatorController = GameManager.instance.playerStatsSO.runtimeAnimatorController;
        if(GameManager.instance.playerStatsSO.gender == "Male")
        {
            animator.runtimeAnimatorController = playerMaleRuntimeAnimator;
        }
        else if(GameManager.instance.playerStatsSO.gender == "Female")
        {
            animator.runtimeAnimatorController = playerFemaleRuntimeAnimator;
        }

        animator.SetFloat("PlayerDirection", 0.1f);

        //Resets stats
        moveSpeed = 6;
        acceleration = 8;
        deceleration = 8;
        velPower = 0.9f;
        frictionAmount = 0.4f;
        jumpForce = 9.5f;
        jumpCounter = 0;
        dashSpeed = 14;
        dashTime = 0.3f;
        dashCooldown = 0.3f;
    }

    void Update()
    {   
        //Run Input
        if(isInputActive) moveInput = Input.GetAxisRaw("Horizontal");

        //Set playerDirection
        if (isInputActive && Input.GetAxisRaw("Horizontal") != 0)
        {
            playerDirection = Input.GetAxisRaw("Horizontal"); //Saves the last Input.GetAxisRaw before Input.GetAxisRaw becomes 0

            if (playerDirection > 0.0001)
            {
                playerDirection = 1;
            }
            else if (playerDirection < -0.0001)
            {
                playerDirection = -1;
            }

            animator.SetFloat("PlayerDirection", playerDirection); //So FacingDirection contains either 0.1 or -0.1
        }

        //Get playerDirection
        if (playerDirection >= 0.1) //playerDirection is right
        {
            itemHolder.localPosition = new Vector2(-0.4f, 0);
        }
        else if (playerDirection <= -0.1) //playerDirection is left
        {
            itemHolder.localPosition = new Vector2(0.4f, 0);
        }

        //Jump Input
        if (isInputActive && !isDashing)
        {
            //Jump Input Down
            if (Input.GetButtonDown("Fire1") && jumpCounter > 0)
            {   
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCounter--;
                //jumpSoundEffect.Play();
                AudioManager.instance.PlaySFX(jumpSound);
            }

            //Jump Input Released
            if (Input.GetButtonUp("Fire1") && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        //Dash Input
        if (isInputActive && Input.GetButtonDown("Fire2") && !isDashing && dashCooldown == 0)
        {   
            dashFill = 0;
            isDashing = true;
            trailRenderer.emitting = true;
            rb.gravityScale = 0f;
            Vector2 dashVector = new Vector2(playerDirection, 0);
            rb.velocity = dashVector * dashSpeed;
            AudioManager.instance.PlaySFX(dashSound);
            StartCoroutine(DashTimer());
        }

        //Dash Cooldown
        if (dashCooldown > 0.9) 
        {   
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            dashCooldown = 0;
        }

        //Dash Filler
        if(dashFill < 4.5f) 
        {   
            dashFill += Time.deltaTime;
        }
        else 
        {
            dashFill = 4.5f;
        }

        //WallSliding, checking if isCollidingGround & isCollidingWall is true
        isCollidingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isCollidingWall = Physics2D.Raycast(wallCheck.position, new Vector2(playerDirection, 0), wallCheckDistance, groundLayer);
        isCollidingObject = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, objectLayer);

        //WallSliding, if moveInput is 0 while Wall Sliding, gravityScale will apply normally 
        if (!isCollidingGround && isCollidingWall && moveInput != 0 && !isWallSlippery) //Normal Wall Slide
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        else if (!isCollidingGround && isCollidingWall && moveInput != 0 && isWallSlippery) //Slippery Wall Slide
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 1f);
        }
    
        //Animations
        if(GameManager.instance.playerStatsSO.energy <= 0)
        {
            //StartCoroutine(DeathTimer());
        }
        else if(equippedItem != null && equippedItem.itemName == "First Aid" && equippedItem.itemFill > 0.5 && GameManager.instance.inventorySO.isItemUsed)
        {   
            animator.Play("FirstAidBlendTree");
        }
        else if(equippedItem != null && equippedItem.itemName == "Energy Drink" && equippedItem.itemFill > 4.5 && GameManager.instance.inventorySO.isItemUsed)
        {
            animator.Play("EnergyDrinkBlendTree");
        }
        else if(equippedItem != null && equippedItem.itemName == "Coffee" && equippedItem.itemFill > 2.5 && GameManager.instance.inventorySO.isItemUsed)
        {
            animator.Play("CoffeeBlendTree");
        }
        else if(equippedItem != null && equippedItem.itemName == "Umbrella" && equippedItem.itemFill > 0 && GameManager.instance.inventorySO.isItemUsed)
        {
            animator.Play("GlideBlendTree");
        }
        else if (isDashing)
        {
            animator.Play("JumpBlendTree"); //Dash & Jump has the same animation
        }
        else if (moveInput == 0 && (isCollidingGround || isCollidingObject))
        {
            animator.Play("IdleBlendTree");
            DeactivateItems();
        }
        else if (moveInput != 0 && (isCollidingGround || isCollidingObject))
        {
            animator.Play("RunBlendTree");
        }
        else if (rb.velocity.y > 0 && !isCollidingGround && !isCollidingWall)
        {
            animator.Play("JumpBlendTree");
        }
        else if (rb.velocity.y < 0 && !isCollidingGround && !isCollidingWall)
        {
            animator.Play("FallBlendTree");
        }
        else if (rb.velocity.y < 0 && !isCollidingGround && isCollidingWall)
        {
            animator.Play("WallSlideBlendTree");
        }

        //Audios
        if(Input.GetButtonUp("Fire3") && equippedItem != null && equippedItem.itemName == "First Aid" && equippedItem.itemFill > 0.9 && GameManager.instance.inventorySO.isItemUsed)
        {   
            AudioManager.instance.PlaySFX(itemConsume);
        }
        else if(Input.GetButtonUp("Fire3") && equippedItem != null && equippedItem.itemName == "ESD Shoes" && equippedItem.itemFill > 5.8 && GameManager.instance.inventorySO.isItemUsed)
        {   
            AudioManager.instance.PlaySFX(itemESDShoes);
        }
        else if(Input.GetButtonUp("Fire3") && equippedItem != null && equippedItem.itemName == "Energy Drink" && equippedItem.itemFill > 4.9 && GameManager.instance.inventorySO.isItemUsed)
        {
            AudioManager.instance.PlaySFX(itemConsume);
        }
        else if(Input.GetButtonUp("Fire3") && equippedItem != null && equippedItem.itemName == "Coffee" && equippedItem.itemFill > 2.9 && GameManager.instance.inventorySO.isItemUsed)
        {
            AudioManager.instance.PlaySFX(itemConsume);
        }

        //Items
        if (isInputActive && equippedItem != null)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                equippedItem.ActivateEffect();
            }
            equippedItem.PassiveEffect();
        }

        //Stats
        //Fitness
        if(GameManager.instance.playerStatsSO.fitness != 0)
        {
            GameManager.instance.playerStatsSO.maxEnergy = GameManager.instance.playerStatsSO.fitness;
        }
        else if(GameManager.instance.playerStatsSO.fitness <= 0)
        {
            Debug.Log("Fitness is too low it can't be the maxEnergy, else the player will continuously die");
        }
        
        if(GameManager.instance.playerStatsSO.energy > GameManager.instance.playerStatsSO.maxEnergy) 
        {
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.maxEnergy;
        }

        //Logic & Creativity
        //On questions by NPCs

        //Charisma
        //When buying items
    }

    void FixedUpdate()
    {
        if (moveInput != 0) //Player is moving
        {
            //calculate the direction we want to move in and our desired velocity
            float targetSpeed = moveInput * moveSpeed;
            //calculate difference between current velocity and desired velocity
            float speedDif = targetSpeed - rb.velocity.x;
            //change acceleration rate depending on situation, multiply it by acceleration or deceleration value
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
            //applies acceleration to speed difference, the raises to a set power so acceleration increases with higher speeds
            //finally multiplies by sign to reapply or preserve direction, velPower helps acceleration feel responsive and happen smoothly
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
            //applies force force to rigidbody, multiplying by Vector2.right so that it only affects X axis
            rb.AddForce(movement * Vector2.right);
        }
        else if (moveInput == 0) //Player is idle
        {
            //We use either the friction amount (~ 0.2) or our velocity
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            //sets to movement direction
            amount *= Mathf.Sign(rb.velocity.x);
            //applies force against movement direction
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 8)
        {
            jumpCounter++;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if ((other.gameObject.layer == 6 && isCollidingGround || isCollidingWall) || (other.gameObject.layer == 8 && isCollidingObject))
        {
            if (jumpCounter <= 0)
            {
                jumpCounter = 1;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 8)
        {
            jumpCounter--;
        }
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = 2.2f;
        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, dashSpeed);
        trailRenderer.emitting = false;
        isDashing = false;
        dashCooldown = 5;
    }

    private IEnumerator DeathTimer()
    {   
        animator.Play("DeathBlendTree");
        yield return new WaitForSeconds(dashTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    private void DeactivateItems()
    {
        for(int i = 0; i < 4; i++)
        {
            items.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData gameData)
    {
        transform.position = new Vector2(gameData.playerStatsData.xPosition, gameData.playerStatsData.yPosition);

        for (int i = 0; i < GameManager.instance.itemsArray.Length; i++)
        {
            if (GameManager.instance.playerStatsSO.equippedItemName == GameManager.instance.itemsArray[i].itemName)
            {
                equippedItem = GameManager.instance.itemsArray[i];
                equippedItem.playerControls = this;
                //equippedItem.OnEquip(); //Commented sometimes since it is called again when playing the game again
                Debug.Log("PlayerControls equippedItem: " + equippedItem.itemName);

                if(playerMenu != null)
                {
                    playerMenu.SetStatsText();
                }
                
                return; //Stops looping the entire itemsArray
            }
            else
            {
                equippedItem = null;
            }
        }
    }

    public void SaveData(GameData gameData)
    {
        //gameData.playerPosition = this.transform.position;
    }
}
