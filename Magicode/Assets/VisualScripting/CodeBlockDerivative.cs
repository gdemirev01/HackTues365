﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeBlockDerivative : CodeBlock
{
    private string var;

    public override string execute()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        this.executableCode = "Executable code for a generic code block";
    }

    public void setVar(TMP_InputField text)
    {
        this.var = text.text;
    }

    public override bool validateBlock()
    {
        return true;
    }
}
