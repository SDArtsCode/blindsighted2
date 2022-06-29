using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controls;
    private Animator anim;

    [SerializeField] private float speed = 8;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.75f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    Vector3 velocity;
    public static Vector3 playerPosition;

    [SerializeField] private float dashTime = 1.5f;
    [SerializeField] private float dashSpeed = 3f;
    float time = 0.0f;
    bool canDash = true;
    bool canJump = true;
    bool dashing = false;
    public static bool locked;

    private void Awake()
    {
        PlayerHealth.onPlayerDeath += OnPlayerDeath;
        PlayerHealth.onPlayerHit += OnPlayerHit;
    }

    void Start()
    {
        controls = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!locked)
        {
            if (dashing == true)
            {
                if (time >= 0.1f)
                {
                    speed /= dashSpeed;
                    dashing = false;
                }
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

            if (Input.GetKeyDown(KeyCode.Q))
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

            if (Input.GetButtonDown("Jump") && canJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (Input.GetButtonDown("Dash") && canDash)
            {
                AudioManager.instance.Play("Dash");
                speed *= dashSpeed;
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

    void OnPlayerHit()
    {
        speed /= 2.5f;
    }

    void OnPlayerDeath()
    {
        speed /= 3;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
