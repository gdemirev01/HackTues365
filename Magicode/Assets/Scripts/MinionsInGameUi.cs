using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionsInGameUi : MonoBehaviour
{

    public GameObject minion;
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if(minion != null)
        {
            var health = minion.GetComponent<MinionInfo>().health;
            List<string> speels = minion.GetComponent<MinionInfo>().speels;
            transform.Find("HealthAndImage").GetComponent<Text>().text = health + "";
            //set images for speels 
        }
    }
}
