using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;

    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;
    
    // Start is called before the first frame update
    void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        hasProgress.OnProgressChanged += HasProgressOnProgressChanged;
         barImage.fillAmount = 0f;
         Hide();
    } 

    private void HasProgressOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        
        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
            Hide();
        else
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
