using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockEnd : CodeBlock
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
