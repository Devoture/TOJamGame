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
	public Collider m_swordCollider;
	public float m_damage;
	public bool m_dashAttack;
	public float m_dashTimeLength = 1.0f;
	public float m_dashSpeed = 40.0f;
	public float m_normalSpeed;
	public float m_blackholeDamage = 100;

	private bool m_heroicLeeping = false;
	private NavMeshAgent agent;
	private Animator m_animator;
	private Rigidbody m_rb;
	private ClicktoMove m_movementScript;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		m_animator = GetComponent<Animator>();
		m_rb = GetComponent<Rigidbody>();
		m_movementScript = GetComponent<ClicktoMove>();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Q)) {
			m_animator.SetTrigger("spellCast");
		}

		if(Input.GetKeyDown(KeyCode.W)) {
			Blink();
		}

		if(Input.GetKeyDown(KeyCode.E)) {
			DashAttack();
		}

		if(Input.GetKeyDown(KeyCode.R)) {
			Blackhole();
		}

		if(Input.GetMouseButtonDown(1)) {
			AutoAttack();
		}

		if(m_dashAttack) {
			agent.speed = m_dashSpeed;
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
					collider.GetComponent<Health>().TakeDamage(m_blackholeDamage);
				}
			}
		}
	}

	public void FireBall() {
		agent.isStopped = true;
		transform.LookAt(GetMousePos());
		m_animator.SetBool("isMoving", false);
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

	public void DashAttack() {
		m_movementScript.m_disableMovement = true;
		transform.LookAt(GetMousePos());
		agent.destination = GetMousePos();
		m_dashAttack = true;
		StartCoroutine(DashTimer(m_dashTimeLength));
		m_damage = 100.0f;
		m_animator.SetBool("isDashing", true);
		m_swordCollider.enabled = true;
	}

	public void DashEnd() {
		m_movementScript.m_disableMovement = false;
		agent.speed = m_normalSpeed;
		m_dashAttack = false;
		m_animator.SetBool("isDashing", false);
		m_swordCollider.enabled = false;
		foreach(GameObject enemy in m_swordCollider.GetComponent<SwordCollider>().m_enemiesHit) {
			enemy.GetComponent<Health>().m_hasBeenHit = false;
		}
		
		for(int i = m_swordCollider.GetComponent<SwordCollider>().m_enemiesHit.Count; i > 0; i--) {
			m_swordCollider.GetComponent<SwordCollider>().m_enemiesHit.RemoveAt(0);
		}
	}

	public void AutoAttack() {
		m_damage = 50.0f;
		m_animator.SetBool("isAuto", true);
		m_swordCollider.enabled = true;
	}

	public void StopAuto() {
		m_animator.SetBool("isAuto", false);
		m_swordCollider.enabled = false;
		foreach(GameObject enemy in m_swordCollider.GetComponent<SwordCollider>().m_enemiesHit) {
			enemy.GetComponent<Health>().m_hasBeenHit = false;
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

	private IEnumerator DashTimer(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		DashEnd();
    }
}
