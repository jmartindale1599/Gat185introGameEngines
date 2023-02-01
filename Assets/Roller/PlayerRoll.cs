using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerRoll : MonoBehaviour{

    [SerializeField] private Transform view;

    [SerializeField] private float groundRayLength = 1.8f;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField, Range(8, 50)] private float maxForce = 20;

	private int score = 0;

	private Vector3 force;

    private Rigidbody rb;

    public bool gravSwitch = false;

    public bool didWin = false;

    void Start(){

        rb = GetComponent<Rigidbody>();

        view = Camera.main.transform;

        Camera.main.GetComponent<RollerCamera>().setTarget(transform);

        GetComponent<Health>().onDamage += OnDamage;

        GetComponent<Health>().onDeath += OnDeath;

        GetComponent<Health>().onHeal += OnHeal;

		GameManager.Instance.setHealth((int)GetComponent<Health>().health);

	}

	// Update is called once per frame
	void Update(){

        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");

        direction.z = Input.GetAxis("Vertical");

        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);

        force = viewSpace * (direction * maxForce);

		Ray ray = new Ray(transform.position, Vector3.down);

		bool onGround = Physics.Raycast(ray, groundRayLength, groundLayer);
		
        Debug.DrawRay(transform.position, ray.direction * groundRayLength);

		if (onGround && Input.GetButtonDown("Jump")){

            rb.AddForce(Vector3.up * 7, ForceMode.Impulse);

        }

		if (gravSwitch == true){

			rb.AddForce(Vector3.up * 15, ForceMode.Force);

		}

		if (Input.GetButtonDown("Fire1")){//respawn button

            this.transform.position = Vector3.up;

        }

    }

	private void LateUpdate(){
		
        if(gravSwitch == true && Input.GetButtonDown("Jump")){

			rb.AddForce(Vector3.up * -7, ForceMode.Impulse);

            view.GetComponent<RollerCamera>().pitch = -180.0f;

        }else{

			view.GetComponent<RollerCamera>().pitch = 180.0f;

		}

        if (didWin == true){

            win();

        }

	}

	private void FixedUpdate(){

		rb.AddForce(force);

	}

	public void AddPoints(int points){ 
    
        score += points;
    
        GameManager.Instance.setScore(score);

    }

    public void OnDamage(){

        GameManager.Instance.setHealth((int)GetComponent<Health>().health);
        
    }

    public void OnHeal(){

        GameManager.Instance.setHealth((int)GetComponent<Health>().health);
        
    }

    public void OnDeath(){

        GameManager.Instance.setGameOver();

        Destroy(gameObject);
    
    }

    public void win(){

        GameManager.Instance.setGameWon();

        Destroy(gameObject);

        didWin = false;
    
    }

    public void switchGrav(){

        if (gravSwitch == false){

            gravSwitch = true;

        }else{

            gravSwitch = false;

        }

    }

}
