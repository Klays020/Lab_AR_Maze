using UnityEngine;

public class FinishTileController : MonoBehaviour
{
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Победа!");
            if (rend != null)
            {
                rend.material.color = Color.green;
            }
        }
    }
}
