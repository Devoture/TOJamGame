﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float m_maxHealth = 100.0f;
	public bool m_hasBeenHit;
	
	private float m_currHealth;

	void Start() {
		m_currHealth = m_maxHealth;
	}

	public void TakeDamage(float damage) {
		if(!m_hasBeenHit) {
			m_currHealth -= damage;
			m_hasBeenHit = true;
			if(m_currHealth <= 0) {
				m_currHealth = 0;
				Dead();
			}
			Debug.Log(m_currHealth);
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
