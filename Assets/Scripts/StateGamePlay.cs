using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGamePlay : GameState {

	public StateGamePlay(GameManager gm):base(gm) { }

	private bool m_isPaused = false;

	public override void Enter() { }
	public override void Execute() { 
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(m_isPaused) {
				ResumeGame();
			} else {
				PauseGame();
			}
		}
	}
	public override void Exit() { }

	public void PauseGame() {
		Time.timeScale = 0.0f;
		m_isPaused = true;
	}

	public void ResumeGame() {
		Time.timeScale = 1.0f;
		m_isPaused = false;
	}
}