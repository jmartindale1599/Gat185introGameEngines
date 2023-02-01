using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]

public class WinCon : Interactable{

    // Start is called before the first frame update

    void Start(){
        
    }

    public override void OnInteract(GameObject target){

		target.GetComponent<PlayerRoll>().didWin = true;

	}

}
