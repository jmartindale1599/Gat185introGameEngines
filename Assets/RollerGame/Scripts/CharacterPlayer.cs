using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]


public class CharacterPlayer : MonoBehaviour{
    
	[SerializeField] private PlayerData playerData;
	
	[SerializeField] private Animator animator;

	[SerializeField] private InputRouter inputRouter;

    CharacterController characterController;

	Camera mainCamera;

	Vector2 inputAxis;

	Vector3 velocity = Vector3.zero;

	float airtime = 0;

    void Start(){
        
        characterController = GetComponent<CharacterController>();

		mainCamera = Camera.main;

		inputRouter.jumpEvent += OnJump;

		inputRouter.moveEvent += OnMove;
		
		inputRouter.fireEvent += OnFire;
		
		inputRouter.fireStopEvent += OnFireStop;

	}

	public void OnJump(){

		if (characterController.isGrounded == true){

			animator.SetTrigger("Jump");

			velocity.y = Mathf.Sqrt(playerData.jumpHeight * -3 * playerData.gravity);

		}

	}

	public void OnFire(){

	}

	public void OnFireStop(){

	}

	public void OnMove(Vector2 axis){

		inputAxis = axis;
	
	}

	void Update(){

        Vector3 direction = Vector3.zero;

        direction.x = inputAxis.x;

        direction.z = inputAxis.y;

		direction = mainCamera.transform.TransformDirection(direction);

		if (characterController.isGrounded){ 
		
			velocity.x = direction.x * playerData.speed;
		
			velocity.z = direction.z * playerData.speed;

			airtime = 0;

		}else{

			airtime += Time.deltaTime;

			velocity.y += playerData.gravity * Time.deltaTime;

		}

        characterController.Move(velocity * Time.deltaTime);

		Vector3 look = direction;

		look.y = 0;

		if(look.magnitude > 0) { transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), playerData.turnRate * Time.deltaTime); }

		//set animator

        animator.SetFloat("Speed", characterController.velocity.magnitude);
        
		animator.SetFloat("velY", characterController.velocity.y);

		animator.SetFloat("airtime", airtime);

		animator.SetBool("IsGrounded", characterController.isGrounded);

    }

	void OnControllerColliderHit(ControllerColliderHit hit){

		Rigidbody body = hit.collider.attachedRigidbody;

		// no rigidbody
		
		if (body == null || body.isKinematic){

			return;
		
		}

		// We dont want to push objects below us
		
		if (hit.moveDirection.y < -0.3){

			return;

		}

		// Calculate push direction from move direction,
		
		// we only push objects to the sides never up and down
		
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

		// If you know how fast your character is trying to move,
		
		// then you can also multiply the push velocity by that.

		// Apply the push
		
		body.velocity = pushDir * playerData.hitForce;

	}

	public void onJump(InputAction.CallbackContext context){

		if (context.performed){

			Debug.Log("Jump");

		}

	}

	public void onLeftFootSpawn(GameObject go){

		Debug.Log("left");

		Transform bone = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
	
		Instantiate(go, bone.position, bone.rotation);

	}

	public void onRightFootSpawn(GameObject go){
        
		Debug.Log("right");
        
		Transform bone = animator.GetBoneTransform(HumanBodyBones.RightFoot);
	
		Instantiate(go, bone.position, bone.rotation);

	}

}
