using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;

    [SerializeField] private Image barImage;
    
    // Start is called before the first frame update
    void Start()
    {
         cuttingCounter.OnProgressChanged += CuttingCounterOnOnProgressChanged;
         barImage.fillAmount = 0f;
         Hide();
    } 

    private void CuttingCounterOnOnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
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
