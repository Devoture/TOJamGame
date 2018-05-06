using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour {
	public float m_maxXP = 1000.0f;
	public Image m_XPFill;	
	public Text m_leveldisplay;
	private float m_currentXP;
	private float m_totalLevel;
	private float m_tmpXp;
	public GameObject m_SkillTree;
	
	void Start () {
		m_currentXP = m_maxXP;
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			GainXp(311);
		}
	}

	public void GainXp(float xpPoints) {
		m_currentXP += xpPoints;

		if(m_currentXP >= m_maxXP) {
			m_tmpXp = m_currentXP - m_maxXP;
			LevelUp();
		}
		UpdateHUD();
	}

	public void LevelUp(){
		m_totalLevel++;
		m_currentXP = m_tmpXp;
		UpdateHUD();
		// Time.timeScale = 0;
        // m_SkillTree.SetActive(true);
	}
	public void UpdateHUD (){
		m_leveldisplay.text ="Level: " +  m_totalLevel;
		m_XPFill.fillAmount = (float)m_currentXP / (float)m_maxXP;
	}
}
