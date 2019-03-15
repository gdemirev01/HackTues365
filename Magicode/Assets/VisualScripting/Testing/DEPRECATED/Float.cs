using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatWrap : DataType
{
    private float value;

    public override dynamic GetValue()
    {
        return value;
    }

    public override void SetValue(dynamic value)
    {
        // this.value = (float) value;
    }
}
