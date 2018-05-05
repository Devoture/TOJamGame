using UnityEngine;
using System.Collections;
 
public class ClicktoMove : MonoBehaviour {
	private Transform myTransform;				
	private Vector3 destinationPosition;		
	private float destinationDistance;			
	private Animator m_anim;
 
	public float moveSpeed;						
 
 
 
	void Start () {
		myTransform = transform;							// sets myTransform to this GameObject.transform
		destinationPosition = myTransform.position;			// prevents myTransform reset
		m_anim = GetComponent<Animator>();
	}
 
	void Update () {
 
		// keep track of the distance between this gameObject and destinationPosition
		destinationDistance = Vector3.Distance(destinationPosition, myTransform.position);
 
		if(destinationDistance < .5f){		// To prevent shakin behavior when near destination
			moveSpeed = 0;
			m_anim.SetBool("isMoving", false);
		}
		else if(destinationDistance > .5f){			// To Reset Speed to default
			moveSpeed = 3;
			m_anim.SetBool("isMoving", true);
		}
 
 
		// Moves the Player if the Left Mouse Button was clicked
		if (Input.GetMouseButtonDown(0)&& GUIUtility.hotControl ==0) {
 
			Plane playerPlane = new Plane(Vector3.up, myTransform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;
 
			if (playerPlane.Raycast(ray, out hitdist)) {
				Vector3 targetPoint = ray.GetPoint(hitdist);
				destinationPosition = ray.GetPoint(hitdist);
				Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
				myTransform.rotation = targetRotation;
			}
		}
 
		// To prevent code from running if not needed
		if(destinationDistance > .5f){
			myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
		}
	}
}