using UnityEngine;

/// <summary>
/// 공룡(Dino) 캐릭터의 이동을 제어하는 컴포넌트.
/// Z축 방향으로 자동 전진하며, 좌우 방향키로 X축 이동이 가능하다.
/// </summary>
public class DinoController : MonoBehaviour
{

    public DinoPositionController dinoPositionController; // 랩터 배치 컨트롤러 참조

    public float zMoveSpeed; // Z축(전진) 이동 속도
    public float xMoveSpeed; // X축(좌우) 이동 속도

    public Vector3 sphereCenter; // 충돌 감지용 구체의 중심 위치
    public float sphereRadius = 0.5f;   // 충돌 감지용 구체의 반지름

    void Start()
    {
    }

    /// <summary>매 프레임 이동 처리와 문 충돌 감지를 수행합니다.</summary>
    void Update()
    {
        DinoMove();
        DoorCheck();
    }

    /// <summary>
    /// Z축 자동 전진과 좌우 방향키 입력을 처리하며, X축 이동 범위를 제한합니다.
    /// </summary>
    private void DinoMove()
    {
        // 매 프레임 Z축 방향으로 자동 전진
        transform.position += Vector3.forward * Time.deltaTime * zMoveSpeed;

        // 왼쪽 방향키: X축 음의 방향으로 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-xMoveSpeed * Time.deltaTime, 0, 0);
        }

        // 오른쪽 방향키: X축 양의 방향으로 이동
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(xMoveSpeed * Time.deltaTime, 0, 0);
        }

        // X축 이동 범위를 -3.8 ~ 3.8 사이로 제한 (화면 밖으로 나가지 않도록)
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.8f, 3.8f), transform.position.y, transform.position.z);
        
    }
    
    /// <summary>
    /// OverlapSphere로 주변 콜라이더를 감지하고, 문과 충돌 시 해당 문의 타입과 숫자를
    /// DinoPositionController에 전달하여 랩터 수를 갱신합니다.
    /// </summary>
    void DoorCheck()
    {
        // 충돌 감지용 구체를 현재 위치에 생성하여 충돌 여부 확인
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + sphereCenter, sphereRadius);
        foreach (Collider doors in hitColliders)
        {
            int doorNum = doors.gameObject.GetComponent<SelectDoors>().GetDoorNumber(transform.position.x);
            DoorType doorType = doors.gameObject.GetComponent<SelectDoors>().GetDoorType(transform.position.x);

            dinoPositionController.SetDoorCalc(doorType, doorNum);
        }
    }

    /// <summary>Unity 에디터에서 충돌 감지 구체 범위를 빨간 와이어프레임으로 시각화합니다.</summary>
    void OnDrawGizmos()
    {
        // 충돌 감지용 구체를 에디터에서 시각적으로 표시 (디버깅 용도)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + sphereCenter, sphereRadius);
    }
}
