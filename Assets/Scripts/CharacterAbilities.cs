using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour {

	public GameObject m_fireballPrefab;
	public Transform m_fireballSpawnPoint;
	public float m_fireballSpeed = 5.0f;

	void Start() {
		
	}

	void Update() {
		if(Input.GetMouseButtonDown(1)) {
			FireBall();
		}
	}

	void FireBall() {
		GameObject fireball = Instantiate(m_fireballPrefab, m_fireballSpawnPoint.position, Quaternion.identity);
		var mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		fireball.transform.LookAt(new Vector3(mousePos.x, 0, 0));
		fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * m_fireballSpeed;
	}
}
