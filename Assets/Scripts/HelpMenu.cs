using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour {

	public GameObject m_helpCanvas;
	public GameObject m_menuCanvas;

	void Start() {
		CloseHelp();
	}

	public void Help() {
		m_helpCanvas.SetActive(true);
		m_menuCanvas.SetActive(false);

	}

	public void CloseHelp() {
		m_helpCanvas.SetActive(false);
		m_menuCanvas.SetActive(true);
	}
}
