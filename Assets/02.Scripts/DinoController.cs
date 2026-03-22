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
            // dino의 x좌표가 -3.8보다 작아지는 것을 방지
            if (transform.position.x < -3.8f)            {
                transform.position = new Vector3(-3.8f, transform.position.y, transform.position.z);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(xMoveSpeed * Time.deltaTime, 0, 0);
            // dino의 x좌표가 3.8보다 커지는 것을 방지
            if (transform.position.x > 3.8f)            {
                transform.position = new Vector3(3.8f, transform.position.y, transform.position.z);
            }
        }
    }
}
