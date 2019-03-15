using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Wrap : DataType
{
    private Vector3 value;

    public override dynamic GetValue()
    {
        return value;
    }

    public override void SetValue(dynamic value)
    {
        // this.value = (Vector3) value;
    }
}
