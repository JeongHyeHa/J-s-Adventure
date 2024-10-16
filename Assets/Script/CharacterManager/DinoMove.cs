using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 중력이 적용되면 이동
        if (rb.gravityScale > 0)
        {
            // 이동 속도로 한 칸씩 내려가는 동작
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }
}
