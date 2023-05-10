using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    State state;
    float waitingToStartTimer = 1f;
    float countdownToStartTimer = 3f;
    float gamePlayingTimer;
    float gamePlayingTimerMax = 10f;
    bool isGamePaused = false;

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInputOnPauseAction;
    }

    private void GameInputOnPauseAction(object sender, EventArgs e)
    {
        ToggleGamePause();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
        }
    }

    public bool IsGamePlaying() => state == State.GamePlaying;

    public bool IsGameOver() => state == State.GameOver;

    public bool IsCountdownToStartActive() => state == State.CountdownToStart;

    public float GetCountdownToStartTimer() => countdownToStartTimer;

    public float GetGamePlayingTimerNormalized() => 1 - (gamePlayingTimer / gamePlayingTimerMax);

    public void ToggleGamePause()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameResumed?.Invoke(this, EventArgs.Empty);
        }
    }
}
