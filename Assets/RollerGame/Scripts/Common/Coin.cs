using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Interactable{

    void Start(){

        GetComponent<CollisionEvent>().onEnter += OnInteract;

    }

    public override void OnInteract(GameObject go){

        if(go.TryGetComponent<PlayerRoll>(out PlayerRoll player)){

            player.AddPoints(100);

        }

        Debug.Log("Pickup");

        if(interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
    
        if(destroyOnInteract)Destroy(gameObject);

    }

}
