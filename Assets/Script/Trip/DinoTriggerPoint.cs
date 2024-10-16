using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoTriggerPoint : MonoBehaviour
{
    public List<Rigidbody2D> enemies = new List<Rigidbody2D>();
    public float grv = 4f;   // 변경될 중력 값

    private void Start()
    {
        GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("Enemy"); // Enemy 태그로 적들 찾기
        foreach (GameObject enemy in foundEnemies)
        {
            if (enemy.name.Contains("dino1"))
            {
                enemies.Add(enemy.GetComponent<Rigidbody2D>());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 트리거에 닿았을 때만 실행
        if (other.CompareTag("Player"))
        {
            for(int i=0; i< enemies.Count; i++)
            {
                // 적들의 중력을 4로 변경
                foreach (Rigidbody2D enemy in enemies)
                {
                    enemy.gravityScale = grv;
                }
            }
        }
    }
}
