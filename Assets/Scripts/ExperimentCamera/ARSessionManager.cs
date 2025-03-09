using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionManager : MonoBehaviour
{
    private void OnDestroy()
    {
        // ���������� ARSession ����� �������
        ARSession arSession = FindObjectOfType<ARSession>();
        if (arSession != null)
        {
            Destroy(arSession.gameObject);
        }
    }
}
