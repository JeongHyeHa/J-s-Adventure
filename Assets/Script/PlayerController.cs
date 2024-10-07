using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 7f;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator playerAnim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);
        }

        // Direction Sprite
        if (Input.GetAxisRaw("Horizontal") !=0)
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // Animator
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            playerAnim.SetFloat("running", 0f);
        else
            playerAnim.SetFloat("running", 0.2f);
    }

    private void FixedUpdate()
    {
        // Move Speed
        float h = Input.GetAxis("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // Max Speed
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
    }
}
