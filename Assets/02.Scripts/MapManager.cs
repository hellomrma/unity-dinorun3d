using UnityEngine;

/// <summary>
/// StageScriptableObject를 기반으로 맵 세그먼트를 Z축 방향으로 이어 붙여 게임 스테이지를 생성하는 컴포넌트.
/// PlayerPrefs에 저장된 스테이지 번호를 읽어 해당 스테이지의 맵 구성을 로드합니다.
/// </summary>
public class MapManager : MonoBehaviour
{

    public static MapManager instance;

    /// <summary>태그 "Goal"로 찾은 골 지점 오브젝트. 진행도 계산에 사용됩니다.</summary>
    public GameObject goalObject;

    /// <summary>스테이지별 맵 구성을 담은 ScriptableObject 배열. 인덱스는 스테이지 번호에 대응합니다.</summary>
    public StageScriptableObject[] stages;

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

    /// <summary>PlayerPrefs에서 현재 스테이지 번호를 반환합니다. 저장된 값이 없으면 1을 기본값으로 반환합니다.</summary>
    public int GetStage()
    {
        return PlayerPrefs.GetInt("Stage", 1);
    }

    /// <summary>게임 시작 시 현재 스테이지에 맞는 맵을 생성하고 골 지점을 탐색합니다.</summary>
    void Start()
    {
        CreateStage();
        goalObject = GameObject.FindWithTag("Goal");
        GetGoalDistance();
    }

    /// <summary>
    /// PlayerPrefs의 스테이지 번호를 읽어 해당 StageScriptableObject를 선택한 뒤 CreateMap()을 호출합니다.
    /// 스테이지 인덱스가 stages 배열 범위를 벗어나지 않도록 모듈로 연산을 적용합니다.
    /// </summary>
    private void CreateStage()
    {
        int currentStageIndex = GetStage() % stages.Length;
        StageScriptableObject stage = stages[currentStageIndex];
        CreateMap(stage.maps);
    }

    /// <summary>
    /// stageMaps 배열에 정의된 맵 프리팹을 Z축 방향으로 순서대로 이어 붙입니다.
    /// 각 맵의 Z 길이(GetMapSize())를 기준으로 다음 맵의 배치 위치를 계산합니다.
    /// 생성된 맵들은 이 컴포넌트의 자식으로 정리됩니다.
    /// </summary>
    /// <param name="stageMaps">배치할 맵 프리팹 배열</param>
    private void CreateMap(Map[] stageMaps)
    {
        Vector3 mapPosition = Vector3.zero;

        for (int i = 0; i < stageMaps.Length; i++)
        {
            Map selectedMap = stageMaps[i];

            if (i > 0)
            {
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2f;
            }
            Map nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity, transform);
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2f;
        }
    }

    /// <summary>
    /// 골 지점 오브젝트의 Z축 위치를 반환합니다.
    /// 진행도(Progress Bar) 계산 시 전체 거리 기준값으로 활용됩니다.
    /// </summary>
    public float GetGoalDistance()
    {
        return goalObject.transform.position.z;  // goalObject의 Z축 위치를 반환하여 목표 지점까지의 거리를 계산할 때 사용
    }
}
