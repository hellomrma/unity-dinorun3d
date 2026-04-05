using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 적 캐릭터(Enemy) 동작을 제어하는 컴포넌트
// Idle 상태에서 주변 랩터를 탐지하고, 발견 시 Run 상태로 전환해 추격 후 충돌 처리한다
public class Enermy : MonoBehaviour
{
    // 적의 행동 상태
    enum State
    {
        Idle, // 대기 상태: 주변 랩터 탐지 중
        Run   // 추격 상태: 타겟 랩터를 향해 이동 중
    }

    public float moveSpeed;    // 추격 시 이동 속도
    public float detectRadius; // 랩터 탐지 반경 (구형 범위)

    private State state;             // 현재 행동 상태
    private Transform targetRaptor;  // 추격 중인 랩터의 Transform

    void Start()
    {
        // 시작 시 Animator 속도를 0으로 설정해 Idle 애니메이션을 정지 상태로 둔다
        GetComponent<Animator>().speed = 0f;
    }

    void Update()
    {
        SetState();
    }

    // 현재 state에 따라 적절한 동작을 수행한다
    private void SetState()
    {
        switch (state)
        {
            case State.Idle:
                DetectDino(); // 대기 중: 매 프레임 랩터 탐지
                break;

            case State.Run:
                GoToDino();   // 추격 중: 타겟을 향해 이동
                break;
        }
    }

    // detectRadius 내의 Collider를 검사해 아직 타겟 지정되지 않은 랩터를 찾는다
    // 찾으면 해당 랩터를 타겟으로 지정하고 추격을 시작한다
    private void DetectDino()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (Collider colls in hitColliders)
        {
            if (colls.gameObject.GetComponent<Raptor>() != null) {}
            {
                // 이미 다른 적이 추격 중인 랩터는 건너뜀
                if (colls.gameObject.GetComponent<Raptor>().IsTarget())
                    continue;

                // 타겟 지정 후 추격 시작
                colls.gameObject.GetComponent<Raptor>().SetTarget();
                targetRaptor = colls.gameObject.transform;
                StartGotoDino();
            }
        }
    }

    // Run 상태로 전환하고 Animator 속도를 복원해 이동 애니메이션을 재생한다
    private void StartGotoDino()
    {
        state = State.Run;
        GetComponent<Animator>().speed = 1f;
    }

    // 타겟 랩터를 향해 이동한다
    // 타겟과 0.1f 이내로 가까워지면 타겟과 자신을 모두 파괴한다 (충돌 처리)
    private void GoToDino()
    {
        if (targetRaptor == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetRaptor.position, moveSpeed * Time.deltaTime);

        // 타겟에 충분히 근접했으면 양쪽 오브젝트 제거
        if (Vector3.Distance(transform.position, targetRaptor.position) < 0.1f)
        {
            Destroy(targetRaptor.gameObject);
            Destroy(this.gameObject);
        }
    }
}
