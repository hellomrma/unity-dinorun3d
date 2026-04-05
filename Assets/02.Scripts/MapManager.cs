using UnityEngine;

/// <summary>
/// 맵 세그먼트를 Z축 방향으로 이어 붙여 게임 스테이지를 생성하는 컴포넌트.
/// 첫 번째 맵은 항상 고정 프리팹(mapPrefabs[0])을 사용하고,
/// 이후 맵은 목록에서 랜덤하게 선택합니다.
/// </summary>
public class MapManager : MonoBehaviour
{

    public static MapManager instance;

    /// <summary>랜덤으로 선택할 맵 프리팹 목록. 인덱스 0은 항상 첫 번째 맵으로 사용됩니다.</summary>
    // public GameObject[] mapPrefabs;

    /// <summary>태그 "Goal"로 찾은 골 지점 오브젝트. 진행도 계산에 사용됩니다.</summary>
    public GameObject goalObject;

    /// <summary>테스트용 맵 프리팹 목록. CreateTestMap()에서 순서대로 배치됩니다.</summary>
    // public GameObject[] testMapPrefabs;

    public StageScriptableObject[] stages; // 스테이지별 맵 구성을 담은 ScriptableObject 배열

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

    /// <summary>게임 시작 시 맵 5개를 순서대로 이어 붙여 생성하고 골 지점을 탐색합니다.</summary>
    void Start()
    {
        CreateStage();
        goalObject = GameObject.FindWithTag("Goal");  // 태그가 "Goal"인 오브젝트를 찾아 goalObject에 할당
        GetGoalDistance();  // 목표 지점까지의 초기 거리를 계산하여 필요에 따라 활용할 수 있도록 합니다.
    }

    /// <summary>
    /// mapPrefabs 목록에서 맵을 선택하여 Z축 방향으로 5개 이어 붙입니다.
    /// 각 맵의 Z 길이(GetMapSize())를 기준으로 다음 맵의 배치 위치를 계산합니다.
    /// 생성된 맵들은 씬의 "GeneratedMaps" 오브젝트 하위에 정리됩니다.
    /// </summary>
    private void CreateMap()
    {
        Vector3 mapPosition = Vector3.zero;
        Transform generatedMapsParent = GameObject.Find("GeneratedMaps").transform;  // 생성된 맵들을 정리할 부모 오브젝트

        for (int i = 0; i < 5; i++)
        {
            GameObject selectedMap;

            if (i>0)
            {
                selectedMap = mapPrefabs[Random.Range(1, mapPrefabs.Length)];  // 첫 번째 맵 이후부터는 랜덤으로 선택
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2f;  // 이전 위치에서 현재 맵의 절반 길이만큼 전진
            } else
            {
                selectedMap = mapPrefabs[0];  // 첫 번째 맵은 항상 고정된 프리팹으로 선택
            }

            GameObject nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity, generatedMapsParent);  // 선택한 맵 프리팹을 mapPosition 위치에 회전 없이 생성하고 GeneratedMaps의 자식으로 설정
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2f;  // 다음 맵 배치를 위해 현재 맵의 나머지 절반 길이만큼 전진
        }
    }

    /// <summary>
    /// testMapPrefabs 목록에 정의된 맵을 순서대로 Z축 방향으로 이어 붙입니다.
    /// 실제 게임 대신 테스트용 맵 구성을 사용할 때 호출됩니다.
    /// </summary>
    private void CreateTestMap()
    {
        Vector3 mapPosition = Vector3.zero;
        Transform generatedMapsParent = GameObject.Find("GeneratedMaps").transform;  // 생성된 맵들을 정리할 부모 오브젝트

        for (int i = 0; i < testMapPrefabs.Length; i++)
        {
            GameObject selectedMap = testMapPrefabs[i];  // 첫 번째 맵은 항상 고정된 프리팹으로 선택

            if (i>0)
            {
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2f;  // 이전 위치에서 현재 맵의 절반 길이만큼 전진
            }

            GameObject nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity, generatedMapsParent);  // 선택한 맵 프리팹을 mapPosition 위치에 회전 없이 생성하고 GeneratedMaps의 자식으로 설정
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2f;  // 다음 맵 배치를 위해 현재 맵의 나머지 절반 길이만큼 전진
        }
    }

    private void CreateStage()
    {
        int currentStageIndex = GetStage();
        currentStageIndex = currentStageIndex % stages.Length; // 스테이지 인덱스가 stages 배열 범위를 벗어나지 않도록 모듈로 연산
        StageScriptableObject stage = stages[currentStageIndex];

        CreateMap(stage.maps);
    }
    
    private void CreateMap(Map[] stageMaps)
    {
        Vector3 mapPosition = Vector3.zero;

        for (int i = 0; i < stageMaps.Length; i++)
        {
            Map selectedMap = stageMaps[i];  // 첫 번째 맵은 항상 고정된 프리팹으로 선택

            if (i>0)
            {
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2f;  // 이전 위치에서 현재 맵의 절반 길이만큼 전진
            }
            Map nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity, transform);  // 선택한 맵 프리팹을 mapPosition 위치에 회전 없이 생성
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2f;  // 다음 맵 배치를 위해 현재 맵의 나머지 절반 길이만큼 전진
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
