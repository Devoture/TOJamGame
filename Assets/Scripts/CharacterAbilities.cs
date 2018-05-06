using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
	public float m_blackholeDamage = 100.0f;
	public float m_fireballMana = 10.0f;
	public float m_blackholeMana = 40.0f;
	public float m_blinkMana = 20.0f;
	public float m_dashStrikeMana = 10.0f;
	public float m_knockBackTimer = 2.0f;
	public float m_blackholeCD = 5.0f;
	public float m_fireballCD = 1.0f;
	public float m_dashStrikeCD = 2.0f;
	public float m_blinkCD = 4.0f;
	public bool m_canBlink = true;
	public bool m_canDash = true;
	public bool m_canBlackhole = true;
	public bool m_canFireball = true;
	public Image  m_fireballMask;
	public Image  m_dashMask;
	public Image  m_blinkmask;
	public Image  m_blackHoleMask;


	private bool m_heroicLeeping = false;
	private NavMeshAgent agent;
	private Animator m_animator;
	private Rigidbody m_rb;
	private ClicktoMove m_movementScript;
	private ManaScript m_manaScript;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		m_animator = GetComponent<Animator>();
		m_rb = GetComponent<Rigidbody>();
		m_movementScript = GetComponent<ClicktoMove>();
		m_manaScript = GetComponent<ManaScript>();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.W) && m_canFireball) {
			if(m_manaScript.GetMana() >= m_fireballMana) {
				m_animator.SetTrigger("spellCast");
				m_canFireball = false;
				StartCoroutine(FireballCD(m_fireballCD));
			}
		}

		if(Input.GetKeyDown(KeyCode.E) && m_canBlink) {
			Blink();
		}

		if(Input.GetKeyDown(KeyCode.Q) && m_canDash) {
			DashAttack();
		}

		if(Input.GetKeyDown(KeyCode.R) && m_canBlackhole) {
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
	}

	public void FireBall() {
		// doesnt need mana check here its done on key press
		m_manaScript.UseMana(m_fireballMana);
		agent.isStopped = true;
		transform.LookAt(GetMousePos());
		m_animator.SetBool("isMoving", false);
		agent.destination = GetMousePos();
		GameObject fireball = Instantiate(m_fireballPrefab, m_fireballSpawnPoint.position, Quaternion.identity);
		fireball.transform.LookAt(GetMousePos());
		fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * m_fireballSpeed;
	}

	public void Blink() {
		if(m_manaScript.GetMana() >= m_blinkMana) {
			m_canBlink = false;
			StartCoroutine(BlinkCD(m_blinkCD));
			m_manaScript.UseMana(m_blinkMana);
			agent.isStopped = true;
			m_animator.SetBool("isMoving", false);
			transform.LookAt(GetMousePos());
			transform.position = GetMousePos();
			agent.destination = GetMousePos();
		}
	}

	public void HeroicLeap() {
		agent.isStopped = true;
		m_animator.SetBool("isMoving", false);
		transform.position = GetMousePos();
	}

	public void Blackhole() {
		if(m_manaScript.GetMana() >= m_blackholeMana) {
			StartCoroutine(BlackholeCD(m_blackholeCD));
			m_canBlackhole = false;
			m_manaScript.UseMana(m_blackholeMana);
			m_isBlackhole = true;
			StartCoroutine(BlackholeKnockbackTimer(m_knockBackTimer));
		}
	}

	public void DashAttack() {
		if(m_manaScript.GetMana() >= m_dashStrikeMana) {
			m_canDash = false;
			StartCoroutine(DashCD(m_dashStrikeCD));
			m_manaScript.UseMana(m_dashStrikeMana);
			agent.isStopped = false;
			m_movementScript.m_disableMovement = true;
			transform.LookAt(GetMousePos());
			agent.destination = GetMousePos();
			m_dashAttack = true;
			StartCoroutine(DashTimer(m_dashTimeLength));
			m_damage = 100.0f;
			m_animator.SetBool("isDashing", true);
			m_swordCollider.enabled = true;
		}
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

	public void BlackholeKnockback() {
		foreach (Collider collider in Physics.OverlapSphere(transform.position, m_pullRadius)) {
			if(collider.gameObject.tag == "Enemy") {
				Vector3 forceDirection = transform.position + collider.transform.position;
				collider.GetComponent<Rigidbody>().AddForce(forceDirection * m_pushForce * Time.fixedDeltaTime);
				collider.GetComponent<Health>().TakeDamage(m_blackholeDamage);
			}
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

	private IEnumerator BlackholeKnockbackTimer(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		m_isBlackhole = false;
		m_animator.SetTrigger("afterBlackhole");
    }

	private IEnumerator BlackholeCD(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		m_canBlackhole = true;
    }

	private IEnumerator FireballCD(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		m_canFireball = true;
    }

	private IEnumerator DashCD(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		m_canDash = true;
    }

	private IEnumerator BlinkCD(float waitTime) {
		// while(!m_canBlink){
		// 	m_fireballMask.fillAmount =0;
		// }
		yield return new WaitForSeconds(waitTime);
		m_canBlink = true;
    }
}
