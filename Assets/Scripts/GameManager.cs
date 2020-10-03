using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string[] levelName;
    public GameObject[] canvasList;

    public void showCanvas(int number)
    {
        for (int i = 0; i < canvasList.Length; i++)
        {
            GameObject currentGameObject = canvasList[i];
            if (i == number)
            {
                currentGameObject.SetActive(true);
            }
            else
            {
                currentGameObject.SetActive(false);
            }
        }
    }

    public void loadLevel(int number)
    {
        SceneManager.LoadScene(levelName[number], LoadSceneMode.Single);
    }

    public void quitGame()
    {
        Application.Quit();
    }
    
}
