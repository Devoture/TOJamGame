using UnityEngine;
using System.Collections;
using UnityEngine.AI; 
public class ClicktoMove : MonoBehaviour {

	public float m_rotationSpeed = 5.0f;

	private NavMeshAgent agent;
	private Animator m_animator;
	
	void Start() {
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		m_animator = GetComponent<Animator>();
	}
	
	void Update() {
		if (Input.GetMouseButton(0)) {
			agent.isStopped = false;
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				agent.destination = hit.point;
				m_animator.SetBool("isMoving", true);
			}
		}
		if(!agent.hasPath) {
			m_animator.SetBool("isMoving", false);
		}
		InstantlyTurn(agent.destination);
	}

	private void InstantlyTurn(Vector3 destination) {
		if ((destination - transform.position).magnitude < 0.1f) 
			return; 

		Vector3 direction = (destination - transform.position).normalized;
		Quaternion qDir= Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * m_rotationSpeed);
	}
}