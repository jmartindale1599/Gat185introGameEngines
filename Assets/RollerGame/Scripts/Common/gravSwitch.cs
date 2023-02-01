using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]

public class gravSwitch : Interactable{

	void Start(){

		GetComponent<CollisionEvent>().onEnter += OnInteract;

	}

	public override void OnInteract(GameObject target){

		if (target.TryGetComponent<PlayerRoll>(out PlayerRoll player)){

			player.switchGrav();

		}
		
	}

}
