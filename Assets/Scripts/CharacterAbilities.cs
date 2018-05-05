using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour {

	public GameObject m_fireballPrefab;
	public Transform m_fireballSpawnPoint;
	public float m_fireballSpeed = 5.0f;
	public float m_heroicLeepSpeed = 5.0f;
	public float m_pullRadius = 10.0f;
	public float m_pullForce = 200.0f;
	public float m_pushForce = 100.0f;
	public bool m_isBlackhole = false;

	private bool m_heroicLeeping = false;

	void Start() {
		
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Q)) {
			FireBall();
		}

		if(Input.GetKeyDown(KeyCode.W)) {
			Blink();
		}

		if(Input.GetKeyDown(KeyCode.E)) {
			HeroicLeap();
		}

		if(Input.GetKeyDown(KeyCode.R)) {
			Blackhole();
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

	void FireBall() {
		GameObject fireball = Instantiate(m_fireballPrefab, m_fireballSpawnPoint.position, Quaternion.identity);
		fireball.transform.LookAt(GetMousePos());
		fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * m_fireballSpeed;
	}

	void Blink() {
		transform.position = GetMousePos();
	}

	void HeroicLeap() {
		transform.position = GetMousePos();
	}

	void Blackhole() {
		m_isBlackhole = true;
	}

	Vector3 GetMousePos() {
		Plane zeroPlane = new Plane(Vector3.up, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;
		Vector3 outputPosition = Vector3.zero;;
         
        if(zeroPlane.Raycast(ray, out distance)) {
             outputPosition = ray.origin + ray.direction * distance;
		}
		return outputPosition;
	}
}
