using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panels;
    

    public void SetPanelIndex(int index)
    {
        if(index < 0 || index >= panels.Count)
        {
            return;
        }
        for(int i = 0; i < panels.Count; i++)
        {
            panels[i].gameObject.SetActive(false);
        }
        panels[index].gameObject.SetActive(true);
    }
}
