using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if(sceneName == "Menu")
        {
            GameObject radioCanvas = GameObject.FindGameObjectWithTag("RadioCanvas");
            Destroy(radioCanvas);
        }

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
