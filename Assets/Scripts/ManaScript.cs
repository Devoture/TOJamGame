using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : MonoBehaviour {

public float m_maxMana = 100.0f;

	public bool m_isPlayer;

	public Image m_manaFill;
	

	
	private float m_currentMana;


	void Start() {
		m_currentMana = m_maxMana;
	}

	public void UseMana(float manaCost) {
			m_currentMana -= manaCost;

			if(m_currentMana <= 0) {

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

	void Dead() {
	}

	void UpdateHUD() {
		m_manaFill.fillAmount = (float)m_currentMana / (float)m_maxMana;
	}

	public float GetMana() {
		return m_currentMana;
	}
}
