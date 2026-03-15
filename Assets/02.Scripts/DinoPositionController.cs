using UnityEngine;

/// <summary>
/// 랩터(공룡) 오브젝트들을 원형으로 배치하는 컨트롤러
/// </summary>
public class DinoPositionController : MonoBehaviour
{
    /// <summary>랩터들이 자식으로 붙어 있는 부모 Transform (원형 배치의 중심)</summary>
    public Transform raptors;

    /// <summary>원형 배치의 반지름 (월드 단위)</summary>
    public float radius = 1f;

    /// <summary>원 위에 배치할 때 각도 간격을 조절하는 비율 (작을수록 더 촘촘하게 배치)</summary>
    public float ratio = 0.1f;

    // public float dinoGapX;  // (미사용) 가로 배치 시 오브젝트 간 X 간격

    /// <summary>게임 시작 시 한 번 호출되어 랩터 위치를 원형으로 설정</summary>
    void Start()
    {
        SetDinoPosition();
    }

    /// <summary>
    /// raptors의 모든 자식 오브젝트를 원 위에 균등하게 배치합니다.
    /// 각도 간격은 (자식 수 * ratio)로 나누어 계산합니다.
    /// </summary>
    private void SetDinoPosition()
    {
        for (int i =0; i < raptors.childCount; i++)
        {
            if (i > 8)
            {
                raptors.GetChild(i).gameObject.SetActive(false);
                continue;
            } else
            {
                if (raptors.childCount < 10)
                {
                    // 원을 자식 수와 ratio에 맞게 나눈 각도 간격 (도 단위)
                    float angleStep = 360f / (raptors.childCount * ratio);

                    // i번째 자식에 해당하는 각도 (도)
                    float angle = i * angleStep;
                    // 삼각함수 사용을 위해 라디안으로 변환
                    float angleRad = angle * Mathf.Deg2Rad;
                    // 원 위의 X, Z 좌표 (Y는 0으로 고정)
                    float x = Mathf.Cos(angleRad) * radius;
                    float z = Mathf.Sin(angleRad) * radius;

                    // 해당 자식의 로컬 위치를 원 위의 좌표로 설정
                    raptors.GetChild(i).localPosition = new Vector3(x, 0f, z);
                }
            }
        }

    }

    /// <summary>매 프레임 호출 (현재는 사용하지 않음, 예전 배치 방식 주석 보관)</summary>
    void Update()
    {
        // ----- 아래는 예전에 사용하던 세로/가로 배치 방식 (참고용) -----

        // 세로로 배치: 이 스크립트가 붙은 오브젝트의 자식 개수만큼 반복
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     float z = i * 3;  // Z축 간격
        //     transform.GetChild(i).localPosition = Vector3.back * z;
        // }

        // 가로로 배치: dinoGapX 간격으로 X축에 일렬 배치
        // float startPosX = (transform.childCount * (-dinoGapX / 2)) + (dinoGapX / 2);
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     transform.GetChild(i).localPosition = new Vector3(startPosX + (dinoGapX * i), 0, 0);
        // }
    }
}
