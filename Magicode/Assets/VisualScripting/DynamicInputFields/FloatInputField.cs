using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;

public class FloatInputField : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    private float value;

    public float getInput()
    {
        if (inputField.text != null && FindObjectOfType<TypeValidator>().validateFloat(inputField.text, this.value))
            return value;
        throw new System.ArgumentException(System.String.Format("{0} is not a float", inputField.text), "inputField");
    }
}
