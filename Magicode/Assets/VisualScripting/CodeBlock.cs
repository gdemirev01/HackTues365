using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CodeBlock : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public abstract void execute();

    [SerializeField]
    protected string executableCode;

    [SerializeField]
    private bool isBase = true;

    private CodeBlock currentBlock;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(isBase)
        {
            currentBlock = Instantiate<CodeBlock>(this, transform.parent);
            currentBlock.DisableBase();
        }
        else
        {
            currentBlock = this;
            VisualScriptingManager.instance.RemoveCodeBlock(currentBlock);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentBlock.transform.position = eventData.position;
        VisualScriptingManager.instance.HandleCodeBlockDrag(currentBlock, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        VisualScriptingManager.instance.HandleCodeBlockDrop(currentBlock, eventData);
    }

    public void DisableBase()
    {
        isBase = false;
    }

    public string getExecutableCode()
    {
        return executableCode;
    }
}
