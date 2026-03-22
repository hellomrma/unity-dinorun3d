using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// 선택 문(좌/우)의 타입과 숫자를 관리하는 컴포넌트.
/// 문에 표시되는 연산 타입(Plus/Minus)과 수치를 설정하고,
/// 정답/오답 여부에 따라 색상을 변경한다.
/// </summary>

/// <summary>문의 연산 타입: Plus(더하기) 또는 Minus(빼기)</summary>
public enum DoorType
{
    Plus,
    Minus,
    Times,
    Division
}

public class SelectDoors : MonoBehaviour
{


    // 오른쪽/왼쪽 문의 스프라이트 렌더러 (색상 변경에 사용)
    public SpriteRenderer rightDoorSpriteRD;
    public SpriteRenderer leftDoorSpriteRD;

    // 오른쪽/왼쪽 문에 표시되는 텍스트 (연산 타입 + 숫자)
    public TextMeshPro rightDoorText;
    public TextMeshPro leftDoorText;

    // [SerializeField]: private 변수임에도 Unity 인스펙터 창에서 값을 직접 편집할 수 있게 해주는 어트리뷰트.
    // private으로 캡슐화를 유지하면서도 에디터에서 손쉽게 설정할 수 있어, public으로 노출하는 것보다 안전하다.
    [SerializeField]
    private DoorType rightDoorType; // 오른쪽 문의 연산 타입
    public int rightDoorNumber;     // 오른쪽 문에 표시될 숫자

    [SerializeField]
    private DoorType leftDoorType;  // 왼쪽 문의 연산 타입
    public int leftDoorNumber;      // 왼쪽 문에 표시될 숫자

    public Color goodColor; // 정답 문에 적용할 색상
    public Color badColor;  // 오답 문에 적용할 색상

    void Start()
    {
        SettingDoors();
    }

    /// <summary>
    /// 좌/우 문의 색상과 텍스트를 각 DoorType에 맞게 초기화한다.
    /// Start()에서 호출되며, 외부에서도 호출 가능(public).
    /// </summary>
    public void SettingDoors()
    {
        // 오른쪽 문 설정: DoorType에 따라 색상과 연산 기호 텍스트를 지정
        if (rightDoorType.Equals(DoorType.Plus))
        {
            rightDoorSpriteRD.color = goodColor;    // 정답 색상
            rightDoorText.text = "+" + rightDoorNumber;
        }
        else if (rightDoorType.Equals(DoorType.Minus))
        {
            rightDoorSpriteRD.color = badColor;     // 오답 색상
            rightDoorText.text = "-" + rightDoorNumber;
        }
        else if (rightDoorType.Equals(DoorType.Times))
        {
            rightDoorSpriteRD.color = goodColor;    // 정답 색상
            rightDoorText.text = "x" + rightDoorNumber;
        }
        else if (rightDoorType.Equals(DoorType.Division))
        {
            rightDoorSpriteRD.color = badColor;     // 오답 색상
            rightDoorText.text = "÷" + rightDoorNumber;
        }

        // 왼쪽 문 설정: 오른쪽과 동일한 방식으로 처리
        if (leftDoorType.Equals(DoorType.Plus))
        {
            leftDoorSpriteRD.color = goodColor;
            leftDoorText.text = "+" + leftDoorNumber;
        }
        else if (leftDoorType.Equals(DoorType.Minus))
        {
            leftDoorSpriteRD.color = badColor;
            leftDoorText.text = "-" + leftDoorNumber;
        }
        else if (leftDoorType.Equals(DoorType.Times))
        {
            leftDoorSpriteRD.color = goodColor;
            leftDoorText.text = "x" + leftDoorNumber;
        }
        else if (leftDoorType.Equals(DoorType.Division))
        {
            leftDoorSpriteRD.color = badColor;
            leftDoorText.text = "÷" + leftDoorNumber;
        }
    }

    public DoorType GetDoorType(float xPos)
    {
        // xPos가 0보다 크면 오른쪽 문, 그렇지 않으면 왼쪽 문으로 간주하여 해당 DoorType 반환
        if (xPos > 0)
        {
            return rightDoorType;
        }
        else
        {
            return leftDoorType;
        }
    }

    public int GetDoorNumber(float xPos)
    {
        // xPos가 0보다 크면 오른쪽 문, 그렇지 않으면 왼쪽 문으로 간주하여 해당 숫자 반환
        if (xPos > 0)
        {
            return rightDoorNumber;
        }
        else
        {
            return leftDoorNumber;
        }
    }
}
