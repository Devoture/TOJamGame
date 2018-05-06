using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour {

	public CharacterAbilities m_abilitiesScript;
	public List<GameObject> m_enemiesHit;
	public int m_enemyIndex = 0;

	void Start() {
		m_enemiesHit = new List<GameObject>();
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Enemy" && !other.GetComponent<Health>().m_hasBeenHit) {
			Health health = other.GetComponent<Health>();
			health.TakeDamage(m_abilitiesScript.m_damage);
			m_enemiesHit.Add(other.gameObject);
			m_enemyIndex++;
		}
	}
}
