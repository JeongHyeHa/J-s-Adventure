using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 7f;
    public float jumpPower = 20f;

    GameManager gameManager;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator playerAnim;
    public AudioSource audioSource;

    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip moneySound;
    public AudioClip getOffSound;

    bool isGrounded;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            playerAnim.SetBool("jumping", true);
            audioSource.PlayOneShot(jumpSound);
            isGrounded = false;
        }

        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);
        }

        // Direction Sprite

        float move = Input.GetAxisRaw("Horizontal");
        if (move != 0)
            spriteRenderer.flipX = move == -1;

        // Animator
        playerAnim.SetFloat("running", Mathf.Abs(move));
    }

    private void FixedUpdate()
    {
        // Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // Max Speed
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed)
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
    
        // mapGround
        Debug.DrawRay(rigid.position, Vector3.down, Color.yellow);
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground", "BearGround"));
        if (rayHit.collider != null)
        {
            if(rayHit.distance < 1.3f)
            {
                isGrounded = true;
                playerAnim.SetBool("jumping", false);
                playerAnim.SetBool("falling", false);
            }
        }
        else
        {
            isGrounded = false;
            if (rigid.velocity.y < 0)
                playerAnim.SetBool("falling", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            //Atack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                EnemyDie enemyDie = collision.transform.GetComponent<EnemyDie>();   
                enemyDie.OnDamaged();

                //Reaction Force
                rigid.AddForce(Vector2.up*10, ForceMode2D.Impulse);

                //Point
                bool isFrog = collision.gameObject.name.Contains("Frog");
                bool isEagle = collision.gameObject.name.Contains("Eagle");
                bool isBear = collision.gameObject.name.Contains("Bear");

                if (isFrog || isEagle) 
                    gameManager.gameScore += 3;
                else if (isBear)
                    gameManager.gameScore += 10;
                else
                    gameManager.gameScore += 1;
                gameManager.UpdateScoreUI();
            }
            else  //Damaged
            {
                //gameManager.LoseLife(collision.transform.position);
                if (gameManager.playerLives > 1)
                {
                    gameManager.LoseLifeWithPosition(collision.transform.position);  
                }
                else
                {
                    gameManager.LoseLife();  
                }
            }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Rewards")
        {
            //Point
            bool isCherry = collision.gameObject.name.Contains("cherry");
            bool isgem = collision.gameObject.name.Contains("gem");

            if (isCherry)
            {
                gameManager.gameScore += 1;
                gameManager.UpdateScoreUI();
            }
            else if (isgem)
            {
                gameManager.gameScore += 10;
                gameManager.UpdateScoreUI();
            }

            //Sound
            audioSource.PlayOneShot(moneySound);
            
            //Animation
            Animator itemAnim = collision.GetComponent<Animator>();
            itemAnim.SetTrigger("Get");

            //Destroy
            Destroy(collision.gameObject, 0.1f);
        }

        if (collision.gameObject.name.Contains("DeathY"))
        {
            audioSource.PlayOneShot(getOffSound);
            gameManager.isDead = true;
            gameManager.LoseLife();
            rigid.gravityScale = 6;
        }
    }
}
