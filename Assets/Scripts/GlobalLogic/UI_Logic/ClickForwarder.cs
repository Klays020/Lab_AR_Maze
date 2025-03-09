using UnityEngine;
using UnityEngine.EventSystems;

public class ClickForwarder : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("������ �� ������������ ������ � ����������� DifficultySelector")]
    [SerializeField] private GameObject targetObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (targetObject != null)
        {
            DifficultySelector ds = targetObject.GetComponent<DifficultySelector>();
            if (ds != null)
            {
                // ���������� ������� �����
                ds.OnPointerClick(eventData);
                Debug.Log("ClickForwarder �������� ���� �������� " + targetObject.name);
            }
            else
            {
                Debug.LogWarning("��������� DifficultySelector �� ������ �� " + targetObject.name);
            }
        }
        else
        {
            Debug.LogWarning("TargetObject �� ����� � ClickForwarder.");
        }
    }
}
