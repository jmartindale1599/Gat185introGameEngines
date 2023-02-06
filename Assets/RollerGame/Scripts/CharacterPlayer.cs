using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class CharacterPlayer : MonoBehaviour{

    [SerializeField] private float speed = 3;

    [SerializeField] private float hitForce = 2;

	[SerializeField] private float gravity = Physics.gravity.y;

	[SerializeField] private float turnRate = 10;

	[SerializeField] private float jumpHeight = 2.5f;

    CharacterController characterController;

	PlayerInputActions playerInput;

	Camera mainCamera;

	Vector3 velocity = Vector3.zero;

    public void OnEnable(){
        
		playerInput.Enable();

    }

    public void Awake(){

		playerInput = new PlayerInputActions();

    }

    public void OnDisable(){
        
		playerInput.Disable();

    }

    void Start(){
        
        characterController = GetComponent<CharacterController>();

		mainCamera = Camera.main;

    }

    void Update(){

        Vector3 direction = Vector3.zero;

		Vector2 axis = playerInput.Player.Move.ReadValue<Vector2>();

        direction.x = axis.x;

        direction.z = axis.y;

		direction = mainCamera.transform.TransformDirection(direction);

		if (characterController.isGrounded){ 
		
			velocity.x = direction.x * speed;
		
			velocity.z = direction.z * speed;

			if (playerInput.Player.Jump.triggered){ 
			
				velocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
			
			}

		}else{

			velocity.y += gravity * Time.deltaTime;

		}

        characterController.Move(velocity * Time.deltaTime);

		Vector3 look = direction;

		look.y = 0;

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), turnRate * Time.deltaTime);

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
		
		body.velocity = pushDir * hitForce;

	}

	public void onJump(InputAction.CallbackContext context){

		if (context.performed){

			Debug.Log("Jump");

		}

	}

}
