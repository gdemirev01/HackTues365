using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetTextFromField : MonoBehaviour
{

    public TMP_InputField userInput;


    // Start is called before the first frame update
    void Start()
    {
        userInput.text = "123";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
