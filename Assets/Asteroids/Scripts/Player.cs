using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [Range(1,80), Tooltip("Controls movement speed")] public float speed = 6;

    [Range(1,360), Tooltip("Controls rotation intensity")] public float rotationRate = 110;

    public Transform bulletSpawnlocation;

    public GameObject prefab;

    void Start(){

        Debug.Log("Start");

    }

    void Update(){

        Vector3 direction = Vector3.zero;

        direction.z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.zero;

        rotation.y = Input.GetAxis("Horizontal");

        Quaternion rotate = Quaternion.Euler(rotation * rotationRate * Time.deltaTime);

        transform.rotation = transform.rotation * rotate;

        transform.Translate(direction * speed * Time.deltaTime);

        //transform.position += direction * speed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1")){

            //GetComponent<AudioSource>().Play();

            GameObject go = Instantiate(prefab, bulletSpawnlocation.position, bulletSpawnlocation.rotation);

        }

    }

	private void OnTriggerEnter(Collider other){

		if (other.gameObject.CompareTag("Enemy")){

			FindObjectOfType<AsteroidGameManager>()?.setGameOver();
		
        }

	}

	private void Awake(){

    }

}
