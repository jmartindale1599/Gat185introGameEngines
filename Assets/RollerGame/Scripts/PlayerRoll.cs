using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerRoll : MonoBehaviour{

    [SerializeField] private float maxForce = 8;

    private Vector3 force;

    private Rigidbody rb;

    void Start(){

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update(){

        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");

        direction.z = Input.GetAxis("Vertical");

        force = direction * 7;

        if (Input.GetButtonDown("Jump")){

            rb.AddForce(Vector3.up * 7, ForceMode.Impulse);
        
        }

        if (Input.GetButtonDown("Fire1")){

            this.transform.position = Vector3.up;

        }

    }

	private void FixedUpdate(){

		rb.AddForce(force);

	}

}
