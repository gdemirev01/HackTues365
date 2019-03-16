using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;

public class FloatInputField : InputField
{
    [SerializeField]
    private TMP_InputField inputField;

    private float value;

    public override string getInput()
    {
        if (inputField.text != null && FindObjectOfType<TypeValidator>().validateFloat(inputField.text, out this.value))
            return value.ToString();
        throw new System.ArgumentException(System.String.Format("{0} is not a float", inputField.text), "inputField");
    }
}
