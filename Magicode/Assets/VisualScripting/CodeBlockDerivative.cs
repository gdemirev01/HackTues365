using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockDerivative : CodeBlock
{
    public override void execute()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        this.executableCode = "Executable code for a generic code block";
    }
}
