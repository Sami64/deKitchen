using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI countdownText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;
        Hide();
    }

    private void GameManagerOnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString();
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
