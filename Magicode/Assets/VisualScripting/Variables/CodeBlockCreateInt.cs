using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeBlockCreateInt : CodeBlockCreate
{
    

    private int intVarValue;

    public override bool validateVarVal()
    {
        if (int.TryParse(varValue, out intVarValue))
            return true;
        return false;
    }

    public override string getVarVal()
    {
        if (validateVarVal())
            return intVarValue.ToString();
        throw new System.ArgumentException(System.String.Format("{0} is not an integer", varValue), "varValue");
    }

    public override string execute()
    {
        return "int " + getVarName() + " = " + intVarValue.ToString() + ";";
    }
}
