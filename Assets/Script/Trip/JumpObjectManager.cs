using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObjectManager : MonoBehaviour
{
    public List<GameObject> squareObjects;  // 8개의 사각형 오브젝트 리스트
    public GameObject triggerPoint;         // 플레이어가 밟을 트리거 지점
    public Rigidbody2D rigid;
    public Transform player;

    void Start()
    {
        triggerPoint = GameObject.Find("JumpTrigger");
        rigid = GameObject.Find("Player").GetComponent< Rigidbody2D>();

        // 오브젝트들을 비활성화 상태로 시작
        foreach (GameObject obj in squareObjects)
        {

            obj.SetActive(false);
        }
    }

    void Update()
    {
        // 플레이어의 위치에서 위로 Raycast를 발사
        Debug.DrawRay(player.position, Vector2.down * 1.5f, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, 1.5f, LayerMask.GetMask("Trigger"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "TriggerPoint") 
            {
                Debug.Log("플레이어가 트리거 지점에 도달했습니다.");
                // 여기서 추가 로직을 수행 (예: 오브젝트 활성화)
            }
        }
    }
}
