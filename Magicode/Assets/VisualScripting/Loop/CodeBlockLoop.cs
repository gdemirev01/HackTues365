using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeBlockLoop : CodeBlockBegin
{
    private int loopCounter;

    private bool loopCounterIsOK;

    public override string execute()
    {
        int val = 1;
        return "for(int i = 0; i < " + val + "; i++) {"; // TODO[CODEBLOCK] FIX
    }

    public void setLoopCounter(TMP_InputField input)
    {
        
        if (input.text != null && FindObjectOfType<TypeValidator>().validateInt(input.text, loopCounter))
        {
            Debug.Log(input.text);
            loopCounterIsOK = true;
        } else
        {
            loopCounterIsOK = false;
        }
    }

    public override bool validateBlock()
    {
        if (loopCounterIsOK)
            return true;
        return false;
    }

    public int getLoopCounter()
    {
        return loopCounter;
    }

    void Start()
    {
        this.executableCode = "Executable code for a generic code block";
    }

}
