using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeBlockLoop : CodeBlockBegin
{
    private string loopCounter;

    private bool loopCounterIsOK;

    public override string execute()
    {
        return "for(int i = 0; i < " + loopCounter + "; i++) {"; // TODO[CODEBLOCK] FIX
    }

    public void setLoopCounter(TMP_InputField input)
    {
        if (input != null)
        {
            int temp = 0;
            if (FindObjectOfType<TypeValidator>().validateInt(input.text, temp))
            {
                loopCounter = input.text;
                Debug.Log("loop counter:" + loopCounter);
                loopCounterIsOK = true;
                return;
            }
        }
        loopCounterIsOK = false;
    }

    public override bool validateBlock()
    {
        if (loopCounterIsOK)
            return true;
        return false;
    }

    public string getLoopCounter()
    {
        return loopCounter;
    }

    void Start()
    {
        this.executableCode = "Executable code for a generic code block";
    }

}
