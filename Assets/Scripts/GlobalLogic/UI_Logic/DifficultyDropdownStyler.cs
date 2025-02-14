using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DifficultyDropdownStyler : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public Color highColor = Color.red;
    public Color mediumColor = Color.yellow;
    public Color defaultColor = Color.green;

    private void Start()
    {
        if (dropdown == null)
            dropdown = GetComponent<TMP_Dropdown>();

        Debug.Log("dropdown = GetComponent<TMP_Dropdown>();");
        UpdateCaptionColor(dropdown.value);
        dropdown.onValueChanged.AddListener(UpdateCaptionColor);
    }

    private void UpdateCaptionColor(int index)
    {
        if (dropdown.captionText != null)
        {
            if (index == 2)
            {
                dropdown.captionText.color = highColor;
                Debug.Log("выбран highColor");
            }
            else if (index == 1)
            {
                dropdown.captionText.color = mediumColor;
                Debug.Log("выбран mediumColor");
            }
            else
            {
                dropdown.captionText.color = defaultColor;
                Debug.Log("выбран defaultColor");
            }

        }
    }
}
