using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GunAnimations : MonoBehaviour {
    public AudioSource fire_audio;
    public AudioSource reload_audio;
    public AudioSource reload_open_audio;
    public AudioSource reload_close_audio;
    public AudioSource energy_power_up_audio;
    public Sprite battery_4_cells;
    public Sprite battery_3_cells;
    public Sprite battery_2_cells;
    public Sprite battery_1_cells;
    public Sprite battery_0_cells;
    public static int health = 100;
    public static int kill_count = 0;
    public static bool red_barrel = false;
    public static bool white_barrel = false;
    public static bool plastic_barrel = false;
    public static bool blue_barrel = false;

    Animator animator;
    Canvas gun_canvas;
    GameObject alien_beam;
    Image image;

    int ammo = 8;
    bool reload = false;
    
    void Start () {
        health = 100;
        kill_count = 0;
        red_barrel = false;
        white_barrel = false;
        plastic_barrel = false;
        blue_barrel = false;

        animator = GetComponent<Animator>();

        GameObject battery_game_object = GameObject.FindWithTag("battery");
        alien_beam = GameObject.FindWithTag("Alien_Beam");
        alien_beam.SetActive(false);
        image  = battery_game_object.GetComponent<Image>();
    }

    IEnumerator ReloadGun() {
        animator.SetBool("Reload", true);
        ammo = 8;
        reload_open_audio.PlayDelayed(0.25f);
        yield return new WaitForSeconds(0.8f);
        energy_power_up_audio.loop = true;
        energy_power_up_audio.Play();
        yield return new WaitForSeconds(1f);
        energy_power_up_audio.Stop();
        energy_power_up_audio.loop = false;
        reload_close_audio.Play();
        yield return new WaitForSeconds(0.5f);
        reload_audio.Play();
        yield return null;
    }
    
    void Update () {
        if(!PauseMenu.GameIsPaused)
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).
            IsName("gun_shooting") || 
            this.animator.GetCurrentAnimatorStateInfo(0).
            IsName("gun_reloading")) {
                animator.SetBool("Fire", false);
            } else if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (ammo <= 0) {
                    reload = true;
                } else {
                    RayCastController.shoot = true;
                    animator.SetBool("Fire", true);
                    fire_audio.Play(0);
                    ammo--;
                }
            } else {
                animator.SetBool("Fire", false);
            }
        }

        if ((Input.GetKeyDown(KeyCode.R) || reload) && ammo != 8 && !this.
          animator.GetCurrentAnimatorStateInfo(0).IsName("gun_shooting")) {
            StartCoroutine(ReloadGun());
            reload = false;
        } else {
            animator.SetBool("Reload", false);
        }

        if (ammo == 8) {
            image.sprite = battery_4_cells;
        } else if (ammo == 6) {
            image.sprite = battery_3_cells;
        } else if (ammo == 4) {
            image.sprite = battery_2_cells;
        } else if (ammo == 2) {
            image.sprite = battery_1_cells;
        } else if (ammo == 0) {
            image.sprite = battery_0_cells;
        }

        if (red_barrel && white_barrel && plastic_barrel && blue_barrel) {
            alien_beam.SetActive(true);
        }

        Text health_text = GameObject.FindWithTag("Health").
          GetComponent<Text>();
        health_text.text = "Health: " + health;

        if (health <= 0) {
            SceneManager.LoadScene("Lost");
        }
    }

}