using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;        
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        if (scene == "exit")
        {
            Application.Quit();
        }        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
