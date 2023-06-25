using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class levels : MonoBehaviour
{
    public Button[] level;

    void Start()
    {

        int episodes = PlayerPrefs.GetInt("whichLevel");
        for (int i = 0; i < episodes; i++)
        {
            level[i].interactable = true;
        }
    }
    public void level1()
    {
        SceneManager.LoadScene(1);
    }
    public void level2()
    {
        SceneManager.LoadScene(2);
    }
    public void level3()
    {
        SceneManager.LoadScene(3);
    }
    public void level4()
    {
        SceneManager.LoadScene(4);
    }
    public void level5()
    {
        SceneManager.LoadScene(5);
    }
    public void level6()
    {
        SceneManager.LoadScene(6);
    }
    public void level7()
    {
        SceneManager.LoadScene(7);
    }
    public void level8()
    {
        SceneManager.LoadScene(8);
    }
    public void level9()
    {
        SceneManager.LoadScene(9);
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
    }
}