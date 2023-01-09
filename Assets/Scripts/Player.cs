using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [Range(1,10), Tooltip("Controls movement speed")] public float speed = 6;

    public GameObject prefab;

    void Start(){

        Debug.Log("Start");

    }

    void Update(){

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) direction.x = -1;

        if (Input.GetKey(KeyCode.D)) direction.x = +1;

        if (Input.GetKey(KeyCode.W)) direction.z = +1;

        if (Input.GetKey(KeyCode.S)) direction.z = -1;

        direction.x = Input.GetAxis("Horizontal");

        direction.z = Input.GetAxis("Vertical");

        transform.position += direction * speed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1")){

            Debug.Log("pew pew!");

            GetComponent<AudioSource>().Play();

            GameObject go = Instantiate(prefab, transform.position, transform.rotation);

            Destroy(go, 7);

        }

    }

    private void Awake(){

        //Debug.Log("Awake"); degub yada yada

    }

}
