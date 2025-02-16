using UnityEngine;

public class BallGyroController : MonoBehaviour
{
    [Header("Настройки управления")]
    [Tooltip("Множитель силы, применяемой к мячу")]
    [SerializeField] private float forceMultiplier = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    private void FixedUpdate()
    {
        ProcessGyroRaw();
    }

    // Получаем кватернион из гироскопа
    // Берем Input.gyro.attitude, преобразуем его в углы Эйлера,
    // Используем соответствующие компоненты для управления движением.
    private void ProcessGyroRaw()
    {
        Quaternion gyroAttitude = Input.gyro.attitude;
        Vector3 euler = gyroAttitude.eulerAngles;

        float deltaX = Mathf.DeltaAngle(0, euler.x);
        float deltaY = Mathf.DeltaAngle(0, euler.y);
        Vector3 force = new Vector3(deltaY, 0, -deltaX);
        rb.AddForce(force * forceMultiplier);
    }
}
