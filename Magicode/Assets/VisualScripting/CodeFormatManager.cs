using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeFormatManager : MonoBehaviour
{
    [SerializeField]
    private VisualScriptingManager VSManager;

    public static CodeFormatManager instance;

    private void Start()
    {
        instance = this;
    }

    public void formatCode(List<CodeBlock> codeBlocks)
    {
        List<List<CodeBlock>> loopPairs = getLoopPairs(codeBlocks);
        List<List<CodeBlock>> ifPairs = getIfPairs(codeBlocks);

        resetAll(codeBlocks);

        indentPairs(loopPairs, codeBlocks);
        indentPairs(ifPairs, codeBlocks);
    }

    private void indentPairs(List<List<CodeBlock>> pairs, List<CodeBlock> codeBlocks)
    {
        foreach (List<CodeBlock> pair in pairs)
        {
            //Debug.Log("beginning index: " + codeBlocks.IndexOf(pair[0]) + ", end index:" + codeBlocks.IndexOf(pair[1]));
            for (int i = codeBlocks.IndexOf(pair[0]) + 1; i < codeBlocks.IndexOf(pair[1]); i++)
            {
                //Debug.Log("indented: " + i, codeBlocks[i].gameObject);
                //Debug.Log(codeBlocks[i]);
                indent(codeBlocks[i]);
            }

        }
    }

    private List<List<CodeBlock>> getLoopPairs(List<CodeBlock> codeBlocks)
    {
        List<List<CodeBlock>> loopPairs = new List<List<CodeBlock>>();
        Stack<CodeBlock> loops = new Stack<CodeBlock>();

        foreach (CodeBlock cb in codeBlocks)
        {
            if (cb is CodeBlockLoop)
            {
                loops.Push(cb);
            }
            else if (cb is CodeBlockEndLoop)
            {
                List<CodeBlock> pair = new List<CodeBlock>();
                pair.Add(loops.Pop());
                pair.Add(cb);
                loopPairs.Add(pair);
            }
        }
        loopPairs.Reverse();
        return loopPairs;
    }

    private List<List<CodeBlock>> getIfPairs(List<CodeBlock> codeBlocks)
    {
        List<List<CodeBlock>> ifPairs = new List<List<CodeBlock>>();
        Stack<CodeBlock> ifs = new Stack<CodeBlock>();

        foreach (CodeBlock cb in codeBlocks)
        {
            if (cb is CodeBlockIf)
            {
                ifs.Push(cb);
            }
            else if (cb is CodeBlockEndIf)
            {
                List<CodeBlock> pair = new List<CodeBlock>();
                pair.Add(ifs.Pop());
                pair.Add(cb);
                ifPairs.Add(pair);
            }
        }
        ifPairs.Reverse();
        return ifPairs;
    }

    private void resetAll(List<CodeBlock> codeBlocks)
    {
        float referenceWidth = codeBlocks[0].GetComponent<LayoutElement>().preferredWidth;
        foreach (CodeBlock cb in codeBlocks)
        {
            cb.GetComponent<LayoutElement>().preferredWidth = referenceWidth;
        }
    }

    private void indent(CodeBlock cb)
    {
        LayoutElement le = cb.GetComponent<LayoutElement>();
        le.preferredWidth = le.preferredWidth - 25;
    }
}
