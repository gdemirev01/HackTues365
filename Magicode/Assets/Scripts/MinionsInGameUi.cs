using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MinionsInGameUi : NetworkBehaviour
{
<<<<<<< HEAD
    public List<GameObject> minions;

    void Start()
    {
        
=======

    [SerializeField] // TODO: Remove
    private List<MinionInfo> minions;

    // Update is called once per frame
    
    void UpdateMinionsInfo(List<MinionInfo> minions)
    {

>>>>>>> 5e1bb90f19994c7b4a3b9272fe10d7bcf6f67ddc
    }
    

    void Update()
    {
        /*
        if(minions.Count == 1)
        {
            transform.GetComponent<CanvasGroup>().alpha = 1;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.GetComponent<CanvasGroup>().interactable = true;
            var health = minions[0].GetComponent<BaseMinionBehaviour>().health;
            var spells = minions[0].GetComponent<BaseMinionBehaviour>().spells;
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
