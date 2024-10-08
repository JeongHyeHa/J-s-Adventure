using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public static StartGameManager Instance { get; private set; }
    public GameObject startCanvas;
    public Image blackScreen;
    public float fadeDuration = 0.5f;       // 페이드 아웃에 걸리는 시간
    public bool isGameStarting = false;    // 게임이 시작되었는지 여부

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (startCanvas == null)
        {
            startCanvas = GameObject.Find("StartCanvas");
        }
        if (blackScreen == null)
        {
            blackScreen = GameObject.Find("Black").GetComponent<Image>();
        }

        blackScreen.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        // 마우스 클릭이 감지되면 페이드 아웃 시작
        if (Input.GetMouseButtonDown(0) && !isGameStarting)
        {
            StartCoroutine(FadeToBlackAndStartGame());
        }
    }

    IEnumerator FadeToBlackAndStartGame()
    {

        float elapsedTime = 0f;

        // 검은 화면이 서서히 나타남 (투명 -> 불투명)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackScreen.color = new Color(0, 0, 0, alpha);  // 투명도(alpha)를 증가시킴
            yield return null;
        }

        // 페이드인  완료 후 캔버스를 비활성화하고 게임 시작
        startCanvas.SetActive(false);

        SceneManager.LoadScene("J's Adventure _ver001");
        isGameStarting = true;
    }
}
