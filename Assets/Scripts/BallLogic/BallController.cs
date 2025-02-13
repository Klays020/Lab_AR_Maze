using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Настройки управления")]
    [Tooltip("Множитель силы, применяемой к мячу.")]
    [SerializeField] private float forceMultiplier;
    [SerializeField] private float correctionAngleZ = 90f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    private void FixedUpdate()
    {
        Vector3 tilt =new Vector3(Input.acceleration.x, 0, Input.acceleration.y);
        rb.AddForce(tilt * forceMultiplier);

    }
    

    /*
private void FixedUpdate()
{
    Vector3 rawAcceleration = Input.acceleration;

    Quaternion correction = Quaternion.Euler(0, 0, correctionAngleZ);
    Vector3 correctedAcceleration = correction * rawAcceleration;

    Vector3 tilt = new Vector3(correctedAcceleration.x, 0, correctedAcceleration.y);
    rb.AddForce(tilt * forceMultiplier);
}
    */
}
