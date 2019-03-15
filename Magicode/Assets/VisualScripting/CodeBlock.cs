using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CodeBlock : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public abstract void execute();

    [SerializeField]
    private bool isBase = true;

    private CodeBlock currentBlock;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(isBase)
        {
            currentBlock = Instantiate<CodeBlock>(this, transform.parent);
        }
        else
        {
            currentBlock = this;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentBlock.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        VisualScriptingManager.instance.HandleCodeBlockDrop(currentBlock, eventData);
    }
}
