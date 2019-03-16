using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class CodeBlockCreateFloat : CodeBlockCreate
{

    private float floatVarValue;

    public override bool validateVarVal()
    {
        this.varValue = this.varValue.Replace(',', '.');
        if (float.TryParse(varValue, NumberStyles.Any, CultureInfo.InvariantCulture, out floatVarValue))
            return true;
        return false;
    }

    public override string getVarVal()
    {
        if (validateVarVal())
            return floatVarValue.ToString();
        throw new System.ArgumentException(System.String.Format("{0} is not a float", varValue), "varValue");
    }

    public override string execute()
    {
        throw new System.NotImplementedException();
    }
}
