using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour {
    public AudioSource typer_audio;
    public GameObject text_object;
    public static float seconds_per_character = 0.1f;
    public static string message = "";
    public static int character_index = 0;
    private Text text;
    float timer = 0;

    void Awake() {
        text = text_object.GetComponent<Text>();
    }

    void Start() {
    }

    void Update() {
        if (message != "") {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                timer = seconds_per_character;
                character_index++;
                text.text = message.Substring(0, character_index) + 
                "<color=#00000000>" + message.Substring(character_index) + 
                "</color>";
                typer_audio.Play(0);
                if (character_index >= message.Length) {
                    message = "";
                    character_index = 0;
                }
            }
        }
    }
}
