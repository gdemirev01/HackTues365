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
            for(int i = 0; i < codeBlocks.Count; i++)
            {

            }
        }
    }

    public void HandleCodeBlockDrop(CodeBlock codeBlock, PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach(RaycastResult result in results)
        {
            if(result.gameObject == codeBlockArea)
            {
                Debug.Log("add code block");
                AddCodeBlock(codeBlock, eventData.position.y);
                return;
            }
        }
        Destroy(codeBlock);
    }
}
