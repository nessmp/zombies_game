using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Buttons : MonoBehaviour {
    public void ChangeSceneButton(string scene){
        if (scene == "Quit") {
            Application.Quit();
        } else {
            SceneManager.LoadScene(scene);
        }
    }
}
