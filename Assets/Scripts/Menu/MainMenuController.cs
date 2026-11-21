using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject SettingsPanel;

    [Header("First Selected Buttons")]
    [SerializeField] private GameObject MainMenuFirstButton;
    [SerializeField] private GameObject SettingsFirstButton;

    [Header("Gameplay Scene")]
    [SerializeField] private string SampleScene = "SampleScene";

    private void Start()
    {
        // When the game launches, show the main menu and hide settings
        ShowMainMenu();
    }

    public void OnStartGameClicked()
    {
        // Loads your first gameplay level
        SceneManager.LoadScene(SampleScene);
    }

    public void OnSettingsClicked()
    {
        // Hides the main buttons/title and opens the settings panel
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(SettingsFirstButton);
    }

    public void ShowMainMenu()
    {
        // Shows the main buttons/title and hides the settings panel
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(MainMenuFirstButton);
    }

    public void OnExitGameClicked()
    {
        // Closes the built game
        Application.Quit();

        // Also stops Play Mode when testing inside the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}