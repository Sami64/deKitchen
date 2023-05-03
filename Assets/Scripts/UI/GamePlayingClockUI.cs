using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] Image clockImage;

    private void Update()
    {
        clockImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
