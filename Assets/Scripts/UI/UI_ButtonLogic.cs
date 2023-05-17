using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_ButtonLogic : MonoBehaviour
{
    public void LoadMenuScene() => SceneManager.LoadScene("StartScene");
    public void LoadLevelScene() => SceneManager.LoadScene("LoadLevelScene");
    public void LoadEndgameScene() => SceneManager.LoadScene("EndScene");
    public void CloseGame() => Application.Quit();
    
    public void ChangeTextColorHover(GameObject text)
    {
        text.GetComponent<TextMeshProUGUI>().color = Color.gray;
    }
    
    public void ChangeTextColorExit(GameObject text)
    {
        text.GetComponent<TextMeshProUGUI>().color = Color.black;
    }
}