using UnityEngine;
using System.Collections;
using UnityEngine.AI; 
public class ClicktoMove : MonoBehaviour {
	NavMeshAgent agent;
	bool hasMoved = false;
	private Animator m_animator;
	
	void Start() {
		agent = GetComponent<NavMeshAgent>();
		m_animator = GetComponent<Animator>();
	}
	
	void Update() {
		if (Input.GetMouseButton(0)) {
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				agent.destination = hit.point;
				m_animator.SetBool("isMoving", true);
				hasMoved = true;	
			}
		}
		if(agent.velocity == Vector3.zero && hasMoved) {
			Debug.Log("arrived");
			m_animator.SetBool("isMoving", false);
		}
	}
}