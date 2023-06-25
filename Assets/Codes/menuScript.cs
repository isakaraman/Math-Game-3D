using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void levels()
    {
        SceneManager.LoadScene("Levels");
    }
    public void quitApp()
    {
        Application.Quit();
    }
}
