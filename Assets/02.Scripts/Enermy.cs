using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    enum State
    {
        Idle,
        Run
    }

    public float moveSpeed;
    public float detectRadius;
    private State state;
    private Transform targetRaptor;
    [SerializeField] private bool isTargetOn;
    
    void Start()
    {
        GetComponent<Animator>().speed = 0f;
    }
    
    void Update()
    {
        SetState();
    }

    private void SetState()
    {
        switch (state)
        {
            case State.Idle:
            DetectDino();
            break;

            case State.Run:
            GoToDino();
            break;
        }
    }

    private void DetectDino()
    {
        if (isTargetOn.Equals(true))
        {
            return;
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (Collider colls in hitColliders)
        {
            // if (colls.gameObject.GetComponent<Raptor>() != null)
            // {
            //     if (colls.gameObject.GetComponent<Raptor>().IsTarget())
            //     continue;

            //     colls.gameObject.GetComponent<Raptor>().SetTarget();
            //     targetRaptor = colls.gameObject.transform;
            //     StartGotoDino();
            // }
            Raptor raptor = colls.GetComponent<Raptor>();
            if (raptor != null && raptor.IsTarget().Equals(false))  // Raptor가 타겟이 아닌 경우만 검사
            {
            Invoke("SetTargetDino", 0.1f);
            targetRaptor = raptor.transform;
            break;  // 첫 번째 타겟만설정하고루프중단
            }
        }
    }

    private void SetTargetDino()
    {
        // Raptor가 타겟이지정되있지않은경우만검사
        if (targetRaptor != null && targetRaptor.GetComponent<Raptor>().IsTarget().Equals(false))
        {
        targetRaptor.GetComponent<Raptor>().SetTarget();
        isTargetOn = true;  // 나는 찜 했어
        StartGotoDino();  // 상태 변경
        }
    }

    private void StartGotoDino()
    {
        state = State.Run;
        GetComponent<Animator>().speed = 1f;
    }

    private void GoToDino()
    {
        if (targetRaptor == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetRaptor.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetRaptor.position) < 0.1f)
        {
            SoundManager.instance.DinoDieSoundPlay(); // Raptor가 삭제될 때 죽는 사운드 출력
            Destroy(targetRaptor.gameObject);
            Destroy(this.gameObject);
        }
    }
}
