using UnityEngine;

/// <summary>
/// 랩터(공룡) 오브젝트들을 원형으로 배치하는 컨트롤러
/// </summary>
public class DinoPositionController : MonoBehaviour
{
    /// <summary>랩터들이 자식으로 붙어 있는 부모 Transform (원형 배치의 중심)</summary>
    public Transform raptors;

    /// <summary>랩터 프리팹 (PlusRaptor/TimesRaptor에서 동적 생성에 사용)</summary>
    public GameObject raptorPrefab;

    /// <summary>원형 배치의 반지름 (월드 단위)</summary>
    public float radius = 1f;

    /// <summary>원 위에 배치할 때 각도 간격을 조절하는 비율 (작을수록 더 촘촘하게 배치)</summary>
    public float ratio = 0.1f;

    /// <summary>게임 시작 시 한 번 호출되어 랩터 위치를 원형으로 설정</summary>
    void Start()
    {
        SetDinoPosition();
    }

    /// <summary>
    /// 플레이어가 통과한 문의 타입과 숫자에 따라 랩터 수를 조절합니다.
    /// </summary>
    /// <param name="doorType">통과한 문의 연산 타입 (Plus/Minus/Times/Division)</param>
    /// <param name="doorNumber">통과한 문에 표시된 숫자</param>
    public void SetDoorCalc(DoorType doorType, int doorNumber)
    {
        if (doorType.Equals(DoorType.Plus))
        {
            PlusRaptor(doorNumber);
        }
        else if (doorType.Equals(DoorType.Minus))
        {
            MinusRaptor(doorNumber);
        }
        else if (doorType.Equals(DoorType.Times))
        {
            TimesRaptor(doorNumber);
        }
        else if (doorType.Equals(DoorType.Division))
        {
            DivisionRaptor(doorNumber);
        }

        SetDinoPosition();
    }

    /// <summary>raptorPrefab을 number개만큼 생성하여 raptors의 자식으로 추가합니다.</summary>
    /// <param name="number">추가할 랩터 수</param>
    private void PlusRaptor(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(raptorPrefab, raptors);
        }
    }

    /// <summary>
    /// raptors의 자식 오브젝트를 뒤에서부터 number개만큼 제거합니다.
    /// number가 현재 자식 수보다 크면 전부 제거합니다.
    /// </summary>
    /// <param name="number">제거할 랩터 수</param>
    private void MinusRaptor(int number)
    {
        if (number > raptors.childCount)
        {
            number = raptors.childCount; // 현재 수보다 많이 제거하려 하면 전부 제거
        }

        int raptorNum = raptors.childCount;

        for (int i = raptorNum-1; i >= (raptorNum - number); i--)
        {
            Destroy(raptors.GetChild(i).gameObject);
        }   
    }

    /// <summary>현재 랩터 수에 number를 곱한 수가 되도록 랩터를 추가합니다.</summary>
    /// <param name="number">곱할 배수</param>
    private void TimesRaptor(int number)
    {
        int currentCount = raptors.childCount;
        int toAdd = currentCount * number - currentCount;

        for (int i = 0; i < toAdd; i++)
        {
            Instantiate(raptorPrefab, raptors);
        }
    }

    /// <summary>현재 랩터 수를 number로 나눈 수가 되도록 랩터를 제거합니다. (미구현)</summary>
    /// <param name="number">나눌 수</param>
    private void DivisionRaptor(int number)
    {
    }

    /// <summary>
    /// raptors의 모든 자식 오브젝트를 원 위에 균등하게 배치합니다.
    /// 9개 초과 시 초과분은 비활성화하며, 각도 간격은 (자식 수 * ratio)로 계산합니다.
    /// </summary>
    private void SetDinoPosition()
    {
        for (int i = 0; i < raptors.childCount; i++)
        {
            if (i > 8)
            {
                // 최대 9마리 초과분은 비활성화
                raptors.GetChild(i).gameObject.SetActive(false);
                continue;
            }

            if (raptors.childCount < 10)
            {
                float angleStep = 360f / (raptors.childCount * ratio); // 각도 간격 (도 단위)
                float angleRad = i * angleStep * Mathf.Deg2Rad;        // i번째 각도 (라디안)
                float x = Mathf.Cos(angleRad) * radius;
                float z = Mathf.Sin(angleRad) * radius;

                raptors.GetChild(i).localPosition = new Vector3(x, 0f, z);
            }
        }
    }

    void Update() { }
}
