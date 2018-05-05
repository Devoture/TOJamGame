using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson {
	public class basicAI : MonoBehaviour {
		public NavMeshAgent agent;
		public ThirdPersonCharacter character;

		public enum State {
			PATROL,
			CHASE
		}

		public State state;
		public bool alive;
		
		//variables for patrolling
		public GameObject[] waypoints;
		private int waypointInd = 0;
		public float patrolSpeed = 0.5f;

		//variables for chasing
		public float chaseSpeed = 1f;
		public GameObject target;
		void Start () {
			agent = GetComponent<NavMeshAgent>();
			character = GetComponent<ThirdPersonCharacter>();

			agent.updatePosition = true;
			agent.updateRotation = false;

			state = basicAI.State.PATROL;
			alive = true;
			
			StartCoroutine("FSM");
		}

		IEnumerator FSM() {
			while(alive) {
				switch(state) {
					case State.PATROL:
						Patrol();
						break;
					case State.CHASE:
						Chase();
						break;
				}
				yield return null;
			}

		}
	
		void Patrol(){
			agent.speed = patrolSpeed;
			if(Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2){
				agent.SetDestination(waypoints[waypointInd].transform.position);
				character.Move(agent.desiredVelocity, false, false);
			}else if(Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2 ){
				waypointInd +=1;
				if(waypointInd > waypoints.Length) {
					waypointInd = 0;
				}
			}else {
				character.Move(Vector3.zero, false, false);
			}


			
		}
		void Chase(){
			agent.speed = chaseSpeed;
			agent.SetDestination(target.transform.position);
			character.Move(agent.desiredVelocity, false, false);

		}

		void OnTriggerEnter(Collider coll){
			if(coll.tag == "Player") {
				state = basicAI.State.CHASE;
				target = coll.gameObject;
			}

		}
		
	}

}



