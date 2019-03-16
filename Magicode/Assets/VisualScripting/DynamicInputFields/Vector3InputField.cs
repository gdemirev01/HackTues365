using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;

public class Vector3InputField : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputFieldX;

    [SerializeField]
    private TMP_InputField inputFieldY;

    [SerializeField]
    private TMP_InputField inputFieldZ;

    private Vector3 value;

    public Vector3 getInput()
    {
        string vector3AsString = null;
        if (inputFieldX != null && inputFieldY != null && inputFieldZ != null)
        {
            vector3AsString = inputFieldX.text + "," + inputFieldY.text + "," + inputFieldZ.text;
            if (FindObjectOfType<TypeValidator>().validateVector3(vector3AsString, this.value))
                return value;
        }
        throw new System.ArgumentException(System.String.Format("{0} is not a valid Vector3", vector3AsString), "vector3AsString");
    }
}
