using UnityEngine;

/// <summary>
/// 공룡(Dino)을 Z축 방향으로 따라가는 카메라 컴포넌트.
/// X·Y 좌표는 고정하고 Z축만 대상(target)의 위치를 추적하여
/// 공룡이 전진할 때 카메라가 함께 이동하도록 합니다.
/// </summary>
public class DinoFollowCamera : MonoBehaviour
{
    /// <summary>추적할 대상 Transform (공룡 오브젝트)</summary>
    public Transform target;

    /// <summary>
    /// 카메라와 대상 사이의 초기 거리 벡터.
    /// Start()에서 자동 계산되며, Z 오프셋을 유지하는 데 사용됩니다.
    /// </summary>
    public Vector3 offset;

    /// <summary>게임 시작 시 카메라와 대상 간의 초기 오프셋을 계산합니다.</summary>
    void Start()
    {
        offset = target.position - transform.position;
    }

    /// <summary>
    /// 매 프레임 마지막에 호출되어 카메라의 Z 위치를 대상에 맞춰 갱신합니다.
    /// X·Y축은 고정되어 있어 카메라는 Z축(전진 방향)으로만 이동합니다.
    /// LateUpdate를 사용하여 이동 처리 이후에 카메라 위치를 갱신합니다.
    /// </summary>
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, target.position.z - offset.z);
            transform.position = newPosition;
        }
    }

    void Update()
    {

    }
}
