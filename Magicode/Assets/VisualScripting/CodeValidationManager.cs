using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeValidationManager : MonoBehaviour
{
    [SerializeField]
    private VisualScriptingManager VSManager;

    public static CodeValidationManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public bool validateCode(List<CodeBlock> codeBlocks)
    {

        if (!blocksAreOK(codeBlocks))
            return false;
        return true;
    }

    private bool blocksAreOK(List<CodeBlock> codeBlocks)
    {
        Stack<CodeBlock> loops = new Stack<CodeBlock>();
        foreach (CodeBlock cb in codeBlocks)
        {
            if (cb is CodeBlockLoop || cb is CodeBlockIf)
            {
                loops.Push(cb);
            } else if (cb is CodeBlockEnd)
            {
                if (loops.Count == 0)
                    return false;
                if (!(loops.Pop() is CodeBlockLoop || loops.Pop() is CodeBlockIf))
                    return false;
            }
            
        }
        if (loops.Count == 0)
            return true;
        return false;
    }
    
}
