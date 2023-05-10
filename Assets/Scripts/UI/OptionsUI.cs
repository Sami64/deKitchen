using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] Button soundEffectsButton;
    [SerializeField] Button musicButton;
    [SerializeField] Button backButton;
    [SerializeField] TextMeshProUGUI soundEffectsText;
    [SerializeField] TextMeshProUGUI musicText;

    [SerializeField] TextMeshProUGUI moveUpButtonText;
    [SerializeField] TextMeshProUGUI moveDownButtonText;
    [SerializeField] TextMeshProUGUI moveLeftButtonText;
    [SerializeField] TextMeshProUGUI moveRightButtonText;
    [SerializeField] TextMeshProUGUI interactButtonText;
    [SerializeField] TextMeshProUGUI interactAltButtonText;
    [SerializeField] TextMeshProUGUI pauseButtonText;

    [SerializeField] Button moveUpButton;
    [SerializeField] Button moveDownButton;
    [SerializeField] Button moveLeftButton;
    [SerializeField] Button moveRightButton;
    [SerializeField] Button interactButton;
    [SerializeField] Button interactAltButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Transform pressToChangeBindingTransform;

    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        backButton.onClick.AddListener(() =>
        {
            Hide();
        });

        moveDownButton.onClick.AddListener(() => ChangeBinding(GameInput.Binding.MoveUp));
        moveUpButton.onClick.AddListener(() => ChangeBinding(GameInput.Binding.MoveDown));
        moveLeftButton.onClick.AddListener(() => ChangeBinding(GameInput.Binding.MoveLeft));
        moveRightButton.onClick.AddListener(() => ChangeBinding(GameInput.Binding.MoveRight));
        interactButton.onClick.AddListener(() => ChangeBinding(GameInput.Binding.Interact));
        interactAltButton.onClick.AddListener(() => ChangeBinding(GameInput.Binding.InteractAlternate));
        pauseButton.onClick.AddListener(() => ChangeBinding(GameInput.Binding.Pause));
    }

    private void Start()
    {
        GameManager.Instance.OnGameResumed += OnGameResumed;
        UpdateVisual();

        Hide();
        HidePressToChangeBinding();
    }

    private void OnGameResumed(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        moveUpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void ShowPressToChangeBinding()
    {
        pressToChangeBindingTransform.gameObject.SetActive(true);
    }

    public void HidePressToChangeBinding()
    {
        pressToChangeBindingTransform.gameObject.SetActive(false);
    }

    void ChangeBinding(GameInput.Binding binding)
    {
        ShowPressToChangeBinding();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            UpdateVisual();
            HidePressToChangeBinding();
        });
    }
}
