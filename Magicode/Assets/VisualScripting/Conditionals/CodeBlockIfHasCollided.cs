using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockIfHasCollided : CodeBlockIf
{
    public CodeBlockIfHasCollided()
    {
        this.condition = "hasCollided";
    }
}
