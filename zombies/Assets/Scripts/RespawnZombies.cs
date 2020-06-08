using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnZombies : MonoBehaviour {
    public GameObject original_zombie;

    int round = 1;
    int spawned_zombies = 0;
    const float seconds_between_rounds = 30.0f;
    float timer = 0.0f;

    GameObject respawn;

    void Start() {
        respawn = GameObject.FindWithTag("Respawn");
        original_zombie.SetActive(false);
        PlayRound(round);
    }

    void InstantiateZombie(int n_zombies) {
        GameObject player = GameObject.FindWithTag("Player");
        // Instantiate num_of_rounds zombies
        for (int i = 0; i < n_zombies; i++) {
            Vector3 new_zombie_position;
            int direction = Random.Range(1, 4);
            if (direction == 1) {
                new_zombie_position = Vector3.back;
            } else if (direction == 2) {
                new_zombie_position = Vector3.left;
            } else if (direction == 3) {
                new_zombie_position = Vector3.forward;
            } else {
                new_zombie_position = Vector3.right;
            }
            GameObject zombie_clone = Instantiate(original_zombie,
              player.transform.position + (new_zombie_position * 
              Random.Range(10.0f, 20.0f)), player.transform.rotation, 
                respawn.transform);
            zombie_clone.SetActive(true);
        }
    }

    void PlayRound(int num_of_round) {
        if (spawned_zombies - GunAnimations.kill_count + num_of_round < 20) {
            InstantiateZombie(num_of_round);
            spawned_zombies += num_of_round;
        } else {
            int zombies_left = 20 - (spawned_zombies - GunAnimations.kill_count);
            InstantiateZombie(zombies_left);
            if (zombies_left > 0) {
                spawned_zombies += zombies_left;
            }
        }
        timer = 0.0f;
    }

    void GetRound() {
        int actual_round = 0;
        round = 0;
        for (int i = 1; i <= spawned_zombies; i++) {
            actual_round += i;
            if (actual_round <= spawned_zombies) {
                round++;
            } else {
                break;
            }
        }
    }

    void Update() {
        foreach (Transform child in respawn.transform) {
            GameObject player = GameObject.FindWithTag("Player");
            if (Vector3.Distance(player.transform.position, child.position) > 
              50) {
                Destroy(child.gameObject);
                InstantiateZombie(1);
            }
        }
        timer += Time.deltaTime;
        if (timer >= seconds_between_rounds || 
          spawned_zombies == GunAnimations.kill_count) {
            PlayRound(round + 1);
            GetRound();
            Text round_text = GameObject.FindWithTag("Round").
              GetComponent<Text>();
            round_text.text = "#" + round;
        }
    }
}
