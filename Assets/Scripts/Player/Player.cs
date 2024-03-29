﻿using System.Collections;
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

    //attack object
    [SerializeField]
    GameObject basicAttack;

    //other variables
    bool facingLeft = false;
    float currentHorizontalSpeed = 0f;
    bool isGrounded = true;
    SpriteRenderer sRender;

    //footstep timers
    float maxFootStepTime = 0.2f;
    float footStepTimer = 0f;

    float health = Constants.PLAYER_HEALTH_AMOUNT;

    //health bar variables
    [SerializeField]
    Image healthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    //auto firing controll
    float maxAutoFireTimer = 0.15f;
    float autoFireTimer = 0f;
    bool canShoot = false;
    //bool buttonPressedShoot = false;

    #endregion

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        //set references
        GameManager.Instance.Player = gameObject;
        sRender = GetComponent<SpriteRenderer>();
        rBody.freezeRotation = true;
        normalHealthBar = healthBar.sprite;
        damagedHealthBar = Resources.Load<Sprite>("Graphics/Environment/HealthBarDamagedSprite");
        
        //create new inventory
        PlayerInventory = new Inventory();

        //register as do not destroy
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.Instance.Paused && !PausePlayer)
        {
            //update player movement
            PlayerMovement();

            //update player animations
            PlayerAnimations();

            //check states
            CheckStates();

            //update health bar
            healthBar.fillAmount = health / Constants.PLAYER_HEALTH_AMOUNT;

            //flash health bar if damaged
            if (flashHealthBar)
            {
                healthBarFlash += Time.deltaTime;

                if (healthBarFlash <= maxHealthBarFlash)
                {
                    //healthBar.GetComponent<Image>().color = Color.red;
                    healthBar.sprite = damagedHealthBar;
                }
                else
                {
                    //healthBar.GetComponent<Image>().color = Color.white;
                    healthBar.sprite = normalHealthBar;
                    healthBarFlash = 0f;
                    flashHealthBar = false;
                }
            }

            //change scenes if dead
            if (health <= 0f)
            {
                MySceneManager.Instance.ChangeScene(Scenes.Defeat);
            }
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

    public bool PausePlayer
    { get; set; }


    public bool InputPlayerShoot
    { get; set; }

    public bool MobileMoveLeft
    { get; set; }

    public bool MobileMoveRight
    { get; set; }

    public bool MobileJump
    { get; set; }

    public bool MobileUsePortal
    { get; set; }

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
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0 || MobileMoveRight)
                {
                    facingLeft = false;
                    if (rBody.velocity.x < 0f)
                    {
                        rBody.velocity = new Vector2(0f, rBody.velocity.y);
                    }
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x + Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }
                else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0 || MobileMoveLeft)
                {
                    facingLeft = true;
                    if (rBody.velocity.x > 0f)
                    {
                        rBody.velocity = new Vector2(0f, rBody.velocity.y);
                    }
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
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0f || MobileMoveRight)
                {
                    facingLeft = false;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x + Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }
                else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0f || MobileMoveLeft)
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
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0 || MobileMoveRight)
                {
                    facingLeft = false;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x + Constants.PLAYER_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.PLAYER_MAX_HORIZONTAL_SPEED, Constants.PLAYER_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }
                else if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0 || MobileMoveLeft)
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

        if (autoFireTimer < maxAutoFireTimer)
        {
            autoFireTimer += Time.deltaTime;
        }
        else
        {
            canShoot = true;
        }

        //attack
        //InputManager.Instance.GetButton(PlayerAction.FirePrimary)
        if (InputPlayerShoot && canShoot)
        {
            BasicAttack();
            canShoot = false;
            autoFireTimer = 0f;
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
                if ((InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0 || MobileMoveRight) || (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0 || MobileMoveLeft) && isGrounded)
                {
                    playerState = PlayerState.Running;
                }
                //jumping
                else if ((InputManager.Instance.GetButton(PlayerAction.Jump) || MobileJump) && isGrounded)
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
                if ((InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) == 0f) && (!MobileMoveLeft && !MobileMoveRight))
                {
                    playerState = PlayerState.Idle;
                }
                //jumping
                else if (InputManager.Instance.GetButtonDown(PlayerAction.Jump) || MobileJump)
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
                if ((InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) == 0f) || (!MobileMoveLeft && !MobileMoveRight))
                {
                    playerState = PlayerState.Idle;
                }
                //running
                else if ((InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0 || MobileMoveRight) || (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0 || MobileMoveLeft) && isGrounded)
                {
                    playerState = PlayerState.Running;
                }
                //jumping
                else if ((InputManager.Instance.GetButton(PlayerAction.Jump) || MobileJump) && isGrounded)
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
                if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) == 0f || (!MobileMoveLeft && !MobileMoveRight))
                {
                    playerState = PlayerState.Idle;
                }
                //running
                else if ((InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) > 0 || MobileMoveRight) || (InputManager.Instance.GetAxisRaw(PlayerAction.MoveHorizontal) < 0 || MobileMoveLeft) && isGrounded)
                {
                    playerState = PlayerState.Running;
                }
                //jumping
                else if ((InputManager.Instance.GetButton(PlayerAction.Jump) || MobileJump) && isGrounded)
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
            flashHealthBar = true;
        }
    }
}