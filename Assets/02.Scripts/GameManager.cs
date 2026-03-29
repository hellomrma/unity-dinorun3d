using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 인스턴스

    public bool isGameStart;

    public GameObject titlePanel;

    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 오브젝트를 파괴
        }
        else
        {
            instance = this; // 싱글톤 인스턴스 할당
        }
    }
    
    public void GameStart()
    {
        // 게임 시작 시 필요한 초기화 작업을 여기에 추가할 수 있습니다.
        // 예: UI 초기화, 배경 음악 재생 등
        isGameStart = true; // 게임 시작 플래그 설정
        titlePanel.SetActive(false);
    }
}
