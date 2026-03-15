using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] mapPrefabs;  // 랜덤으로 생성할 맵 프리팹 목록

    // 시작 시 맵 5개를 순서대로 이어 붙여 생성한다.
    void Start()
    {
        Vector3 mapPosition = Vector3.zero;
        Transform generatedMapsParent = GameObject.Find("GeneratedMaps").transform;  // 생성된 맵들을 정리할 부모 오브젝트

        for (int i = 0; i < 5; i++)
        {
            GameObject selectedMap = mapPrefabs[Random.Range(0, mapPrefabs.Length)];  // 맵 프리팹 중 하나를 랜덤 선택

            mapPosition.y = selectedMap.transform.position.y;  // 프리팹이 가진 기본 y 위치를 유지

            if (i>0)
            {
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2f;  // 이전 위치에서 현재 맵의 절반 길이만큼 전진
            }

            GameObject nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity, generatedMapsParent);  // 선택한 맵 프리팹을 mapPosition 위치에 회전 없이 생성하고 GeneratedMaps의 자식으로 설정
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2f;  // 다음 맵 배치를 위해 현재 맵의 나머지 절반 길이만큼 전진
        }
    }
}

// Euler 회전에 대해서
// - Euler 회전은 3D 공간에서 오브젝트의 회전을 표현하는 방법 중 하나입니다.
// - 오일러 회전은 세 개의 축(X, Y, Z)에 대한 회전 각도로 표현됩니다. 예를 들어, (30, 45, 60)는 X축으로 30도, Y축으로 45도, Z축으로 60도 회전한다는 의미입니다.
// - Unity에서는 Quaternion과 함께 오일러 회전을 사용할 수 있습니다. Quaternion은 오일러 회전보다 더 안정적이고 부드러운 회전을 제공하지만, 오일러 회전은 직관적이고 이해하기 쉬운 방식으로 회전을 표현할 수 있습니다.
// - 오일러 회전은 회전 순서에 따라 결과가 달라질 수 있습니다. Unity에서는 기본적으로 ZXY 순서로 회전이 적용됩니다. 따라서, 오일러 회전을 사용할 때는 회전 순서를 고려해야 합니다.
// Y축을 -90, 90도로 변경할 경우 오브젝트가 좌우로 뒤집히는 현상이 발생할 수 있습니다. 이는 오일러 회전의 특성 때문입니다. Y축을 90도로 회전하면 오브젝트가 오른쪽으로 90도 회전하고, -90도로 회전하면 왼쪽으로 90도 회전하게 됩니다. 이로 인해 오브젝트가 좌우로 뒤집히는 것처럼 보일 수 있습니다. 따라서, Y축 회전을 조절할 때는 이러한 현상을 고려하여 적절한 각도를 선택해야 합니다.
// - 짐벌락(Gimbal Lock)은 오일러 회전에서 발생할 수 있는 문제로, 특정 회전 각도에서 두 축이 일치하여 하나의 자유도가 사라지는 현상입니다. 예를 들어, Y축을 90도로 회전하면 X축과 Z축이 일치하게 되어 오브젝트가 특정 방향으로만 회전할 수 있게 됩니다. 이로 인해 오브젝트의 회전이 제한되고, 원하는 방향으로 회전하기 어려워질 수 있습니다. 짐벌락을 방지하기 위해서는 Quaternion을 사용하는 것이 좋습니다.

// 쿼터니언은 왜 써야하나?
// - 쿼터니언(Quaternion)은 3D 공간에서 오브젝트의 회전을 표현하는 방법 중 하나입니다. 쿼터니언은 네 개의 요소(w, x, y, z)로 구성된 수학적 구조로, 오일러 회전보다 더 안정적이고 부드러운 회전을 제공합니다.
// - 쿼터니언은 오일러 회전에서 발생할 수 있는 짐벌락(Gimbal Lock) 문제를 방지할 수 있습니다. 짐벌락은 오일러 회전에서 특정 회전 각도에서 두 축이 일치하여 하나의 자유도가 사라지는 현상입니다. 쿼터니언은 이러한 문제를 방지하여 오브젝트의 회전을 보다 자유롭게 표현할 수 있습니다.
// - 쿼터니언은 오일러 회전보다 더 효율적으로 회전을 계산할 수 있습니다. 쿼터니언은 회전 연산을 수행할 때 더 적은 계산량으로 회전을 표현할 수 있으며, 오일러 회전보다 더 빠르게 회전을 계산할 수 있습니다.
// - 쿼터니언은 오일러 회전보다 더 부드러운 회전을 제공합니다. 쿼터니언은 회전 간의 보간(Interpolation)을 수행할 때 더 부드러운 결과를 제공하며, 오일러 회전보다 더 자연스러운 회전을 표현할 수 있습니다. 따라서, 3D 게임이나 애니메이션에서 오브젝트의 회전을 표현할 때 쿼터니언을 사용하는 것이 일반적입니다.
