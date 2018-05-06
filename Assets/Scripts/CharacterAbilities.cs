using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAbilities : MonoBehaviour {

	public GameObject m_fireballPrefab;
	public Transform m_fireballSpawnPoint;
	public float m_fireballSpeed = 5.0f;
	public float m_heroicLeepSpeed = 5.0f;
	public float m_pullRadius = 10.0f;
	public float m_pullForce = 200.0f;
	public float m_pushForce = 100.0f;
	public bool m_isBlackhole = false;
	public float m_rotationSpeed = 5.0f;
	public Collider m_swordCollider;

	private bool m_heroicLeeping = false;
	private NavMeshAgent agent;
	private Animator m_animator;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		m_animator = GetComponent<Animator>();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Q)) {
			m_animator.SetTrigger("spellCast");
		}

		if(Input.GetKeyDown(KeyCode.W)) {
			Blink();
		}

		if(Input.GetKeyDown(KeyCode.E)) {
			m_animator.SetTrigger("hLeap");
		}

		if(Input.GetKeyDown(KeyCode.R)) {
			Blackhole();
		}

		if(Input.GetMouseButtonDown(1)) {
			AutoAttack();
		}
	}

	public void FixedUpdate() {
		if(m_isBlackhole) {
			foreach (Collider collider in Physics.OverlapSphere(transform.position, m_pullRadius)) {
				if(collider.gameObject.tag == "Enemy") {
					Vector3 forceDirection = transform.position - collider.transform.position;
					collider.GetComponent<Rigidbody>().AddForce(forceDirection * m_pullForce * Time.fixedDeltaTime);
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.F)) {
			m_isBlackhole = false;
			foreach (Collider collider in Physics.OverlapSphere(transform.position, m_pullRadius)) {
				if(collider.gameObject.tag == "Enemy") {
					Vector3 forceDirection = transform.position + collider.transform.position;
					collider.GetComponent<Rigidbody>().AddForce(forceDirection * m_pushForce * Time.fixedDeltaTime);
				}
			}
		}
	}

	public void FireBall() {
		agent.isStopped = true;
		m_animator.SetBool("isMoving", false);
		transform.LookAt(GetMousePos());
		agent.destination = GetMousePos();
		GameObject fireball = Instantiate(m_fireballPrefab, m_fireballSpawnPoint.position, Quaternion.identity);
		fireball.transform.LookAt(GetMousePos());
		fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * m_fireballSpeed;
	}

	public void Blink() {
		agent.isStopped = true;
		m_animator.SetBool("isMoving", false);
		transform.LookAt(GetMousePos());
		transform.position = GetMousePos();
		agent.destination = GetMousePos();
	}

	public void HeroicLeap() {
		agent.isStopped = true;
		m_animator.SetBool("isMoving", false);
		transform.position = GetMousePos();
	}

	public void Blackhole() {
		m_isBlackhole = true;
	}

	public void AutoAttack() {
		m_animator.SetBool("isAuto", true);
		m_swordCollider.enabled = true;
	}

	public void StopAuto() {
		m_animator.SetBool("isAuto", false);
		m_swordCollider.enabled = false;
		foreach(GameObject enemy in m_swordCollider.GetComponent<SwordCollider>().m_enemiesHit) {
			enemy.GetComponent<Health>().m_hasBeenHit = false;
			Debug.Log(enemy.name);
			Debug.Log(m_swordCollider.GetComponent<SwordCollider>().m_enemiesHit.Count);
		}
		
		for(int i = m_swordCollider.GetComponent<SwordCollider>().m_enemiesHit.Count; i > 0; i--) {
			m_swordCollider.GetComponent<SwordCollider>().m_enemiesHit.RemoveAt(0);
		}
	}

	Vector3 GetMousePos() {
		RaycastHit hit;
		Vector3 mousePos = Vector3.zero;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
			mousePos = hit.point;
		}
		return mousePos;
	}
}
