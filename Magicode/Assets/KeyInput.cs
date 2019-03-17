using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        KeyCode[] keyCodes = {
             KeyCode.Alpha1,
             KeyCode.Alpha2,
             KeyCode.Alpha3,
             KeyCode.Alpha4,
             KeyCode.Alpha5,
             KeyCode.Alpha6,
        };

        int i = 1;
        foreach(KeyCode keycode in keyCodes){
            if(Input.GetKeyDown(keycode))  
            {
                var minion = GameObject.Find("Minion " + i);
                var selectedMinions = gameObject.GetComponent<BaseCameraBehaviour>().selectedMinions;
                selectedMinions.Clear();
                selectedMinions.Add(minion);
            }
            i++;
        }

    }
}
