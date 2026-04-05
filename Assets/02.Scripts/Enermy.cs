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
        
    }

    private void StartGotoDino()
    {
        
    }

    private void GoToDino()
    {
        
    }
}
