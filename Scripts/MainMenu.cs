using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void exitButton() 
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
