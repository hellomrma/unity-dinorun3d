using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public GameObject[] mapPrefabs;  // 맵 프리팹 배열 (인스펙터에서 할당)

    // Start is called before the first frame update
    void Start()
    {
        // 맵 프리팹 중 하나를 랜덤으로 선택하여 인스턴스화
        // GeneratedMaps 오브젝트 안에 생성되도록 설정 (필요 시 부모 설정 추가)
        int randomIndex = Random.Range(0, mapPrefabs.Length);
        GameObject instantiatedMap = Instantiate(mapPrefabs[randomIndex], Vector3.zero, Quaternion.identity);
        instantiatedMap.transform.SetParent(GameObject.Find("GeneratedMaps").transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
