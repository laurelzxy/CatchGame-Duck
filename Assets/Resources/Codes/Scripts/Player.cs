using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;

    public RandomFood randomFood;

    [SerializeField] private Animator animator;

    [SerializeField] private AudioClip collectSound; // MOD: som ao coletar
    private AudioSource audioSource;                 // MOD: referência ao AudioSource


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>(); // MOD

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
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        }


        if (collision.CompareTag("Trash"))
        {
            DepleteLife();
        }

    }



    public void OnDeathAnimationEnd()
    {
        // Aqui ele desaparece de vez
        gameObject.SetActive(false);
    }

    private void Death()
    {


        Debug.Log("Morreu!");
        //randomFood.StopCoroutine(randomFood.StartGeneratingFood());
        randomFood.StopFoodGeneration(); 
        animator.SetTrigger("Death");
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
