using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinoCounter : MonoBehaviour
{
    public TextMeshPro dinoCountText;  // 현재 공룡 수를 표시할 UI 텍스트
    public Transform dinoParent;  // 공룡 오브젝트들이 자식으로 등록된 부모 Transform

    // 매 프레임 부모 오브젝트의 자식 수를 UI에 반영한다.
    void Update()
    {
        dinoCountText.text = dinoParent.childCount.ToString();  // 현재 공룡 수를 문자열로 표시
    }
}
