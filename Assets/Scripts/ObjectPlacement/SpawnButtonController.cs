using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonController : MonoBehaviour
{
    private Button spawnButton;

    private void Awake()
    {
        spawnButton = GetComponent<Button>();
        if (spawnButton == null)
        {
            Debug.LogError("SpawnButtonController: Компонент Button не найден!");
        }
    }

    private void Start()
    {
        if (spawnButton != null)
        {
            spawnButton.interactable = SpawnToggleController.IsSpawnEnabled;
            Debug.Log("SpawnButton interactable: " + spawnButton.interactable);
        }
    }
}
