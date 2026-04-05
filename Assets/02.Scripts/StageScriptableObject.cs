using UnityEngine;

/// <summary>
/// 하나의 스테이지를 구성하는 맵 프리팹 목록을 담는 ScriptableObject.
/// Unity 에디터 메뉴 "Stage Objects/Stage"에서 에셋을 생성할 수 있습니다.
/// MapManager가 스테이지 번호에 따라 이 에셋을 선택하여 맵을 생성합니다.
/// </summary>
[CreateAssetMenu(fileName = "Stage", menuName = "Stage Objects/Stage", order = 0)]
public class StageScriptableObject : ScriptableObject
{
    /// <summary>이 스테이지를 구성하는 맵 프리팹 배열. 인덱스 순서대로 Z축 방향으로 배치됩니다.</summary>
    public Map[] maps;
}
