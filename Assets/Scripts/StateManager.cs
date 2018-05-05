using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates {INTRO, MENU, PLAY}
public class StateManager : MonoBehaviour {
	public GameObject[] m_gameState;

	private GameStates m_activeState;
	private int m_numStates;
	
	void Start () {
		m_numStates = m_gameState.Length;
		for(int i = 0; i < m_numStates; i++) {
			m_gameState[i].SetActive(false);
		}
		m_activeState = GameStates.INTRO;
		m_gameState[(int)m_activeState].SetActive(true);
		GameManager.Instance.StartGame();
	}

	public void ChangeState(GameStates newState) {
		m_gameState[(int)m_activeState].SetActive(false);
		m_activeState = newState;
		m_gameState[(int)m_activeState].SetActive(true);
	}
	
	public void PlayGame() {
		GameManager.Instance.m_stateGameMenu.PlayGame();
		//ChangeState(GameStates.PLAY);
	}

	public void QuitGame() {
		GameManager.Instance.m_stateGameMenu.QuitGame();
	}
}
