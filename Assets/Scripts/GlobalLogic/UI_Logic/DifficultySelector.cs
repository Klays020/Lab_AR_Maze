using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Difficulty difficulty;

    private Outline outline;
    private Color normalColor = Color.clear;
    private Color selectedColor = Color.yellow;

    private void Awake()
    {
        outline = GetComponent<Outline>();

        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }

        outline.effectColor = normalColor;
        outline.effectDistance = new Vector2(10, -10);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameSettings.selectedDifficulty = difficulty;
        Debug.Log("Выбрана сложность: " + difficulty);
        UpdateAllSelectorsVisual();

        if (PlayButtonHandler.Instance != null)
        {
            //GameSettings.difficultyChosen = true;
            PlayButtonHandler.Instance.SetInteractable(true);
        }    
            
    }

    private void UpdateAllSelectorsVisual()
    {
        // Найти все объекты с компонентом DifficultySelector
        DifficultySelector[] selectors = FindObjectsOfType<DifficultySelector>();
        foreach (DifficultySelector ds in selectors)
        {
            if (ds.outline != null)
            {
                ds.outline.effectColor = (ds.difficulty == GameSettings.selectedDifficulty) ? selectedColor : normalColor;
            }
        }
    }
}
