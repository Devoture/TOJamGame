using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour {

	public float m_fireballDamage = 50.0f;
	public float m_radius = 10.0f;
	public float m_pushForce = 10.0f;

	private bool m_hitEnemy;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Enemy") {
			m_hitEnemy = true;
		}
	}

	void FixedUpdate() {
		if(m_hitEnemy) {
			foreach (Collider collider in Physics.OverlapSphere(transform.position, m_radius)) {
				if(collider.tag == "Enemy") {
					Vector3 forceDirection = transform.position + collider.transform.position;
					collider.GetComponent<Rigidbody>().AddForce(forceDirection * m_pushForce * Time.fixedDeltaTime);
					collider.GetComponent<Health>().TakeDamage(m_fireballDamage);
				}
			}
			Destroy(this.gameObject);
		}
	}
}
