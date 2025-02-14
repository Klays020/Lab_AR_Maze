using UnityEngine;

public class GlobalScaleUIController : MonoBehaviour
{
    [Header("Scale Settings")]
    [Tooltip("��� ��������� ��������")]
    public float scaleStep = 0.1f;
    [Tooltip("����������� ������� ����������� ����������")]
    public float minScale = 0.2f;
    [Tooltip("������������ ������� ����������� ����������")]
    public float maxScale = 3f;

    // ����� ��� ������ "��������� �������"
    public void IncreaseScale()
    {
        if (GlobalContainer.Instance != null)
        {
            Vector3 currentScale = GlobalContainer.Instance.transform.localScale;
            float newScale = Mathf.Min(currentScale.x + scaleStep, maxScale);
            GlobalContainer.Instance.transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log("����� ������� (����������): " + newScale);
        }
    }

    // ����� ��� ������ "��������� �������"
    public void DecreaseScale()
    {
        if (GlobalContainer.Instance != null)
        {
            Vector3 currentScale = GlobalContainer.Instance.transform.localScale;
            float newScale = Mathf.Max(currentScale.x - scaleStep, minScale);
            GlobalContainer.Instance.transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log("����� ������� (����������): " + newScale);
        }
    }
}
