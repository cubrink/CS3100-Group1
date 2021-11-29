using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public void mainScene()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        SceneManager.LoadScene("MainMenu");
    }

    public void next()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        SceneManager.LoadScene("Tutorial2");
    }

    public void back()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        SceneManager.LoadScene("Tutorial");
    }
}
