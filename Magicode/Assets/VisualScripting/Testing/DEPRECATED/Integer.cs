using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegerWrap : DataType
{
    private int value;

    public override dynamic GetValue()
    {
        return value;
    }
    public override void SetValue(dynamic value)
    {
        // this.value = (int) value;
    }
}
