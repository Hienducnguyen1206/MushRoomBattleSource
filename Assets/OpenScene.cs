using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenScene : MonoBehaviour
{

    public void Playgame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("OpenScene");
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#endif
    }

}
