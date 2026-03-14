using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float zMoveSpeed;
    public float xMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * zMoveSpeed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-xMoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(xMoveSpeed * Time.deltaTime, 0, 0);
        }
    }
}
