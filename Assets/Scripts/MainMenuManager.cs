using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1); //Can also use scene name
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
