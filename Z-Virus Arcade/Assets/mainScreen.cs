using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainScreen : MonoBehaviour
{
    public string newGameScene;
    // Start is called before the first frame update

    public void play()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void quit()
    {
        Application.Quit();
    }
}
