using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollision : MonoBehaviour {
	public QuestManager m_qmgr;
	public ClicktoMove m_playMovement;
	public GameObject m_HUD;
	public string m_questTitle;
	public string m_questContent;
	public int m_questExp;

	public GameObject npcAvail;
	public GameObject pending;
	public GameObject complete;
	public bool accepted;

	void OnTriggerEnter(Collider other){
		if(!accepted){
			m_qmgr.SetQuestText(m_questTitle,m_questContent,m_questExp.ToString());
			m_qmgr.m_questCanvas.SetActive(true);
			m_HUD.SetActive(false);
		}

		//m_playMovement.enabled = false;
	}
	void OnTriggerExit(Collider other){
		m_qmgr.m_questCanvas.SetActive(false);
		m_HUD.SetActive(true);
		//m_playMovement.enabled = true;
	}
	
	public void AcceptQuest(){
		accepted = true;
		m_qmgr.m_questCanvas.SetActive(false);
		m_HUD.SetActive(true);
		m_qmgr.ChangeQuestIndicator(npcAvail,pending,complete,false);
	}

}
