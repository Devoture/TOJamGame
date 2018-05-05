using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

	public Transform target;

	public float distance = 5.0f;
	public float maxDistance = 7.0f;
	public float minDistance = 1.0f;
	public Vector3 offset;
	public float smoothSpeed = 5.0f;
	public float scrollSensitivity = 1.0f;

	
	// Update is called once per frame
	void LateUpdate () {
		if(!target){
			return;
		}
		float num = Input.GetAxis("Mouse ScrollWheel");
		distance -= num * scrollSensitivity;
		distance = Mathf.Clamp(distance, minDistance,maxDistance);

		Vector3 pos = target.position + offset;
		pos -= transform.forward * distance;

		transform.position = Vector3.Lerp(transform.position,pos, smoothSpeed * Time.deltaTime);


		transform.LookAt(target);


	}
}
