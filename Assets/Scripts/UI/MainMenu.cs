using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingsPanel;

    private List<int> _widths = new List<int>{568,960,1280,1920}; 
    private List<int> _heights = new List<int>{320,540,800,1080}; 

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        SetScreenSize(1);
    }

    public void GoToMain()
    {
        _mainPanel.SetActive(true);
        _settingsPanel.SetActive(false);
    }
    public void GoToSettings()
    {
        _mainPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }
    public void SetScreenSize(int index)
    {
        bool isFullscreen = Screen.fullScreen;
        Screen.SetResolution(_widths[index],_heights[index],isFullscreen);
    }
    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    public void LoadLevelScene() => SceneManager.LoadScene("LoadLevelScene");
    public void CloseGame() => Application.Quit();
 
}
