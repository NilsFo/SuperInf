using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnde : MonoBehaviour
{

    public string nextLevel = "";
    public string mainLevel = "MainMenü";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (nextLevel.Length > 0)
            {
                SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(mainLevel, LoadSceneMode.Single);
            }
        }
    }
}
