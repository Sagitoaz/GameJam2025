using UnityEngine;

public class MainCam : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    void Start()
    {
        transform.position = new Vector3(-10, 0, -10);
    }
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
