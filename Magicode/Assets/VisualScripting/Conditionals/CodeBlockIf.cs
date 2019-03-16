using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockIf : CodeBlockBegin
{
    protected string condition = "true";

    public override string execute()
    {
        return "if(" + this.condition + " ) {";
    }
}
