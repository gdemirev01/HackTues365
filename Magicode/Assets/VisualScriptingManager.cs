using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class VisualScriptingManager : MonoBehaviour
{
    [SerializeField]
    List<CodeBlock> codeBlocks = new List<CodeBlock>();
    
    [SerializeField]
    private GameObject codeArea;

    [SerializeField]
    private GameObject codeBlockArea;

    [SerializeField]
    private GameObject intermediaryCodeArea;

    public static VisualScriptingManager instance;

    private void Start()
    {
        instance = this;
        AddInstantiatedCodeBlocks();
    }

    public void AddInstantiatedCodeBlocks()
    {
        try {
            List<CodeBlock> instantiatedBlocks = codeBlockArea.GetComponentsInChildren<CodeBlock>().ToList();
            instantiatedBlocks.OrderByDescending(o => o.transform.position.y);

            codeBlocks = instantiatedBlocks;
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("No objects at start of visual scripting");
        }
    }

    public void AddCodeBlock(CodeBlock codeBlock, float y)
    {
        if(codeBlockArea.transform)
        {

        }
        codeBlock.transform.parent = codeBlockArea.transform;
        int index = GetIndexOfBlockY(codeBlock);
        codeBlock.transform.SetSiblingIndex(index);
        codeBlocks.Insert(index, codeBlock);
    }

    public int GetIndexOfBlockY(CodeBlock codeBlock)
    {
        float y = codeBlock.transform.position.y;
        if (codeBlocks.Count == 0 || codeBlocks.First().transform.position.y < y)
        {
            return 0;
        }
        else if (codeBlocks.Last().transform.position.y > y)
        {
            return codeBlocks.Count;
        }
        else
        {
            Debug.Log("To else");
            for (int i = 0; i < codeBlocks.Count - 1; i++)
            {
                CodeBlock currentBlock = codeBlocks.ElementAt(i);
                CodeBlock nextBlock = codeBlocks.ElementAt(i + 1);
                float firstY = currentBlock.transform.position.y;
                float secondY = nextBlock.transform.position.y;

                if (firstY > y && secondY < y)
                {
                    return i + 1;
                }
            }
        }
        return 0;
    }

    public void HandleCodeBlockDrop(CodeBlock codeBlock, PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach(RaycastResult result in results)
        {
            if(result.gameObject == codeArea)
            {
                Debug.Log("add code block");
                AddCodeBlock(codeBlock, eventData.position.y);
                return;
            }
        }
        Destroy(codeBlock.gameObject);
    }

    public void HandleCodeBlockDrag(CodeBlock codeBlock, PointerEventData eventData)
    {
    }

    public void RemoveCodeBlock(CodeBlock codeBlock)
    {
        codeBlocks.Remove(codeBlock);
        codeBlock.transform.parent = intermediaryCodeArea.transform;
    }
}
