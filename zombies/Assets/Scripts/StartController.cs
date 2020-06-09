using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartController : MonoBehaviour {
    
    public GameObject menu;
    public GameObject cutscene_message;
    public GameObject main_camera;
    public GameObject zombies_camera;
    public GameObject scouts_camera;

    string zombies_message = 
      "Looks like we arrive to a zombie infested world.";
    string scouts_message = "Remember, we are here for the material this "
      + "people were designing before that accident that turn everybody into "
      + "those things.\n\nLooks like our scouts already found the area of the "
      + "barrels. Alright it's your turn to do the job, be quick and bring "
      + "those barrels here.";
    string instructions_message = "Take this energy weapon with you, it's "
      + "the latest innovation of our civilization. Take care of "
      + "those zombies, and don't let them get close to you.\n"
      + "We can't lose you here soldier.\nGood Luck!";
    string controls_message = "W\t\tMove Forward\nA\t\tMove Left\n"
      + "S\t\tMove Backward\nD\t\tMove Right\nSpace\t\tJump\n"
      + "R\t\tReload\nE\t\tInteract\nLeft Click\t\tFire"
      + "Left Shift\t\tRun";

    bool playing_cutscene = false;

    void Awake() {
        zombies_camera.SetActive(false);
        scouts_camera.SetActive(false);
        cutscene_message.SetActive(false);
    }

    void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;        
    }

    IEnumerator CutScene() {
        playing_cutscene = true;
        zombies_camera.SetActive(true);
        main_camera.SetActive(false);
        float delay = 5.0f;
        TextAnimation.character_index = 0;
        TextAnimation.seconds_per_character = 
          (delay) / (zombies_message.Length + 20);
        TextAnimation.message = zombies_message;
        yield return new WaitForSeconds(delay);
        scouts_camera.SetActive(true);
        zombies_camera.SetActive(false);
        delay = 35.0f;
        TextAnimation.character_index = 0;
        TextAnimation.seconds_per_character = 
          delay / (scouts_message.Length + 45);
        TextAnimation.message = scouts_message;
        yield return new WaitForSeconds(delay);
        delay = 35.0f;
        TextAnimation.character_index = 0;
        TextAnimation.seconds_per_character = 
          delay / (instructions_message.Length + 120);
        TextAnimation.message = instructions_message;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainGame");
    }

    public void ChangeScene(string scene)
    {
        if (scene == "MainGame") {
            menu.SetActive(false);
            cutscene_message.SetActive(true);
            StartCoroutine(CutScene());
        } else {
            SceneManager.LoadScene(scene);
        }
    }

    void Update() {
        if (playing_cutscene) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene("MainGame");
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
