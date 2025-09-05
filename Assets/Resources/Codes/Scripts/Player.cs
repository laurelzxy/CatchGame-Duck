using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;

    public RandomFood randomFood;

    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        CheckAlive();
    }

    void FixedUpdate()
    {
        if (isAlive) 
        {
            HandleMovementFlip();

            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (moveInput != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            GameManager.lives += 20;
            if (GameManager.lives >= 100)
            {
                GameManager.lives = 100;
            }
        }

        if (collision.CompareTag("Trash"))
        {
            DepleteLife();
        }
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        Debug.Log("Morreu!");
        randomFood.StopCoroutine(randomFood.StartGeneratingFood());
    }

    public void DepleteLife()
    {
        if (isAlive)
        {
            GameManager.lives -= 20;
            animator.SetTrigger("Damage");
        }    
    }

    private bool isAlive = true;
    private void CheckAlive()
    {
        if (GameManager.lives <= 0 && isAlive)
        {
            isAlive = false;
            Death();
            
        }
    }

    private bool isFacingRight = false;
    private void HandleMovementFlip()
    {
        if (moveInput >= 0 && !isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
        }
        else if (moveInput < 0 && isFacingRight)
        {
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isFacingRight = false;
            }
        }
    }
}
