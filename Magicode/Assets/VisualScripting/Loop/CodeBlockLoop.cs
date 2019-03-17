using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeBlockLoop : CodeBlockBegin
{
    private TMP_InputField input;

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
            this.input = input;
            int temp = 0;
            if (FindObjectOfType<TypeValidator>().validateInt(input.text, out temp) || 
                (CodeBlockCompiler.vars.ContainsKey(input.text) && 
                FindObjectOfType<TypeValidator>().validateInt(CodeBlockCompiler.vars[input.text], out temp)))
            {
                loopCounter = input.text;
                //Debug.Log("loop counter:" + loopCounter);
                loopCounterIsOK = true;
                return;
            }
            //Debug.Log("input.text: " + input.text);
            //Debug.Log("vars count: " + CodeBlockCompiler.vars.Count);
            foreach (KeyValuePair<string, string> entry in CodeBlockCompiler.vars)
            {
                //Debug.Log("entry key: " + entry.Key);
            }
        }
        loopCounterIsOK = false;
    }

    public override bool validateBlock()
    {
        setLoopCounter(this.input);
        if (loopCounterIsOK)
            return true;
        return false;
    }

    public string getLoopCounter()
    {
        return loopCounter;
    }

}
