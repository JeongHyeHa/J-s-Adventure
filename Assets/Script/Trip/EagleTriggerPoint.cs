using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleTriggerPoint : MonoBehaviour
{
    public EagleMove eagle;

    private void Awake()
    {
        eagle = FindObjectOfType<EagleMove>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 트리거에 닿았을 때 독수리 이동 시작
        if (other.CompareTag("Player"))
        {
            //eagle.StartMoving(); // 독수리 이동 함수 호출
        }
    }
}
