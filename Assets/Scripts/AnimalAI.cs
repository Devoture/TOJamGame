using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour {
	public NavMeshAgent agent;

	public enum State {
		HERDING,
		RUNNING
	}

	public State state;
	public bool alive;
	public GameObject[] waypoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
