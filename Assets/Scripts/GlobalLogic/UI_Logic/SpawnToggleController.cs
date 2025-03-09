using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnToggleController : MonoBehaviour
{
    [Header("Дочерние объекты для состояния")]
    [Tooltip("Объект, который отображается, когда спавн отключён")]
    [SerializeField] private GameObject spawnDisabledChild;
    [Tooltip("Объект, который отображается, когда спавн включён")]
    [SerializeField] private GameObject spawnEnabledChild;

    public static bool IsSpawnEnabled { get; private set; } = false;

    public void OnPointerClick()
    {
        ToggleState();
    }

    private void Start()
    {

        UpdateChildStates();
    }

    private void ToggleState()
    {
        IsSpawnEnabled = !IsSpawnEnabled;
        UpdateChildStates();
        Debug.Log("Spawn Enabled: " + IsSpawnEnabled);
    }

    private void UpdateChildStates()
    {
        if (spawnDisabledChild != null)
            spawnDisabledChild.SetActive(!IsSpawnEnabled);
        if (spawnEnabledChild != null)
            spawnEnabledChild.SetActive(IsSpawnEnabled);
    }
}
