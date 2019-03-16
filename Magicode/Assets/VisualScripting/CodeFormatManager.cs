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
        List<List<CodeBlock>> allPairs = getPairs(codeBlocks);

        resetAll(codeBlocks);

        indentPairs(allPairs, codeBlocks);
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

    private List<List<CodeBlock>> getPairs(List<CodeBlock> codeBlocks)
    {
        List<List<CodeBlock>> allPairs = new List<List<CodeBlock>>();
        Stack<CodeBlock> allBlockBounds = new Stack<CodeBlock>();

        foreach (CodeBlock cb in codeBlocks)
        {
            if (cb is CodeBlockIf || cb is CodeBlockLoop)
            {
                allBlockBounds.Push(cb);
            }
            else if (cb is CodeBlockEnd)
            {
                List<CodeBlock> pair = new List<CodeBlock>();
                pair.Add(allBlockBounds.Pop());
                pair.Add(cb);
                allPairs.Add(pair);
            }
        }
        allPairs.Reverse();
        //Debug.Log("allPairs count: " + allPairs.Count);
        return allPairs;
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
