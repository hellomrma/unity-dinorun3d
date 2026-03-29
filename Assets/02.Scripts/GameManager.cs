using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 전체 상태를 관리하는 싱글톤 컴포넌트.
/// 게임 시작/종료 흐름, UI(타이틀 패널, 진행도 바) 제어,
/// 그리고 다른 스크립트에서 GameManager.instance로 접근하는 전역 진입점 역할을 합니다.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 싱글톤 인스턴스. 씬 어디서든 GameManager.instance로 접근합니다.
    /// Awake()에서 중복 인스턴스가 감지되면 현재 오브젝트를 파괴합니다.
    /// </summary>
    public static GameManager instance;

    /// <summary>게임이 시작되었는지 나타내는 플래그. DinoController 등에서 이동 허용 여부를 판단합니다.</summary>
    public bool isGameStart;

    /// <summary>게임 시작 전 표시되는 타이틀 패널 UI</summary>
    public GameObject titlePanel;

    /// <summary>목표 지점까지의 진행도를 표시하는 슬라이더 UI</summary>
    public Slider progressBar;

    /// <summary>
    /// 싱글톤 초기화. 이미 인스턴스가 존재하면 현재 오브젝트를 파괴하고,
    /// 없으면 자신을 인스턴스로 등록합니다.
    /// </summary>
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

    /// <summary>게임 시작 시 Time.timeScale을 0으로 설정하여 타이틀 화면에서 일시 정지 상태를 유지합니다.</summary>
    public void Start()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 타이틀 패널의 시작 버튼에서 호출됩니다.
    /// isGameStart 플래그를 설정하고, 타이틀 패널을 숨긴 뒤 Time.timeScale을 복원하여 게임을 시작합니다.
    /// </summary>
    public void GameStart()
    {
        // 게임 시작 시 필요한 초기화 작업을 여기에 추가할 수 있습니다.
        // 예: UI 초기화, 배경 음악 재생 등
        isGameStart = true; // 게임 시작 플래그 설정
        titlePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 공룡의 현재 위치와 골 지점 거리를 기반으로 progressBar 값을 갱신합니다.
    /// (미구현 — 향후 DinoController 또는 MapManager와 연동하여 채워야 합니다.)
    /// </summary>
    public void SetDistanceProgressBar()
    {

    }

}
