using UnityEngine;
using UnityEngine.EventSystems;

public class ClickForwarder : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Ссылка на родительский объект с компонентом DifficultySelector")]
    [SerializeField] private GameObject targetObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (targetObject != null)
        {
            DifficultySelector ds = targetObject.GetComponent<DifficultySelector>();
            if (ds != null)
            {
                // Пересылаем событие клика
                ds.OnPointerClick(eventData);
                Debug.Log("ClickForwarder переслал клик родителю " + targetObject.name);
            }
            else
            {
                Debug.LogWarning("Компонент DifficultySelector не найден на " + targetObject.name);
            }
        }
        else
        {
            Debug.LogWarning("TargetObject не задан в ClickForwarder.");
        }
    }
}
