using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class CodeBlockCreate : CodeBlock
{
    protected string varName;
    protected string varValue;

    public string getVarName()
    {
        return varName;
    }

    public void setVarName(TMP_InputField input)
    {
        if (input != null)
        {
            this.varName = input.text;
            Debug.Log("varName: " + varName);
        }
    }

    public void setVarVal(TMP_InputField input)
    {
        if (input != null)
        {
            this.varValue = input.text;
            Debug.Log("varValue: " + this.varValue);
        }
    }

    public abstract string getVarVal();
}
