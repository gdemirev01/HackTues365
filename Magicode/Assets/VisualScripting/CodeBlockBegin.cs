﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CodeBlockBegin : CodeBlock
{
    public override bool validateBlock()
    {
        return true;
    }
}
