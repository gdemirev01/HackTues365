using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CodeBlockCompiler : MonoBehaviour
{
    

    [SerializeField]
    [TextArea(3, 30)]
    private string memberVariables;

    [SerializeField]
    [TextArea(3, 30)]
    private string preCodeString;

    [SerializeField]
    [TextArea(3, 30)]
    private string afterCodeString;
    
    static public Dictionary<string, string> vars = new Dictionary<string, string>();
    


    public static CodeBlockCompiler instance;
    
    private void Start()
    {
        instance = this;
    }

    private bool checkAndCollectVariables(List<CodeBlock> codeBlocks)
    {
        foreach (CodeBlock cb in codeBlocks)
        {
            if (cb is CodeBlockCreate)
            {
                CodeBlockCreate cbc = (CodeBlockCreate)cb;
                if (vars.ContainsKey(cbc.getVarName()))
                {
                    //Debug.Log("FALSE RETURNED");
                    return false;
                }
                try
                {
                    //Debug.Log("var name: " + cbc.getVarName() + " var val: " + cbc.getVarVal());
                    vars.Add(cbc.getVarName(), cbc.getVarVal());
                } catch(System.ArgumentException e)
                {
                    return false;
                }
                
            }
        }
        /*foreach (KeyValuePair<string, string> entry in vars)
        {
            Debug.Log("var name: " + entry.Key + " var val: " + entry.Value);
        }*/
        return true;
    }
    
    private bool blocksAreValid(List<CodeBlock> codeBlocks)
    {
        if (checkAndCollectVariables(codeBlocks))
            Debug.Log("Vars collected!");
        else
            Debug.Log("FAILED TO COLLECT VARS");

        foreach (KeyValuePair<string, string> entry in vars)
        {
            Debug.Log("var name: " + entry.Key + " var val: " + entry.Value);
        }

        //Debug.Log("vars count in blocksAreValid(): " + vars.Count);

        if (!CodeValidationManager.instance.validateCode(codeBlocks))
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
                //Debug.Log("vars count in blocksAreValid(): " + vars.Count);

                return false;
            }
        }
        // TODO: Other shit
        
        return true;
    }

    

    public void compile()
    {
        Debug.Log("Compile");
        BaseMinionBehaviour unit = VisualScriptingManager.instance.GetSelectedUnit();
        //Debug.Log("unit ", unit);

        List<CodeBlock> codeBlocks = VisualScriptingManager.instance.GetCodeBlocks();
        //debugBlocks = codeBlocks;
        //Debug.Log("after code blocks");
        int spellIndex = VisualScriptingManager.instance.GetSpellIndex(); // returns 0 always;

        Spell spell = unit.GetSpellObject(0);
        //Debug.Log("spell ", spell);

        if (blocksAreValid(codeBlocks))
        {
            int totalManaCost = 0;
            
            string path = Application.dataPath + "/StreamingAssets/" + unit.name + ".txt";
            
            string code = "";
            code += "using System.Collections;\n using UnityEngine;\n";
            code += "public class " + unit.name + " : MonoBehaviour {";
            
            code += preCodeString;
            foreach (CodeBlock codeBlock in codeBlocks)
            {
                totalManaCost += codeBlock.manaCost;
                string line = codeBlock.execute();
                code += line;
                //Debug.Log("write line " + line);
            }
            //TODO: GET MANA COST FROM HERE (totalManaCost)
            Debug.Log(code);
            code += afterCodeString;
            Debug.Log("Write to " + path);
            //Debug.Log(code);
            File.WriteAllText(path, code);
            PopupManager.instance.ShowNotice();
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


