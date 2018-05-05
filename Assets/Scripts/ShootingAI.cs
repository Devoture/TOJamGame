using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAI : MonoBehaviour {
	public float speed;
	public float stoppingDistance;
	public float retreatDistance;
	public Transform player;
	private float timeBetweenShots;
	public float startTimeBetweenShots;
	public GameObject projectile;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
		timeBetweenShots = startTimeBetweenShots;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, player.position) > stoppingDistance) {
			transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		} else if(Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) < retreatDistance){
			transform.position = this.transform.position;

		} else if(Vector3.Distance(transform.position, player.position) < retreatDistance){
			transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
		}
		
		if(timeBetweenShots <= 0){
			Instantiate(projectile, transform.position, Quaternion.identity);
			timeBetweenShots = startTimeBetweenShots;
		} else {
			timeBetweenShots -= Time.deltaTime;
		}
	}
}
