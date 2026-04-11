using UnityEngine;

public class EnermyRaptors : MonoBehaviour
{
    public GameObject enermyRaptorPrefab;
    public int enermyRaptorNumber;
    public Transform enermyRaptorsParent; // 생성된 EnemyRaptor들을 담을 부모 오브젝트
    public float initialRadius = 0f; // 첫 오브젝트의 반지름
    public float radiusGrowth = 0.12f;  // 오브젝트 간 반지름 증가량
    public float angleIncrement = 137.5f;  // 각도 증가 비율 (보통 골든 앵글 사용)
    void Start()
    {
        CreateEnemyRaptors();
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    private void CreateEnemyRaptors()
    {
        // 내가 정한수만큼EnemyRaptor생성
        for (int i = 0; i < enermyRaptorNumber; i++)
        {
            // 반지름이 점점커짐피보나치수열효과
            float currentRadius = initialRadius + (radiusGrowth * i);
            // 각도가 점점증가(오브젝트가계속나선형으로퍼져나감)
            float angle = i * angleIncrement;
            // 각도를 라디안단위로변환후좌표계산
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * currentRadius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * currentRadius;
            GameObject enemyRaptor = Instantiate(enermyRaptorPrefab, enermyRaptorsParent);
            enemyRaptor.gameObject.transform.localPosition = new Vector3(x, 0, z);
        }
    }
}
