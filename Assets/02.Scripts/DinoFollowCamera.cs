using UnityEngine;

public class DinoFollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = target.position - transform.position;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, target.position.z - offset.z);
            transform.position = newPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
