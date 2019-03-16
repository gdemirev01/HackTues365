﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPanelInfo : MonoBehaviour
{
    [SerializeField]
    private List<MinionInfo> minions;

    [SerializeField]
    private GameObject multipleInfoPanel;

    [SerializeField][Tooltip("In scene")]
    private SingleInfoPanel singlePanel;

    [SerializeField]
    private SingleInfoPanel singlePanelPrefab;

    private bool inMultipleView = false;

    public void SetMinions(List<MinionInfo> info)
    {
        minions = info;
    }

    public void SetFieldMode()
    {
        if(minions.Count <= 0)
        {

        }
        else if(minions.Count == 1)
        {
            singlePanel.gameObject.SetActive(true);
            multipleInfoPanel.gameObject.SetActive(false);
            singlePanel.SetInfo(minions[0]);
        }
        else
        {
            singlePanel.gameObject.SetActive(false);
            multipleInfoPanel.gameObject.SetActive(true);
            List<Transform> children = new List<Transform>();
            for(int i = 0; i < multipleInfoPanel.transform.childCount; i++)
            {
                children.Add(multipleInfoPanel.transform.GetChild(0));
            }

            for(int i = 0; i < children.Count; i++)
            {
                Destroy(children[i]);
            }

            for(int i = 0; i < minions.Count; i++)
            {
                SingleInfoPanel panel = Instantiate(singlePanelPrefab, multipleInfoPanel.transform);
                panel.SetInfo(minions[i]);
            }
        }
    }


}
