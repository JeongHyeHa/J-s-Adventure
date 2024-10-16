using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EagleMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRender;
    public int nextMove = -1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        Invoke("Think", 1.7f);
    }

    void FixedUpdate()
    {
        //Move
        //rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        transform.Translate(Vector2.right * 3f * nextMove * Time.deltaTime);
    }

    void Think()
    {
        nextMove *=-1;
        spriteRender.flipX = nextMove == 1;
        Invoke("Think", 1.5f);
    }
}
