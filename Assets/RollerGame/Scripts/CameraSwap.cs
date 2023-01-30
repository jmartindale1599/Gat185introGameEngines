using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : Interactable{

	void Start(){

		GetComponent<CollisionEvent>().onEnter += OnInteract;

	}

	public override void OnInteract(GameObject target){

		if (target.TryGetComponent<PlayerRoll>(out PlayerRoll player)){

			player.GetComponent<RollerCamera>().setTarget(player.transform);

		}

	}

}
