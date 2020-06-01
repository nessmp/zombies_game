using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Barrel : MonoBehaviour {
    public AudioSource pick_up_audio;

    private bool at_reach = false;
    private Text player_message;
    void Start() {
        player_message = GameObject.FindWithTag("Player_Message").
          GetComponent<Text>();
    }
    void Update() {
        if (at_reach) {
            if (Input.GetKeyDown(KeyCode.E) && !pick_up_audio.isPlaying) {
                player_message.text = "";
                pick_up_audio.Play(0);
                Image image = GameObject.FindWithTag(gameObject.name).
                  GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 255.0f;
                image.color = tempColor;
                Destroy(gameObject, pick_up_audio.clip.length);
                if (gameObject.name == "Barrel_Red") {
                    GameObject alien_beam = GameObject.
                      FindWithTag("Barrel_Red_Alien_Beam");
                    alien_beam.SetActive(false);
                    GunAnimations.red_barrel = true;
                } else if (gameObject.name == "Barrel_Blue") {
                    GameObject alien_beam = GameObject.
                      FindWithTag("Barrel_Blue_Alien_Beam");
                    alien_beam.SetActive(false);
                    GunAnimations.blue_barrel = true;
                } else if (gameObject.name == "Barrel_White") {
                    GameObject alien_beam = GameObject.
                      FindWithTag("Barrel_White_Alien_Beam");
                    alien_beam.SetActive(false);
                    GunAnimations.white_barrel = true;
                } else if (gameObject.name == "Barrel_Plastic") {
                    GameObject alien_beam = GameObject.
                      FindWithTag("Barrel_Plastic_Alien_Beam");
                    alien_beam.SetActive(false);
                    GunAnimations.plastic_barrel = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            player_message.text = "Press 'E' to pick up";
            at_reach = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            player_message.text = "";
            at_reach = false;
        }
    }
}
