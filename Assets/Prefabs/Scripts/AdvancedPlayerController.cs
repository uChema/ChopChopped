using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedPlayerController : MonoBehaviour
{
 
 	public float walkSpeed = 5;
 	public float turnSmoothTime = 0.2f;
 	
 	float turnSmoothVelocity;
 	float speedSmoothVelocity;
 	float currentSpeed;
 	float velocityY;
 	
 	private const float k_gravity = -9.81f;
 	
 	Transform cameraT;
 	CharacterController controller;
 
 	 void Start () 
 	{
 		cameraT = Camera.main.transform;
 		controller = GetComponent<CharacterController> ();
 	}
 
 	void Update () 
 	{
 		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
 		Vector2 inputDir = input.normalized;
 
 		Move (inputDir);
 	}
 
 	void Move(Vector2 inputDir) 
 	{
 		if (inputDir != Vector2.zero) 
 		{
 			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
 			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
 		}
 
 		float targetSpeed = walkSpeed * inputDir.magnitude; 
 		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, turnSmoothTime);
 		
 		velocityY += Time.deltaTime * k_gravity;
 		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
 
 		controller.Move (velocity * Time.deltaTime);
 	}
 	


}
