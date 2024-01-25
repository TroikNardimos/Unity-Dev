using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject titleUI;
    [SerializeField] TMP_Text livesUI;
    [SerializeField] TMP_Text timerUI;
    [SerializeField] Slider healthUI;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;

    [SerializeField] FloatVariable health;


    [SerializeField] GameObject respawn;


    [Header("Events")]
    //[SerializeField] IntEvent scoreEvent;
    [SerializeField] VoidEvent gameStartEvent;
    [SerializeField] GameObjectEvent respawnEvent;
    [SerializeField] GameObjectEvent winEvent;
    [SerializeField] GameObjectEvent loseEvent;




    public enum State
    {
        TITLE,
        START_GAME,
        START_LEVEL,
        PLAY_GAME,
        GAME_OVER,
        WIN_GAME
    }

    public State state = State.TITLE;
    public float timer = 0;
    public int lives = 3;
    public int score = 0;

    public int Lives { 
        get { return lives; }
        set { lives = value; livesUI.text = "LIVES: " + lives.ToString(); } 
    }

    public float Timer
    {
        get { return timer; }
        set { timer = value; timerUI.text = string.Format("{0:F1}", timer); }
    }

    private void OnEnable()
    {
        //scoreEvent.Subscribe(OnAddPoints);
    }

    private void OnDisable()
    {
        //scoreEvent.Unsubscribe(OnAddPoints);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.TITLE:
                titleUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.START_GAME:
                titleUI.SetActive(false);
                winUI.SetActive(false);
                loseUI.SetActive(false);
                Timer = 60;
                Lives = 3;
                health.value = 100;
                score = 0;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gameStartEvent.RaiseEvent();
                respawnEvent.RaiseEvent(respawn);
                state = State.PLAY_GAME;
                break;
            case State.START_LEVEL:
                Timer = 60;
                health.value = 100;
                respawnEvent.RaiseEvent(respawn);
                state = State.PLAY_GAME;
                break;
            case State.PLAY_GAME:
                Timer = Timer - Time.deltaTime;
                if (Timer <= 0 || health.value <= 0.0f)
                {
                    Lives = Lives - 1;
                    if (Lives <= 0)
                    {
                        state = State.GAME_OVER;
                    }
                    else
                    {
                        state = State.START_LEVEL;
                    }
                }
                if (score >= 100)
                {
                    state = State.WIN_GAME;
                }
                else
                {
                    break;
                }
                break;
            case State.GAME_OVER:
                loseUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.WIN_GAME:
                winUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }

        healthUI.value = health.value / 100.0f;
    }

    public void OnStartGame()
    {
        state = State.START_GAME;
    }

    public void OnWinGame()
    {
        state = State.WIN_GAME;
    }

    public void OnPlayerDead()
    {
        state = State.START_GAME;
    }

    public void OnAddPoints(int points) 
    {
        score += points;
        print(score);
    }


}