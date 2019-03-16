using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TypeValidator : MonoBehaviour
{
    public static TypeValidator instance;

    private void Start()
    {
        instance = this;
    }

    public bool validateInt(string toValidate,out int convertedInt)
    {
        if (int.TryParse(toValidate, out convertedInt))
            return true;
        return false;
    }

    public bool validateFloat(string toValidate, out float convertedFloat)
    {
        toValidate = toValidate.Replace(',', '.');
        if (float.TryParse(toValidate, NumberStyles.Any, CultureInfo.InvariantCulture, out convertedFloat))
            return true;
        return false;
    }

    public bool validateVector3(string toValidate, Vector3 convertedVector)
    {
        string[] vectorParams = toValidate.Split();
        float[] floats = new float[3];
        for (int i = 0; i < 3; i++)
        {
            if (!validateFloat(vectorParams[i], out floats[i]))
            {
                return false;
            }
           
        }
        convertedVector.Set(floats[0], floats[1], floats[2]);
        return true;
    }
}
