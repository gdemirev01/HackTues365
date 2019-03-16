using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyBlockSpeed : PropertyBlock
{
    // Start is called before the first frame update
    void Start()
    {
        this.propertyName = "Speed";
        textField.SetText(propertyName);
    }


}
