using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI recipesDeliveredText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;
        Hide();
    }

    private void GameManagerOnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
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
        recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfullRecipes().ToString();
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
