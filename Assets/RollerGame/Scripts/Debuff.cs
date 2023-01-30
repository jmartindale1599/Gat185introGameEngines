using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CollisionEvent))]

public class Debuff : Interactable{

	void Start(){

		GetComponent<CollisionEvent>().onEnter += OnInteract;

    }

	public override void OnInteract(GameObject target){

		int cause = Random.Range(1, 3);

		switch (cause){

			case 1:

				//trying to flip the camera upside down is a pain to figure out ugghhhhh

				break;
			
			case 2:
				
				

				break;

			case 3:



				break;

			default:

				

				break;
		}

		throw new System.NotImplementedException();

	}

}
