using UnityEngine;

/// <summary>
/// 공룡(랩터) 오브젝트들을 피보나치 나선형(황금각 기반)으로 배치하고,
/// 문 통과 시 연산 결과에 따라 랩터 수를 동적으로 조절하는 컨트롤러
/// </summary>
public class DinoPositionController : MonoBehaviour
{
    /// <summary>랩터들이 자식으로 붙어 있는 부모 Transform (배치 기준 중심)</summary>
    public Transform raptors;

    /// <summary>랩터 프리팹 — Plus/Times 연산 시 동적 생성에 사용</summary>
    public GameObject raptorPrefab;

    // ── 이전 방식(균등 원형 배치) ──────────────────────────────────────────
    // public float radius = 1f;   // 원형 배치의 반지름 (월드 단위)
    // public float ratio  = 0.1f; // 각도 간격 비율 (작을수록 촘촘)
    // ────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// 화면에 실제로 표시할 랩터의 최대 수.
    /// 이 수를 초과하는 자식 오브젝트는 비활성화됩니다.
    /// </summary>
    public int visibleRaptorNumber;

    /// <summary>
    /// 첫 번째 랩터의 배치 반지름 (0이면 중심에 배치).
    /// 이후 랩터마다 radiusGrowth씩 증가하여 나선형을 형성합니다.
    /// </summary>
    public float initialRadius = 0f;

    /// <summary>
    /// 랩터 인덱스가 1 증가할 때마다 반지름에 더해지는 증분값.
    /// 값이 클수록 나선이 빠르게 퍼집니다.
    /// </summary>
    public float radiusGrowth = 0.12f;

    /// <summary>
    /// 연속된 랩터 간의 각도 간격 (도 단위).
    /// 137.508°는 황금각(Golden Angle)으로, 피보나치 나선 배치를 만들어
    /// 랩터들이 겹치지 않고 고르게 분포되도록 합니다.
    /// </summary>
    public float angleIncrement = 137.508f;

    /// <summary>게임 시작 시 초기 랩터 위치를 설정합니다.</summary>
    void Start()
    {
        SetDinoPosition();
    }

    /// <summary>
    /// 플레이어가 통과한 문의 연산 타입과 숫자에 따라 랩터 수를 조절한 뒤,
    /// 변경된 수에 맞게 위치를 재배치합니다.
    /// </summary>
    /// <param name="doorType">통과한 문의 연산 타입 (Plus/Minus/Times/Division)</param>
    /// <param name="doorNumber">통과한 문에 표시된 피연산자 숫자</param>
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

        // 랩터 수 변경 후 위치 재배치
        SetDinoPosition();
    }

    /// <summary>
    /// raptorPrefab을 number개 생성하여 raptors의 자식으로 추가합니다. (덧셈 문)
    /// </summary>
    /// <param name="number">추가할 랩터 수</param>
    private void PlusRaptor(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(raptorPrefab, raptors);
        }
    }

    /// <summary>
    /// raptors의 자식을 뒤에서부터 number개 제거합니다. (뺄셈 문)
    /// number가 현재 자식 수보다 크면 모두 제거합니다.
    /// </summary>
    /// <param name="number">제거할 랩터 수</param>
    private void MinusRaptor(int number)
    {
        // 현재 수보다 많이 제거하려 하면 전부 제거
        if (number > raptors.childCount)
        {
            number = raptors.childCount;
        }

        int raptorNum = raptors.childCount;

        // 마지막 인덱스(raptorNum-1)부터 number개를 역순으로 제거
        for (int i = raptorNum-1; i >= (raptorNum - number); i--)
        {
            Destroy(raptors.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 현재 랩터 수에 number를 곱한 만큼 늘어나도록 랩터를 추가합니다. (곱셈 문)
    /// 예: 현재 3마리 × 3 → 6마리 추가하여 총 9마리
    /// </summary>
    /// <param name="number">곱할 배수</param>
    private void TimesRaptor(int number)
    {
        int currentCount = raptors.childCount;
        int toAdd = currentCount * number - currentCount; // 추가해야 할 수 = 목표 수 - 현재 수

        for (int i = 0; i < toAdd; i++)
        {
            Instantiate(raptorPrefab, raptors);
        }
    }

    /// <summary>
    /// 현재 랩터 수를 number로 나눈 몫만 남기고 나머지를 제거합니다. (나눗셈 문)
    /// 소수점 이하는 버림(정수 나눗셈)으로 처리합니다.
    /// 예: currentCount=9, number=3 → toRemain=3, toRemove=6 → 마지막 6마리 제거
    /// </summary>
    /// <param name="number">나눌 수</param>
    private void DivisionRaptor(int number)
    {
        // 나눗셈은 정수 나눗셈으로 처리하여 소수점 이하는 버립니다.
        int currentCount = raptors.childCount;
        int toRemain = currentCount / number; // 남길 수 (소수점 버림)

        // 음수는 존재할 수 없으므로 0으로 처리
        if (toRemain < 0)
        {
            toRemain = 0;
        }

        int toRemove = currentCount - toRemain; // 제거할 수

        // 마지막 인덱스부터 역순으로 toRemove개 제거
        for (int i = currentCount - 1; i >= (currentCount - toRemove); i--)
        {
            Destroy(raptors.GetChild(i).gameObject);
        }

        // 위 코드를 현재 9일때 나누기3이 있다고 가정하고 코드에 직접 넣어봐 주세요
        // 예: currentCount = 9, number = 3, toRemain = 3, toRemove = 6
    }

    /// <summary>
    /// raptors의 자식 오브젝트를 황금각(137.508°) 기반 피보나치 나선형으로 배치합니다.
    ///
    /// 배치 공식 (i번째 랩터):
    ///   radius_i = initialRadius + radiusGrowth * i  (인덱스가 늘수록 반지름 증가)
    ///   angle_i  = i * angleIncrement                (황금각 누적 회전량, 도 단위)
    ///   x = cos(angle_i) * radius_i
    ///   z = sin(angle_i) * radius_i
    ///
    /// visibleRaptorNumber 이상 인덱스의 랩터는 비활성화됩니다.
    /// </summary>
    private void SetDinoPosition()
    {
        // ── 이전 방식(균등 원형 배치) ──────────────────────────────────────
        // for (int i = 0; i < raptors.childCount; i++)
        // {
        //     if (i > 8)
        //     {
        //         // 최대 9마리 초과분은 비활성화
        //         raptors.GetChild(i).gameObject.SetActive(false);
        //         continue;
        //     }

        //     if (raptors.childCount < 10)
        //     {
        //         float angleStep = 360f / (raptors.childCount * ratio); // 각도 간격 (도 단위)
        //         float angleRad = i * angleStep * Mathf.Deg2Rad;        // i번째 각도 (라디안)
        //         float x = Mathf.Cos(angleRad) * radius;
        //         float z = Mathf.Sin(angleRad) * radius;

        //         raptors.GetChild(i).localPosition = new Vector3(x, 0f, z);
        //     }
        // }
        // ────────────────────────────────────────────────────────────────────

        for (int i = 0; i < raptors.childCount; i++)
        {
            // 인덱스가 visibleRaptorNumber-1 초과 → 표시 한도 벗어남, 비활성화
            if (i > visibleRaptorNumber -1)
            {
                // visibleRaptorNumber를 초과하는 랩터는 비활성화
                raptors.GetChild(i).gameObject.SetActive(false);
                continue;
            } else
            {
                if (i < visibleRaptorNumber)
                {
                    float currentRadius = initialRadius + radiusGrowth * i; // 랩터 수에 따라 반지름 증가
                    float angle = i * angleIncrement; // 황금각을 이용한 각도 계산

                    float x = Mathf.Cos(angle * Mathf.Deg2Rad) * currentRadius;
                    float z = Mathf.Sin(angle * Mathf.Deg2Rad) * currentRadius;

                    raptors.GetChild(i).localPosition = new Vector3(x, 0f, z);
                    raptors.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    void Update() { }
}
