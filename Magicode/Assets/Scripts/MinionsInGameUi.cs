﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionsInGameUi : MonoBehaviour
{

    [SerializeField] // TODO: Remove
    private List<MinionInfo> minions;

    // Update is called once per frame
    
    void UpdateMinionsInfo(List<MinionInfo> minions)
    {

    }
    

    void Update()
    {
        /*
        if(minions.Count == 1)
        {
            transform.GetComponent<CanvasGroup>().alpha = 1;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.GetComponent<CanvasGroup>().interactable = true;
            var health = minions[0].GetComponent<MinionInfo>().health;
            List<string> speels = minions[0].GetComponent<MinionInfo>().spells;
            transform.Find("Health").transform.Find("Slider").GetComponent<Slider>().value = health;
            transform.Find("Info").transform.Find("Name").GetComponent<Text>().text = minions[0].name;
            //set images for speels 
        }
        else if(minions.Count > 1) {
            transform.Find("Info").transform.Find("Name").GetComponent<Text>().text = "Minions(" + minions.Count + ")";
        }
        else
        {
            transform.GetComponent<CanvasGroup>().alpha = 0;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = false ;
            transform.GetComponent<CanvasGroup>().interactable = false;
        }
        */
    }
}