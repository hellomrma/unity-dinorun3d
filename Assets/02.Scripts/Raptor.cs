using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 랩터(공룡) 오브젝트를 나타내는 컴포넌트
// 적(Enermy)이 추적할 대상 여부를 관리한다
public class Raptor : MonoBehaviour
{
    // 이미 적에게 추적 대상으로 지정됐는지 여부
    private bool isTarget;

    // 이 랩터를 추적 대상으로 지정한다
    // 여러 적이 동일한 랩터를 중복 추적하지 않도록 방지하는 데 사용된다
    public void SetTarget()
    {
        isTarget = true;
    }

    // 현재 이 랩터가 추적 대상인지 반환한다
    public bool IsTarget()
    {
        return isTarget;
    }
}
