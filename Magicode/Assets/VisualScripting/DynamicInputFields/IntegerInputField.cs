using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;

public class IntegerInputField : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    private int value;

    public int getInput()
    {
        if (inputField.text != null && FindObjectOfType<TypeValidator>().validateInt(inputField.text, this.value))
            return value;
        throw new System.ArgumentException(System.String.Format("{0} is not an integer", inputField.text), "inputField");
    }
}
