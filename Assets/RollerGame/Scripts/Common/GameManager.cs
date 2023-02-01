using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>{

    [SerializeField] Slider healthBar;

    [SerializeField] AudioSource playingGame;

    [SerializeField] TMP_Text scoreUI;

    [SerializeField] GameObject titleUI;

    [SerializeField] GameObject playerPrefab;

    [SerializeField] Transform playerStart;

    [SerializeField] GameObject gameOverUI;

    [SerializeField] GameObject winUI;

    public enum State{ TITLE, START_GAME, PLAY_GAME, GAME_OVER, WIN}

    State state = State.TITLE;

    float stateTimer = 0;

    private void Start(){

    }

	private void Update(){

        switch (state){

            case State.TITLE:

				titleUI.SetActive(true);

                Cursor.lockState = CursorLockMode.None;

                Cursor.visible = true;

				break;
            
            case State.START_GAME:

                titleUI.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;

				Instantiate(playerPrefab, playerStart.position, playerStart.rotation);

                playingGame.Play();

				state = State.PLAY_GAME;

				break;
            
            case State.PLAY_GAME:
            
                break;
            
            case State.GAME_OVER:

				stateTimer -= Time.deltaTime;

                if(stateTimer <= 0){

                    gameOverUI.SetActive(false);

                    state = State.TITLE;
                
                }
            
                break;

            case State.WIN:

				stateTimer -= Time.deltaTime;

                if(stateTimer <= 0){

                    winUI.SetActive(false);

                    state = State.TITLE;
                
                }
            
                break;
           
            default:
            
                break;
        
        }

    }

	public void setHealth(int health) { 
    
        healthBar.value = Mathf.Clamp(health, 0, 100);
    
    }

    public void setScore(int score){

        scoreUI.text = score.ToString();

    }

    public void setGameOver(){

		playingGame.Stop();

		gameOverUI.SetActive(true);

        state = State.GAME_OVER;

        stateTimer = 5;

    }

    public void setGameWon(){

        winUI.SetActive(true);

        state = State.WIN;

        stateTimer = 15;
    
    }

    public void onStartGame(){

        state = State.START_GAME;
    
    }

}
