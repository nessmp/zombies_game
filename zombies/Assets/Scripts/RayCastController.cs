using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RayCastController : MonoBehaviour {
    public GameObject bullet;
    public GameObject aim;
    public float range;

    public static bool shoot = false;
    
    private Vector3 raycast_origin;
    private Vector3 raycast_direction;
    GameObject bullets;

    void Start() {
      shoot = false;

      bullets = GameObject.FindWithTag("Bullets");
      bullet.SetActive(false);
    }
    
    void Update () {
        raycast_origin = GameObject.FindWithTag("Pistol").transform.position;
        raycast_direction = GameObject.FindWithTag("Pistol").transform.
          TransformDirection(Vector3.right);
            RaycastHit hit;
            if (Physics.Raycast(raycast_origin, raycast_direction, out hit, 
              range)) {
                aim.transform.position = hit.point;
                if (shoot) {
                  GameObject zombie_game_object = hit.transform.gameObject;
                  Zombie zombie_script = zombie_game_object.GetComponent<Zombie>();
                  if (zombie_script) {
                    zombie_script.health -= 20;
                  }
                  GameObject bullet_clone = Instantiate(bullet, hit.point, 
                    Quaternion.identity, bullets.transform);
                  bullet_clone.SetActive(true);
                  Destroy(bullet_clone, 1.5f);
                  shoot = false;
                }
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycast_origin, raycast_direction * range);
    }

}