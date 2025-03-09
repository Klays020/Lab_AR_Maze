using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class MainMenuLoader : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    //[SerializeField] private ARSession arSession;
    public void LoadMainMenu()
    {
        //Destroy(arSession.gameObject);
        //Debug.Log("ARSession уничтожен");

        Debug.Log("Переход в главное меню: " + mainMenuSceneName);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
