using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float m_maxHealth = 100.0f;
	
	private float m_currHealth;

	void Start() {
		m_currHealth = m_maxHealth;
	}

	public void TakeDamage(float damage) {
		m_currHealth -= damage;
		if(m_currHealth <= 0) {
			m_currHealth = 0;
			Dead();
		}
	}

	public void Heal(float healthAmt) {
		m_currHealth += healthAmt;
		if(m_currHealth >= m_maxHealth) {
			m_currHealth = m_maxHealth;
		}
	}

	void Dead() {
		Debug.Log("dead");
	}
}
