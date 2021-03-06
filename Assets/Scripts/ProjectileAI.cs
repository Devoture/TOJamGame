﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAI : MonoBehaviour {
	public float speed;
	private Transform player;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
		target = new Vector3(player.position.x, player.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
		if(transform.position.x == target.x && transform.position.y == target.y){
			DestroyProjectile();
		}		
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")){
			DestroyProjectile();
		}
	}

	void DestroyProjectile(){
		Destroy(gameObject);
	}
}
