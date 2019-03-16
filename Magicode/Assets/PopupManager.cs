using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField]
    private Popup errorPopup;

    [SerializeField]
    private Popup noticePopup;

    public static PopupManager instance;

    void Start()
    {
        instance = this;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        
    }
    
    public void ShowError()
    {
        errorPopup.gameObject.SetActive(true);
        errorPopup.Show();
    }

    public void ShowNotice()
    {
        noticePopup.gameObject.SetActive(true);
        noticePopup.Show();
    }
}
