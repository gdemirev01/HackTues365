﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyBlockDamage : PropertyBlock
{
    // Start is called before the first frame update
    void Start()
    {
        this.propertyName = "Damage";
        textField.SetText(propertyName);
    }

    
}
