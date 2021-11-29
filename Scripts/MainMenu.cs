using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void exitButton() 
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        Application.Quit();
        Debug.Log("Game Exited");
    }
    public void startGame()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        SceneManager.LoadScene("Level1");
    }
}
