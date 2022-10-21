using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public AudioSource bGM;
    public Canvas canva;
    public GameObject transition;

    public float maxJumpHeight;
    public float minJumpHeight;
    public float timeToJumpApex;
    public float amountOfExtraJump;
    float extraJumpAmountAvailable;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float dashWaitTime;
    float moveSpeed = 8;
    
    public int life;
    [HideInInspector]
    public bool immune;

    public float wallSlideSpeedMax;
    public float wallStickTime;
    float timeToWallUnstick;

    bool wallSliding;
    int wallDirX;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    [HideInInspector]
    public Vector3 velocity;
    float velocityXSmoothing;

    bool canDash = false;
    public float delayDashTime;
    float resetDashTime;
    float timeToBeAbleToDash = 0;
    float targetDashVelocityX;

    public GameObject immuneEffect;

    float faceDir;

    float targetVelocityX;

    Vector2 directionalInput;

    bool invokingImmune;

    Controller2D controller;

    bool flipY;
    bool instantiateGameOver;

    public Animator playerAnim;
    [HideInInspector]
    public bool walking;
    bool jumping;
    [HideInInspector]
    public bool dashing;
    [HideInInspector]
    public bool died;
    
    void Start()
    {
        faceDir = 1;
        immune = false;
        invokingImmune = false;
        flipY = false;
        instantiateGameOver = false;
        died = false;
        dashing = false;
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        immuneEffect.SetActive(false);
    }

    void Update()
    {
        if (!Pausing.gameIsPaused)
        {
            CalculateVelocity();
            HandleWallSliding();
            CheckDashing();
        }

        if (immune)
        {
            if (!invokingImmune)
            {
                if (life != 0)
                {
                    immuneEffect.SetActive(true);
                }
                invokingImmune = true;
                Invoke("ResetImmune", 2);
            }
        }

        if (directionalInput.x == 1)
        {
            faceDir = 1;
        }
        else if (directionalInput.x == -1)
        {
            faceDir = -1;
        }
        
        if ((velocity.y < 0 || velocity.y > 0) && !controller.collisions.below && !wallSliding)
        {
            jumping = true;
        }

        if (directionalInput.x == 0)
        {
            walking = false;
        }
        else if (directionalInput.x != 0)
        {
            walking = true;
        }

        if (controller.collisions.below)
        {
            extraJumpAmountAvailable = amountOfExtraJump;
            jumping = false;
        }

        if (!died)
            controller.Move(velocity * Time.deltaTime, directionalInput);
        else
        {
            if (!flipY)
            {
                flipY = true;
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            }
            jumping = true;

            transform.Translate(new Vector3(0, -9, 0) * Time.deltaTime);
        }

        if (transform.position.y < -13.5)
            if (!instantiateGameOver)
            {
                instantiateGameOver = true;
                StartCoroutine(GameEnd("GameOverScene"));
            }
            

        HandleAnimation();

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

    }

    IEnumerator GameEnd (string sceneToGo)
    {
        bGM.GetComponent<Animator>().SetTrigger("FadeOut");
        Instantiate(transition, canva.transform.position, Quaternion.identity, canva.transform);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToGo);
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnjumpInputDown()
    {
        if (wallSliding)
        {
            jumping = false;
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
                jumping = true;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
                directionalInput.x = -wallDirX;
                jumping = true;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
                jumping = true;
            }
        }
        else if (controller.collisions.below)
        {
            if (extraJumpAmountAvailable > 0)
            {
                extraJumpAmountAvailable -= 1;
                velocity.y = maxJumpVelocity;
                jumping = true;
            }
        }
        else if (!(controller.collisions.below))
        {
            if (extraJumpAmountAvailable > 0)
            {
                extraJumpAmountAvailable -= 1;
                velocity.y = maxJumpVelocity;
                jumping = true;
            }
        }
    }

    public void OnjumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    void CalculateVelocity ()
    {
        targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

    void HandleWallSliding ()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }
            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }
    
    void CheckDashing ()
    {
        if (timeToBeAbleToDash > 0)
        {
            canDash = false;
            timeToBeAbleToDash -= Time.deltaTime;
        }
        else
        {
            canDash = true;
        }
    }

    public void Dashing ()
    {
        if (!wallSliding)
        {
            if (canDash)
            {
                resetDashTime = 0.1f;

                if (jumping)
                {
                    resetDashTime = 0.3f;
                }

                canDash = false;
                dashing = true;
                timeToBeAbleToDash = delayDashTime;
                targetDashVelocityX = faceDir * moveSpeed * 3;
                velocity.x = targetDashVelocityX;
                velocity.y = -0.001f;
                Invoke("ResetDashing", resetDashTime);
            }
        }
            
    }
    
    void ResetDashing ()
    {
        dashing = false;
    }

    public void ResetImmune()
    {
        immune = false;
        invokingImmune = false;
        immuneEffect.SetActive(false);
    }

    void HandleAnimation ()
    {
        playerAnim.SetBool("Walking", walking);
        playerAnim.SetBool("Jumping", jumping);
        playerAnim.SetBool("WallSliding", wallSliding);
        playerAnim.SetBool("Dashing", dashing);
    }
}
