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

        resetAll(codeBlocks);

        loopPairs.Reverse();
        foreach (List<CodeBlock> pair in loopPairs)
        {
            //Debug.Log("beginning index: " + codeBlocks.IndexOf(pair[0]) + ", end index:" + codeBlocks.IndexOf(pair[1]));
            for (int i = codeBlocks.IndexOf(pair[0]) + 1; i < codeBlocks.IndexOf(pair[1]); i++)
            {
                Debug.Log("indented: " + i, codeBlocks[i].gameObject);
                //Debug.Log(codeBlocks[i]);
                indent(codeBlocks[i]);
            }
            
        }
        Debug.Log("");
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
        //RectTransform rt = cb.transform as RectTransform;
        //rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
    }
}
