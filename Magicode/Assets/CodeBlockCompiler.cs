using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockCompiler : MonoBehaviour
{
    private bool blocksAreValid(List<CodeBlock> codeBlocks)
    {
        if(!CodeValidationManager.instance.validateCode(codeBlocks))
        {
            return false;
        }
        
        // Validate inputs correct
        foreach(CodeBlock codeBlock in codeBlocks)
        {
            if(codeBlock is CodeBlockCreate)
            {
                CodeBlockCreate codeBlockCreate = (CodeBlockCreate)codeBlock;
                if(!codeBlockCreate.validateVarVal())
                {
                    return false;
                }
            }
        }
        // TODO: Other shit

        return true;
    }

    public void compile()
    {
        Debug.Log("Compile");
        Unit unit = VisualScriptingManager.instance.GetSelectedUnit();
        List<CodeBlock> codeBlocks = VisualScriptingManager.instance.GetCodeBlocks();
        int spellIndex = VisualScriptingManager.instance.GetSpellIndex(); // returns 0 always;

        Spell spell = unit.GetSpellObject(0);
        string code = "";
        foreach(CodeBlock codeBlock in codeBlocks)
        {
            string blockCode = codeBlock.execute();
            Debug.Log("Add " + blockCode, codeBlock);
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
