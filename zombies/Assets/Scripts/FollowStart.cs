using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class FollowStart : MonoBehaviour {
	public Transform[] destination; 
	public NavMeshAgent navmeshAgent;
	Zombie zombie;
    
    private int randomNumber;

	// Use this for initialization
	void Start () {
		navmeshAgent=this.GetComponent<NavMeshAgent>();
		zombie = this.GetComponent<Zombie>();
        Random.Range(0, destination.Length);
	}

	// Update is called once per frame
	void Update () {
		//Para un target objeto (Ethan)
        if(EuclideanDistance(zombie.transform, destination[randomNumber]) < 2.1 ){
            randomNumber = Random.Range(0, destination.Length);
        }
		if (!zombie.dead) {
			navmeshAgent.SetDestination (destination[randomNumber].position);
		}
	}
   
    private double EuclideanDistance(Transform t1, Transform t2){
        return Mathf.Sqrt(Mathf.Pow(2, t1.position.x - t2.position.x) + 
               Mathf.Pow(2, t1.position.z - t2.position.z) );
    }
}
