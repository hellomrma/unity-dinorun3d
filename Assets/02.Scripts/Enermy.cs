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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (Collider colls in hitColliders)
        {
            if (colls.gameObject.GetComponent<Raptor>() != null)
            {
                if (colls.gameObject.GetComponent<Raptor>().IsTarget())
                continue;

                colls.gameObject.GetComponent<Raptor>().SetTarget();
                targetRaptor = colls.gameObject.transform;
                StartGotoDino();
            }
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
            Destroy(targetRaptor.gameObject);
            Destroy(this.gameObject);
        }
    }
}
