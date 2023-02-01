using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]

public class HealthPill : Interactable{

	[SerializeField] private float healing;

	// Start is called before the first frame update

	void Start(){

		GetComponent<CollisionEvent>().onEnter += OnInteract;

    }
	
	
	public override void OnInteract(GameObject target){
		
		if(target.TryGetComponent<Health>(out Health health)){

			health.OnApplyHealth(healing);
		
		}

		if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);

		if (destroyOnInteract) Destroy(gameObject);

	}

}
