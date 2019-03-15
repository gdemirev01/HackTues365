using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataType
{
    public abstract dynamic GetValue();
    public abstract void SetValue(dynamic value);

    public static bool IsVariableTypeCorrect(VariableType type, DataType data)
    {
        if(type == VariableType.Integer && data is IntegerWrap ||
           type == VariableType.Float && data is FloatWrap || 
           type == VariableType.Vector3 && data is Vector3Wrap)
        {
            return true;
        }
        return false;
    }
}
