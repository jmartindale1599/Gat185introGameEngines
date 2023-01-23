using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collidable{

    [SerializeField] GameObject pickupFX;

    void Start(){

        onEnter += OnCoinPickup;

    }

    void Update(){
        
    }

    void OnCoinPickup(GameObject go){

        if(go.TryGetComponent<PlayerRoll>(out PlayerRoll player)){

            player.AddPoints(100);

        }

        Debug.Log("Pickup");

        Instantiate(pickupFX, transform.position, Quaternion.identity);
    
        Destroy(this.gameObject);

    }

}
