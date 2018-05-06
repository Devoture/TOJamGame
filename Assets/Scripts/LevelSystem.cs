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
	// Use this for initialization
	void Start () {
		m_currentXP = m_maxXP;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			GainXp(100);
		}
	}

	public void GainXp(float xpPoints) {
			m_currentXP += xpPoints;

			if(m_currentXP >= m_maxXP) {
				LevelUp();
			}

			UpdateHUD();
			

		
	}

	public void LevelUp(){
		m_totalLevel++;
		m_currentXP = 0;
		UpdateHUD();
	}
	public void UpdateHUD (){
		m_leveldisplay.text ="Level: " +  m_totalLevel;
		m_XPFill.fillAmount = (float)m_currentXP / (float)m_maxXP;
	}
}
