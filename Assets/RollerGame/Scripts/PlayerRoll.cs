using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerRoll : MonoBehaviour{

    [SerializeField] private Transform view;

    [SerializeField, Range(8, 50)] private float maxForce = 20;

	private int score = 0;

	private Vector3 force;

    private Rigidbody rb;

    void Start(){

        rb = GetComponent<Rigidbody>();

        view = Camera.main.transform;

        Camera.main.GetComponent<RollerCamera>().setTarget(transform);

    }

    // Update is called once per frame
    void Update(){

        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");

        direction.z = Input.GetAxis("Vertical");

        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);

        force = viewSpace * (direction * maxForce);

        if (Input.GetButtonDown("Jump")){

            rb.AddForce(Vector3.up * 7, ForceMode.Impulse);
        
        }

        if (Input.GetButtonDown("Fire1")){

            this.transform.position = Vector3.up;

        }

        GameManager.Instance.setHealth(69);

    }

	private void FixedUpdate(){

		rb.AddForce(force);

	}

    public void AddPoints(int points){ 
    
        score += points;
    
        GameManager.Instance.setScore(score);

    }

}
