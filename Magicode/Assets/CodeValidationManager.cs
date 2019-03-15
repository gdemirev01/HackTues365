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

    public void validateCode(List<CodeBlock> codeBlocks)
    {

        if(!loopsAreOK(codeBlocks))
            Debug.Log("A loop has not been constructed properly!");
    }

    private bool loopsAreOK(List<CodeBlock> codeBlocks)
    {
        Stack<CodeBlock> loops = new Stack<CodeBlock>();
        foreach (CodeBlock cb in codeBlocks)
        {
            if (cb is CodeBlockLoop)
            {
                loops.Push(cb);
            } else if (cb is CodeBlockEndLoop)
            {
                if (loops.Count == 0)
                    return false;
                if (!loops.Pop() is CodeBlockLoop)
                    return false;
            }
            
        }
        if (loops.Count == 0)
            return true;
        return false;
    }
}
