using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class PropertyBlock : MonoBehaviour
{
    [SerializeField]
    protected TMP_InputField inputField;

    [SerializeField]
    protected TMP_Text textField;

    
    protected int propertyVal;
    protected string propertyName = "propertyName";

    public float getPropertyVal()
    {
        if (inputField != null)
        {
            float val;
            if (FindObjectOfType<TypeValidator>().validateFloat(inputField.text, out val))
            {
                return val;
            }
        }
        return -1;
    }

    public void setPropertyVal(string newVal)
    {
        int.TryParse(newVal, out propertyVal);
        this.inputField.text = newVal;
    }

    public string getPropertyName()
    {
        return propertyName;
    }
}
