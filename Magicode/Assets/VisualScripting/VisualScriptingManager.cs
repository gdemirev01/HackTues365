using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using SimpleJSON;
using System;

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

    [SerializeField]
    private GameObject placeholderAsset;

    [SerializeField] // TODO: Remove serialize field;
    private Unit selectedUnit;

    [SerializeField]
    private TMPro.TMP_Text selectedUnitName;

    [SerializeField]
    private GameObject blockContainer;
    
    Dictionary<Unit, List<CodeBlock>> unitScripts = new Dictionary<Unit, List<CodeBlock>>();
    
    private GameObject placeholder;
    int placeholderSiblingIndex;

    [SerializeField]
    private CodeValidationManager CVManager;
    [SerializeField]
    private CodeFormatManager CFManager;

    private Dictionary<string, string> vars;

    public static VisualScriptingManager instance;

    private void Start()
    {
        instance = this;
        AddInstantiatedCodeBlocks();
        vars = new Dictionary<string, string>();
    }

    public void SetActiveScriptToUnit(Unit unit)
    {
        Debug.Log("Dublicate list");
        List<CodeBlock> copiedBlocks = new List<CodeBlock>();
        copiedBlocks.AddRange(codeBlocks);
        unitScripts[unit] = copiedBlocks;
    }

    public void SetSelectedUnit(Unit unit)
    {
        SaveCurrentBlocks();

        SpellBlockGenerator.instance.GenerateSpellsForUnit(unit, 0);
        selectedUnit = unit;
        selectedUnitName.text = unit.name;

        LoadSelectedUnitBlocks();
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public List<CodeBlock> GetCodeBlocks()
    {
        return codeBlocks;
    }

    public int GetSpellIndex()
    {
        return 0;
    }
    

    public void SaveCurrentBlocks()
    {
        if (selectedUnit)
        {
            List<CodeBlock> dictBlocks = new List<CodeBlock>();
            dictBlocks.AddRange(codeBlocks);

            unitScripts[selectedUnit] = dictBlocks;
            // Unload current blocks;
            foreach (CodeBlock block in dictBlocks)
            {
                block.transform.parent = blockContainer.transform;
            }
            codeBlocks = new List<CodeBlock>();
        }
    }


    public void LoadSelectedUnitBlocks()
    {
        if (unitScripts.ContainsKey(selectedUnit))
        {
            List<CodeBlock> blocks = unitScripts[selectedUnit];
            foreach (CodeBlock codeBlock in blocks)
            {
                codeBlocks.Add(codeBlock);
                codeBlock.transform.parent = codeBlockArea.transform;
            }
        }
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
        codeBlock.transform.SetParent(codeBlockArea.transform);
        int index = GetIndexOfBlockY(codeBlock);
        codeBlock.transform.SetSiblingIndex(index);
        codeBlocks.Insert(index, codeBlock);

        if (CVManager.validateCode(codeBlocks))
            CFManager.formatCode(codeBlocks);
        //saveCode();
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
            //Debug.Log("To else");
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
        if(selectedUnit == null)
        {
            Destroy(placeholder);
            Destroy(codeBlock.gameObject);
            return;
        }
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach(RaycastResult result in results)
        {
            if(result.gameObject == codeArea)
            {
                //Debug.Log("add code block");
                Destroy(placeholder);
                AddCodeBlock(codeBlock, eventData.position.y);
                return;
            }
        }
        
        Destroy(placeholder);
        Destroy(codeBlock.gameObject);
    }


    public void saveCode()
    {
        JSONObject code = new JSONObject();


        for (int i = 0; i < codeBlocks.Count(); i++)
        {
			code.Add(i.ToString(), codeBlocks[i].getExecutableCode());
        }

        Debug.Log(code.ToString());
        
    }

    public void HandleCodeBlockDrag(CodeBlock codeBlock, PointerEventData eventData)
    {
        if(placeholder == null)
        {
            //Debug.Log("New placeholder");
            placeholder = Instantiate<GameObject>(placeholderAsset);
            placeholder.transform.SetParent(codeBlockArea.transform);
            placeholder.transform.SetAsFirstSibling();
            placeholderSiblingIndex = 0;
        }
        int index = GetIndexOfBlockY(codeBlock);
        if(placeholderSiblingIndex != index)
        {
            placeholderSiblingIndex = index;
            placeholder.transform.SetSiblingIndex(index);
        }
        
    }

    public void RemoveCodeBlock(CodeBlock codeBlock)
    {
        codeBlocks.Remove(codeBlock);
        codeBlock.transform.SetParent(intermediaryCodeArea.transform);
    }

}
