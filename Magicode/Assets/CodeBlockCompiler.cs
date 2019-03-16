using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockCompiler : MonoBehaviour
{
    [SerializeField]
    private List<CodeBlock> debugBlocks;

    private bool blocksAreValid(List<CodeBlock> codeBlocks)
    {
        if(!CodeValidationManager.instance.validateCode(codeBlocks))
        {
            Debug.LogWarning("error loops");
            return false;
        }
        
        // Validate inputs correct
        foreach(CodeBlock codeBlock in codeBlocks)
        {
            if (!codeBlock.validateBlock())
            {
                Debug.LogWarning("error block", codeBlock);
                return false;
            }
        }
        // TODO: Other shit

        return true;
    }

    public void compile()
    {
        Debug.Log("Compile");
        Unit unit = VisualScriptingManager.instance.GetSelectedUnit();
        //Debug.Log("unit ", unit);

        List<CodeBlock> codeBlocks = VisualScriptingManager.instance.GetCodeBlocks();
        //debugBlocks = codeBlocks;
        //Debug.Log("after code blocks");
        int spellIndex = VisualScriptingManager.instance.GetSpellIndex(); // returns 0 always;

        Spell spell = unit.GetSpellObject(0);
        //Debug.Log("spell ", spell);

        if (blocksAreValid(codeBlocks))
        {
            string code = "";
            foreach (CodeBlock codeBlock in codeBlocks)
            {
                code += codeBlock.execute();
                //Debug.Log("Add " + code, codeBlock);
            }
            Debug.Log(code);
        }
        else
        {
            PopupManager.instance.ShowError();
        }
    }

    public void validate()
    {
        Debug.Log("Validate");
        List<CodeBlock> codeBlocks = VisualScriptingManager.instance.GetCodeBlocks();
        if(blocksAreValid(codeBlocks))
        {
            Debug.Log("Is Valid");
            PopupManager.instance.ShowNotice();
        }
        else
        {
            Debug.Log("Invalid");
            PopupManager.instance.ShowError();
        }
    }
}
