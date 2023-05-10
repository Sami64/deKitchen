using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGamePause();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        });

        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGamePaused += OnGamePaused;
        GameManager.Instance.OnGameResumed += OnGameResumed;

        Hide();
    }

    private void OnGameResumed(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }


    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }


}
