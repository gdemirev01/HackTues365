﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockEndLoop : CodeBlockEnd
{
    public override string execute()
    {
        return "}";
    }

    public override bool validateBlock()
    {
        return true;
    }
}
