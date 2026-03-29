using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 개별 맵 세그먼트의 크기 정보를 보관하는 컴포넌트.
/// MapManager가 맵을 Z축 방향으로 이어 붙일 때
/// 각 맵의 길이를 조회하는 데 사용됩니다.
/// </summary>
public class Map : MonoBehaviour
{
    /// <summary>맵의 크기 (x: 폭, y: 높이, z: 길이). 배치 계산에는 z값이 사용됩니다.</summary>
    public Vector3 mapSize;

    /// <summary>이 맵 세그먼트의 Z축 길이를 반환합니다. MapManager의 맵 배치 계산에 사용됩니다.</summary>
    public float GetMapSize()
    {
        return mapSize.z;
    }
}
