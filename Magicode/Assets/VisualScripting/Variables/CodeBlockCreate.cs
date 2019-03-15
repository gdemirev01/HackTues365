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
        }
    }

    public void setVarVal(TMP_InputField input)
    {
        if (input != null)
        {
            this.varValue = input.text;
        }
    }

    public abstract bool validateVarVal();

    public abstract string getVarVal();
}
