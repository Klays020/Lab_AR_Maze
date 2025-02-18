using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Настройки управления")]
    [Tooltip("Множитель силы, применяемой к мячу")]
    [SerializeField] private float forceMultiplier = 10f;

    [Tooltip("Использовать акселерометр (true) или гироскоп (false)")]
    [SerializeField] private bool useAccelerometer = true;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!useAccelerometer)
        {
            Input.gyro.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (useAccelerometer)
        {
            ProcessAccelerationCorrected();
        }
        else
        {
            ProcessGyroCorrected();
        }
    }

    private void ProcessAccelerationCorrected()
    {
        Vector3 acceleration = Input.acceleration;

        Vector3 tilt = new Vector3(acceleration.x, 0, acceleration.y);

        rb.AddForce(tilt * forceMultiplier);
    }

    private void ProcessGyroCorrected()
    {
        Quaternion deviceRotation = Input.gyro.attitude;
        Quaternion correction = Quaternion.Euler(90, 0, 0);
        Quaternion worldRotation = correction * deviceRotation;

        Vector3 tilt = worldRotation * Vector3.forward;

        rb.AddForce(new Vector3(tilt.x, 0, tilt.z) * forceMultiplier);
    }
}
