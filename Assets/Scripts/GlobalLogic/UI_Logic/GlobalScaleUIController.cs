using UnityEngine;

public class GlobalScaleUIController : MonoBehaviour
{
    [Header("Scale Settings")]
    [Tooltip("Шаг изменения масштаба")]
    public float scaleStep = 0.1f;
    [Tooltip("Минимальный масштаб глобального контейнера")]
    public float minScale = 0.2f;
    [Tooltip("Максимальный масштаб глобального контейнера")]
    public float maxScale = 3f;

    // Метод для кнопки "Увеличить масштаб"
    public void IncreaseScale()
    {
        if (GlobalContainer.Instance != null)
        {
            Vector3 currentScale = GlobalContainer.Instance.transform.localScale;
            float newScale = Mathf.Min(currentScale.x + scaleStep, maxScale);
            GlobalContainer.Instance.transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log("Новый масштаб (увеличение): " + newScale);
        }
    }

    // Метод для кнопки "Уменьшить масштаб"
    public void DecreaseScale()
    {
        if (GlobalContainer.Instance != null)
        {
            Vector3 currentScale = GlobalContainer.Instance.transform.localScale;
            float newScale = Mathf.Max(currentScale.x - scaleStep, minScale);
            GlobalContainer.Instance.transform.localScale = new Vector3(newScale, newScale, newScale);
            Debug.Log("Новый масштаб (уменьшение): " + newScale);
        }
    }
}
