using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockLoop : CodeBlockBegin
{
    public override string execute()
    {
        int val = 1;
        return "for(int i = 0; i < " + val + "; i++) {"; // TODO[CODEBLOCK] FIX
    }


    void Start()
    {
        this.executableCode = "Executable code for a generic code block";
    }

}
