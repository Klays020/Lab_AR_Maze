using UnityEngine;

public class BallCombinedController : MonoBehaviour
{
    [Header("��������� ����������")]
    [Tooltip("��������� ����, ����������� � ����")]
    [SerializeField] private float forceMultiplier = 10f;
    [Tooltip("����������� ���������������� ������� (0-1, ��� 1 � ��������, 0 � ������������)")]
    [Range(0, 1)]
    [SerializeField] private float alpha = 0.9f;

    private Rigidbody rb;

    private Vector3 filteredTilt;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;

        Vector3 accel = new Vector3(Input.acceleration.x, 0, Input.acceleration.y);
        filteredTilt = accel;
    }

    private void FixedUpdate()
    {
        ProcessCombinedInput();
    }

    private void ProcessCombinedInput()
    {
        Vector3 accel = new Vector3(Input.acceleration.x, 0, Input.acceleration.y);

        Quaternion gyroAttitude = Input.gyro.attitude;
        Quaternion correction = Quaternion.Euler(90, 0, 0);
        Quaternion worldRotation = correction * gyroAttitude;

        Vector3 gyroTilt = worldRotation * Vector3.forward;
        gyroTilt.y = 0;

        filteredTilt = alpha * (filteredTilt + gyroTilt * Time.deltaTime) + (1 - alpha) * accel;

        rb.AddForce(filteredTilt * forceMultiplier);
    }
}
