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

    public event EventHandler OnStateChanged;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
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
}
