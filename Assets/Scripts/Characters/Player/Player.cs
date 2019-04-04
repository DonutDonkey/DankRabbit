using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float health = 3.0f;

    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float maxSpeed = 5.0f;

    [SerializeField]
    private float force = 350f;

    [SerializeField]
    private Animator animator = null;

    private PlayerMovement movement;

    private Rigidbody2D rigidBody;

    private float velocityX;

    private bool stateRunning = false;

    private bool stateIdle = false;

    private bool stateJumping = false;


    public AudioClip hurt;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player created");
        movement  = GetComponent<PlayerMovement>();
        rigidBody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        UpdatePlayerDirection(movement.GetDirection());
        CheckMovementState();
    }

    private void UpdatePlayerDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                velocityX = -speed;
                transform.eulerAngles = new Vector3(0, 180, 0);
                stateIdle = false;  return;
            case Direction.Right:
                velocityX = speed;
                transform.eulerAngles = new Vector3(0, 0, 0);
                stateIdle = false; return;
            case Direction.Idle:
                velocityX = 0.0f;
                stateIdle = true; stateRunning = false; return;
        }
    }

    private void CheckMovementState()
    {
        if (stateIdle)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
        }

        if (stateRunning)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("isJumping", false);
        }

        if(stateJumping)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }
        else
        {
            rigidBody.AddForce (new Vector2(velocityX, rigidBody.velocity.y));
        }

        if (!stateJumping && !stateIdle)
        {
            stateRunning = true;
        }

    }

    private void Jump()
    {
        if(Input.GetKeyDown(CfgVariables.GetMovementJumpKey()) && !stateJumping) {
            stateJumping = true; stateRunning = false;

            rigidBody.AddForce(new Vector2(rigidBody.velocity.x, force));
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Grounded();
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("IsHIT"); Hit(); 
        }

        if(other.gameObject.CompareTag("EnemyKill"))
        {
            BounceOffEnemy();
        }
    }

    private void Grounded()
    {
        stateJumping = false;

        rigidBody.velocity = Vector2.zero;
    }

    private void Hit()
    {
        health -= 1.0f;

        AudioSource.PlayClipAtPoint(hurt, transform.position);

        Debug.Log(transform.eulerAngles.y);

        if (transform.eulerAngles.y == 0)
        {
            rigidBody.AddForce(transform.right * -force);
            rigidBody.AddForce(transform.up * force);
        }

        if (transform.eulerAngles.y != 0)
        {
            rigidBody.AddForce(transform.right * -force);
            rigidBody.AddForce(transform.up * force);
        }

        CheckIfDead();
    }

    private void BounceOffEnemy()
    {
        rigidBody.AddForce(new Vector2(0.0f, force));
    }

    public void CheckIfDead()
    {
        if (health > 0.0f)
            return;
        else
            Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    public float getPosX()
    {
        return this.gameObject.transform.position.x;
    }
}
