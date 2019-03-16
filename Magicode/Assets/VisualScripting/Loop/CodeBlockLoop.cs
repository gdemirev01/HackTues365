using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockLoop : CodeBlockBegin
{
    public override string execute()
    {
        throw new System.NotImplementedException();
    }


    void Start()
    {
        this.executableCode = "Executable code for a generic code block";
    }

}
