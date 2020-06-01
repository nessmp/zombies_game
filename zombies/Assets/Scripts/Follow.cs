using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Follow : MonoBehaviour {
	public Transform destination; 
	public NavMeshAgent navmeshAgent;
	Zombie zombie;

	// Use this for initialization
	void Start () {
		navmeshAgent=this.GetComponent<NavMeshAgent>();
		zombie = this.GetComponent<Zombie>();
	}

	// Update is called once per frame
	void Update () {
		//Para un target objeto (Ethan)
		if (!zombie.dead) {
			SetDestination();
		}
	}

   
	private void SetDestination() {
		if (destination != null) {
			NavMeshHit hit;
			if (NavMesh.SamplePosition(destination.transform.position, 
			  out hit, 3.0f, NavMesh.AllAreas)) {
				Vector3 targetVector = hit.position;
				navmeshAgent.SetDestination (targetVector);
			}
		}
	}
}
