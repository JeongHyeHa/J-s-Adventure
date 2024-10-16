using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameoverText;
    GameManager gameManager;

    private float timeRemaining = 120f; // 120초 (2분)

    void Start()
    {
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        gameoverText = GameObject.Find("GameoverText").GetComponent<TextMeshProUGUI>();
        gameManager = FindObjectOfType<GameManager>();

        // 초기 텍스트 설정
        timerText.text = Mathf.RoundToInt(timeRemaining).ToString();

        gameoverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timeRemaining > 0 && !gameManager.isDead)
        {
            timeRemaining -= Time.deltaTime;

            // 남은 시간이 정확히 10초 이하일 때 텍스트 색상을 빨간색으로 변경
            if (Mathf.RoundToInt(timeRemaining) <= 10)
            {
                timerText.color = Color.red;
            }

            // 초 단위로 텍스트 업데이트 (반올림된 값 표시)
            int displayTime = Mathf.RoundToInt(timeRemaining);
            if (displayTime > 0)
            {
                timerText.text = displayTime.ToString();
            }
        }
        else 
        {
            // 시간이 다 되었을 때 처리
            timeRemaining = 0;
            timerText.text = "0";
            gameManager.isDead = true;
            OnCountdownEnd();
        }
    }

    // 카운트다운이 끝났을 때 실행될 동작
    void OnCountdownEnd()
    {
        Debug.Log("Countdown has ended!");
        timerText.text = "";
        gameManager.isDead = true;
        // 추가적인 처리 가능 (예: 게임 종료, 이벤트 트리거 등)
    }
}
