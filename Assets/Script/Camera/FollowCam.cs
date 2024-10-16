using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;    // 카메라가 따라다닐 대상
    public float smooth = 0.1f; // 카메라가 움직이는 속도
    public Vector3 adjustCamPos;    // 카메라 위치 조정
    public Vector2 minCamLimit; // 카메라 경계 설정
    public Vector2 maxCamLimit;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        //adjustCamPos = new Vector3(0, 0.3f, 0);
        //minCamLimit = new Vector2(2.594f, -8.5f);
        //maxCamLimit = new Vector2(183.6f, 1.92f);
    }

    void Update()
    {
        if (target == null) return;

        // 카메라의 대상 위치 간의 보간
        Vector3 pos = Vector3.Lerp(transform.position, target.position, smooth);

        // 대상과 한계 위치에 따른 카메라 위치
        transform.position = new Vector3(
            Mathf.Clamp(pos.x, minCamLimit.x, maxCamLimit.x) + adjustCamPos.x,
            Mathf.Clamp(pos.y, minCamLimit.y, maxCamLimit.y) + adjustCamPos.y,
            -10f + adjustCamPos.z);
    }
}
