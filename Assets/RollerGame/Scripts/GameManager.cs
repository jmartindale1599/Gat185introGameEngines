using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>{

    [SerializeField] Slider healthBar;

    [SerializeField] TMP_Text scoreUI;

    [SerializeField] GameObject playerPrefab;

    [SerializeField] Transform playerStart;

    private void Start(){

        Instantiate(playerPrefab, playerStart.position, playerStart.rotation);

    }

    public void setHealth(int health) { 
    
        healthBar.value = Mathf.Clamp(health, 0, 100);
    
    }

    public void setScore(int score){

        scoreUI.text = score.ToString();

    }

}
