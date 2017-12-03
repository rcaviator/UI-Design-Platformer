using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : PauseableObject
{
    #region Fields

    //Player character state machine enum
    enum PlayerState
    {
        Idle, Running, Jumping, MidAirJumping, Ledge, ClimbingUp, ClimbingDown, Floating, 
        Landing, Attacking, Hurt
    };

    //player state matchine variable
    PlayerState playerState = PlayerState.Idle;

    [SerializeField]
    GameObject basicAttack;

    bool facingLeft = false;
    float currentHorizontalSpeed = 0f;
    bool isGrounded = true;
    SpriteRenderer sRender;

    float maxFootStepTime = 0.2f;
    float footStepTimer = 0f;

    float health = Constants.PLAYER_HEALTH_AMOUNT;

    [SerializeField]
    Image healthBar;

    #endregion

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        //set references
        GameManager.Instance.Player = gameObject;
        sRender = GetComponent<SpriteRenderer>();
        rBody.freezeRotation = true;
        
        //create new inventory
        PlayerInventory = new Inventory();

        //register as do not destroy
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.Instance.Paused)
        {
            //update player movement
            PlayerMovement();

            //update player animations
            PlayerAnimations();

            //check states
            CheckStates();

            //update health bar
            healthBar.fillAmount = health / Constants.PLAYER_HEALTH_AMOUNT;
        }
    }

    /// <summary>
    /// Move the player to a starting location. Used in scene transitions
    /// </summary>
    /// <param name="location">starting location</param>
    public void SetPlayerLocation(Vector3 location)
    {
        //set location
        transform.position = location;

        //resets camera for canvas component
        transform.GetChild(2).GetComponent<Canvas>().worldCamera = Camera.main;
    }

    /// <summary>
    /// the player health
    /// </summary>
    public float PlayerHealth
    {
        get { return health; }
        set
        {
            health = value;
            if (health > 100f)
            {
                health = 100f;
            }
        }
    }

    /// <summary>
    /// player inventory
    /// </summary>
    public Inventory PlayerInventory
    { get; private set; }

    /// <summary>
    /// handles player movement
    /// </summary>
    void PlayerMovement()
    {
        //switch over player state
        switch (playerState)
        {
            //the idle state
            case PlayerState.Idle:
                //reset horizontal speed
                currentHorizontalSpeed = 0f;

                //update velocity to 0
                if (rBody.velocity.x != 0f)
                {
                    rBody.velocity = new Vector2(rBody.velocity.x * 0.85f, rBody.velocity.y);

                    if (rBody.velocity.x < .05f && rBody.velocity.x > -0.5f)
                    {
                        rBody.velocity = new Vector2(0, rBody.velocity.y);
                    }
                }
                break;

            //the running state
            case PlayerState.Running:
                //apply running motion based on direction
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0)
                {
                    facingLeft = false;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x + Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }
                else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0)
                {
                    facingLeft = true;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x - Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }

                //audio
                footStepTimer += Time.deltaTime;
                if (footStepTimer >= maxFootStepTime)
                {
                    footStepTimer = 0f;
                    AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.PlayerFootsteps);
                }
                break;

            //the jumping state
            case PlayerState.Jumping:
                //apply jump motion
                rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y + Constants.PLAYER_JUMP_FORCE);
                isGrounded = false;

                //audio
                AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.PlayerJump);
                break;

            //case PlayerState.MidAirJumping:
            //    break;
            //case PlayerState.Ledge:
            //    break;
            //case PlayerState.ClimbingUp:
            //    break;
            //case PlayerState.ClimbingDown:
            //    break;

            //the floating state
            case PlayerState.Floating:
                //apply floating motion based on direction
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0f)
                {
                    facingLeft = false;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x + Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }
                else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0f)
                {
                    facingLeft = true;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x - Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }
                
                break;

            //the landing state
            case PlayerState.Landing:
                //audio
                AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.PlayerLand);
                break;
            ////the attacking state
            //case PlayerState.Attacking:
            //    break;
            case PlayerState.Hurt:
                //apply running motion based on direction
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0)
                {
                    facingLeft = false;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x + Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }
                else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0)
                {
                    facingLeft = true;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x - Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }

                //Audio
                AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.PlayerHurt);
                break;
            default:
                break;
        }

        //left vs right facing
        if (!facingLeft)
        {
            sRender.flipX = false;
        }
        else
        {
            sRender.flipX = true;
        }

        //attack
        if (InputManager.Instance.GetButtonDown(PlayerAction.FirePrimary))
        {
            BasicAttack();
        }
    }

    /// <summary>
    /// handles player animations
    /// </summary>
    void PlayerAnimations()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerHurt"))
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        anim.Play("PlayerIdle");
                    }
                }
                else
                {
                    anim.Play("PlayerIdle");
                }
                break;

            case PlayerState.Running:
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerHurt"))
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        anim.Play("PlayerRunning");
                    }
                }
                else
                {
                    anim.Play("PlayerRunning");
                }
                break;

            case PlayerState.Jumping:
                anim.Play("PlayerJump");
                break;

            //case PlayerState.MidAirJumping:
            //    break;
            //case PlayerState.Ledge:
            //    break;
            //case PlayerState.ClimbingUp:
            //    break;
            //case PlayerState.ClimbingDown:
            //    break;

            case PlayerState.Floating:
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump") || anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerHurt"))
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        anim.Play("PlayerFloat");
                    }
                }
                else
                {
                    anim.Play("PlayerFloat");
                }
                break;

            case PlayerState.Landing:
                break;
            //case PlayerState.Attacking:
            //    break;
            case PlayerState.Hurt:
                anim.Play("PlayerHurt");
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// used for transitioning states
    /// </summary>
    void CheckStates()
    {
        //check transitions
        switch (playerState)
        {
            case PlayerState.Idle:
                //running
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0 || InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0 && isGrounded)
                {
                    playerState = PlayerState.Running;
                }
                //jumping
                else if (InputManager.Instance.GetButton(PlayerAction.Jump) && isGrounded)
                {
                    playerState = PlayerState.Jumping;
                }
                //run/slide off platform
                else if (rBody.velocity.y < -0.1f)
                {
                    isGrounded = false;
                    playerState = PlayerState.Floating;
                }
                break;

            case PlayerState.Running:
                //idle
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) == 0f)
                {
                    playerState = PlayerState.Idle;
                }
                //jumping
                else if (InputManager.Instance.GetButtonDown(PlayerAction.Jump))
                {
                    playerState = PlayerState.Jumping;
                }
                //run/slide off platform
                else if (rBody.velocity.y < -0.1f)
                {
                    isGrounded = false;
                    playerState = PlayerState.Floating;
                }
                break;

            case PlayerState.Jumping:
                //transition to floating
                playerState = PlayerState.Floating;
                break;

            //case PlayerState.MidAirJumping:
            //    break;
            //case PlayerState.Ledge:
            //    break;
            //case PlayerState.ClimbingUp:
            //    break;
            //case PlayerState.ClimbingDown:
            //    break;

            case PlayerState.Floating:
                //landing
                if (isGrounded)
                {
                    playerState = PlayerState.Landing;
                }
                break;

            case PlayerState.Landing:
                //idle
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) == 0f)
                {
                    playerState = PlayerState.Idle;
                }
                //running
                else if (InputManager.Instance.GetAxis(PlayerAction.MoveHorizontal) > 0 || InputManager.Instance.GetAxis(PlayerAction.MoveHorizontal) < 0 && isGrounded)
                {
                    playerState = PlayerState.Running;
                }
                //jumping
                else if (InputManager.Instance.GetButton(PlayerAction.Jump) && isGrounded)
                {
                    playerState = PlayerState.Jumping;
                }
                //run/slide off platform
                else if (rBody.velocity.y < -0.1f)
                {
                    isGrounded = false;
                    playerState = PlayerState.Floating;
                }
                break;

            //case PlayerState.Attacking:
            //    break;
            case PlayerState.Hurt:
                //idle
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) == 0f)
                {
                    playerState = PlayerState.Idle;
                }
                //running
                else if (InputManager.Instance.GetAxis(PlayerAction.MoveHorizontal) > 0 || InputManager.Instance.GetAxis(PlayerAction.MoveHorizontal) < 0 && isGrounded)
                {
                    playerState = PlayerState.Running;
                }
                //jumping
                else if (InputManager.Instance.GetButton(PlayerAction.Jump) && isGrounded)
                {
                    playerState = PlayerState.Jumping;
                }
                //run/slide off platform
                else if (rBody.velocity.y < -0.1f)
                {
                    isGrounded = false;
                    playerState = PlayerState.Floating;
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Player isGrounded
    /// </summary>
    public bool IsGrounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    /// <summary>
    /// BasicAttack is used to fire a basic attack 
    /// </summary>
    private void BasicAttack()
    {
        GameObject attack = Instantiate(basicAttack, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        if (facingLeft)
        {
            attack.GetComponent<PlayerBasicAttack>().InitializePlayerBasicProjectile(new Vector2(-5f, 0f), Constants.PLAYER_BASIC_ATTACK_BULLET_LIFETIME);
        }
        else
        {
            attack.GetComponent<PlayerBasicAttack>().InitializePlayerBasicProjectile(new Vector2(5f, 0f), Constants.PLAYER_BASIC_ATTACK_BULLET_LIFETIME);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            playerState = PlayerState.Hurt;
        }
    }
}