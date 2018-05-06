using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : MonoBehaviour {

public float m_maxMana = 100.0f;

	public bool m_isPlayer;
	public Image m_manaFill;
	public float m_manaPerSecond = 5.0f;

	private float m_currentMana;
	private float m_timer = 0.0f;

	void Start() {
		m_currentMana = m_maxMana;
	}

	void Update() {
		m_timer += Time.deltaTime;
		if(m_timer >= 1) {
			m_currentMana += m_manaPerSecond;
			UpdateHUD();
			m_timer = 0;
		}
 	}

	public void UseMana(float manaCost) {
		m_currentMana -= manaCost;
		if(m_currentMana <= 0) {
			m_currentMana = 0;
		}
		if(m_isPlayer){
			UpdateHUD();
		}
	}

	public void RegainMana(float healthAmt) {
		m_currentMana += healthAmt;
		if(m_currentMana >= m_maxMana) {
			m_currentMana = m_maxMana;
		}
		if(m_isPlayer){
			UpdateHUD();
		}
	}

	void UpdateHUD() {
		m_manaFill.fillAmount = (float)m_currentMana / (float)m_maxMana;
	}

	public float GetMana() {
		return m_currentMana;
	}
}
