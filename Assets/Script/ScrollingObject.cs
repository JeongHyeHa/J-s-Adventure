using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;   // 스크롤 이동 속도

     void Update()
    {
        // 초당 speed의 속도로 오른쪽으로 평행 이동
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
