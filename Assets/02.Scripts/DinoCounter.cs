using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 화면에 표시된 공룡(랩터) 수를 TextMeshPro UI에 실시간으로 반영하는 컴포넌트.
/// dinoParent의 자식 수를 매 프레임 읽어 텍스트를 갱신합니다.
/// </summary>
public class DinoCounter : MonoBehaviour
{
    /// <summary>현재 공룡 수를 표시할 TextMeshPro UI 텍스트</summary>
    public TextMeshPro dinoCountText;

    /// <summary>공룡 오브젝트들이 자식으로 등록된 부모 Transform</summary>
    public Transform dinoParent;

    /// <summary>매 프레임 부모 오브젝트의 자식 수를 UI에 반영합니다.</summary>
    void Update()
    {
        dinoCountText.text = dinoParent.childCount.ToString();
    }
}
