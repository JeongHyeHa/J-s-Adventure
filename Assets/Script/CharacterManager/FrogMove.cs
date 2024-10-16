using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMove : MonoBehaviour
{
    public Rigidbody2D rigid;
    public GameObject player;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float detectionRange = 5f;      // 플레이어를 감지하는 거리
    public float jumpForce = 9f;           // 점프 힘
    public float moveSpeed = 4f;           // 이동 속도

    private bool isGrounded = true;
    private bool isJumping = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);   // 플레이어와의 거리

            // 플레이어가 감지 범위 내에 있을 때 (점프 시작)
            if (distance <= detectionRange && isGrounded && !isJumping)
            {
                // 플레이어 쪽으로 방향 전환
                spriteRenderer.flipX = !(player.transform.position.x < transform.position.x);
                StartCoroutine(Jump());
            }
        }

        // 착지 여부를 지속적으로 체크
        CheckGrounded();
    }

    // 플레이어를 향해 점프하는 함수
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.3f);
        isGrounded = false;
        isJumping = true;
        animator.SetBool("jumping", true);

        // 플레이어 방향으로 점프
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rigid.velocity = new Vector2(direction.x * moveSpeed, jumpForce);
    }

    // 착지 여부를 체크하는 함수
    void CheckGrounded()
    {
        Debug.DrawRay(rigid.position, Vector3.down, Color.yellow);
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null)
        {
            if (rayHit.distance < 1.3f)
            {
                isGrounded = true;
                isJumping = false;
                //animator.SetBool("jumping", false);
                animator.SetBool("falling", false);

                // 착지 후 1초 대기 후 다시 점프 가능
                StartCoroutine(WaitForNextJump());
            }
        }
        else
        {
            isGrounded = false;
            if (rigid.velocity.y <= 0)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
                
        }
    }

    // 착지 후 1초 대기하는 함수
    IEnumerator WaitForNextJump()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기
    }
}
