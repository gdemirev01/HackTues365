using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockIf : CodeBlockBegin
{
    public override string execute()
    {
        string condition = "true"; // TODO FIX
        return "if(" + condition + " ) {";
    }
}
