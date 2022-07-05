using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controls;
    private Animator anim;

    [SerializeField] private float baseSpeed = 8;
    float modifiedSpeed;
    float speed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.75f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    Vector3 velocity;
    public static Vector3 playerPosition;

    [SerializeField] private float dashTime = 1.5f;
    [SerializeField] private float dashMultiplier = 3f;
    float dashSpeed;
    float time = 0.0f;
    bool canDash = true;
    bool dashing = false;
    bool dead;

    private void Awake()
    {
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
        PlayerHealth.onPlayerHit += OnPlayerHit;
    }

    void Start()
    {
        controls = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        speed = baseSpeed;
        modifiedSpeed = speed;
    }

    void Update()
    {
        if (!UIController.playerLocked && !dead)
        {
            if (dashing == true)
            {
                speed = modifiedSpeed * dashMultiplier;
                if (time >= 0.1f)
                { 
                    dashing = false;
                }
            }
            else
            {
                speed = modifiedSpeed;
            }
            if (canDash == false)
            {
                time += Time.deltaTime;
                if (time >= dashTime && isGrounded == true)
                {
                    canDash = true;
                    time = 0.0f;
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                LevelController.instance.ReloadLevel();
            }

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (Input.GetButtonDown("Dash") && canDash)
            {
                AudioManager.instance.Play("Dash");
                canDash = false;
                dashing = true;

            }

            Vector3 moveLocalTransform = transform.right * x + transform.forward * z;

            controls.Move(moveLocalTransform * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controls.Move(velocity * Time.deltaTime);

            playerPosition = transform.position;


            anim.SetBool("isWalking", x != 0 || z != 0);
        }
    }

    void OnPlayerHit(float health)
    {
        modifiedSpeed = baseSpeed * (Mathf.Pow(1.02467f, -(health - 3.7511f)) + 0.904256f);
    }

    void OnPlayerDeath()
    {
        dead = true;
        modifiedSpeed /= 10;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
