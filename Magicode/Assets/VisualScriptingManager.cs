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
        codeBlock.transform.parent = codeBlockArea.transform;
        
        if(codeBlocks.Count == 0 || codeBlocks.First().transform.position.y < y)
        {
            Debug.Log("Add first");
            codeBlock.transform.SetAsFirstSibling();
            codeBlocks.Insert(0, codeBlock);
        }
        else if(codeBlocks.Last().transform.position.y > y)
        {
            Debug.Log("Add last");
            codeBlock.transform.SetAsLastSibling();
            codeBlocks.Add(codeBlock);
        }
        else
        {
            Debug.Log("To else");
            for(int i = 0; i < codeBlocks.Count - 1; i++)
            {
                CodeBlock currentBlock = codeBlocks.ElementAt(i);
                CodeBlock nextBlock = codeBlocks.ElementAt(i + 1);
                float firstY = currentBlock.transform.position.y;
                float secondY = nextBlock.transform.position.y;

                if(firstY > y && secondY < y)
                {
                    codeBlocks.Insert(i + 1, codeBlock);
                    codeBlock.transform.SetSiblingIndex(i + 1);
                }
            }
        }
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
}
