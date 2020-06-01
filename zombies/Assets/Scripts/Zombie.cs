using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    public AudioSource walking_audio;
    public AudioSource attacking_audio;
    public int health = 100;
    public bool dead = false;

    private bool on_player = false;
    Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(PlayWalking());
    }

    IEnumerator PlayWalking() {
        while (true){
            if (!attacking_audio.isPlaying && !dead){
                walking_audio.Play();  
            }
            // Don't move! Unity will stop responding if this instruction 
            // isn't call on every cycle of this while!
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
        }
    }

    void AttackFinished() {
        if (on_player) {
            GunAnimations.health -= 20;
        }
    }

    private IEnumerator FadeOut() {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    void Dissappear() {
        GunAnimations.kill_count++;
        StartCoroutine(FadeOut());
    }

    void Update() {
        if (health <= 0) {
            dead = true;
            animator.SetBool("Die", true);
            Collider[] colliders = GetComponents<Collider>();

            for (int i = 0; i < colliders.Length; i++) {
                colliders[i].enabled = false;
            }
        }
        if (on_player && !attacking_audio.isPlaying){
            attacking_audio.Play();  
        }
        animator.SetBool("Attacking", on_player);
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            on_player = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            on_player = false;
        }
    }
}
