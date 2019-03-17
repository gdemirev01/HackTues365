using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeBlockIf : CodeBlockBegin
{
    [SerializeField]
    private TMP_InputField leftArgumentInputField;

    [SerializeField]
    private TMP_InputField compareFunctionInputField;

    [SerializeField]
    private TMP_InputField rightArgumentInputField;

    protected string condition = "true";

    private bool getInput()
    {
        if (leftArgumentInputField != null && compareFunctionInputField != null && rightArgumentInputField != null)
        {
            float temp = 0;
            if ((FindObjectOfType<TypeValidator>().validateFloat(leftArgumentInputField.text, out temp) ||
                (CodeBlockCompiler.vars.ContainsKey(leftArgumentInputField.text) &&
                FindObjectOfType<TypeValidator>().validateFloat(CodeBlockCompiler.vars[leftArgumentInputField.text], out temp)))
                && 
                (compareFunctionInputField.text == ">" || compareFunctionInputField.text == "<" || compareFunctionInputField.text == "==")
                &&
                (FindObjectOfType<TypeValidator>().validateFloat(rightArgumentInputField.text, out temp) ||
                (CodeBlockCompiler.vars.ContainsKey(rightArgumentInputField.text) &&
                FindObjectOfType<TypeValidator>().validateFloat(CodeBlockCompiler.vars[rightArgumentInputField.text], out temp))))
            {
                this.condition = leftArgumentInputField.text + compareFunctionInputField.text + rightArgumentInputField.text;
                return true;
            }
        }
        return false;
    }

    public override string execute()
    {
        getInput();
        //Debug.Log(condition);
        return "if(" + this.condition + ") {";
    }

    public override bool validateBlock()
    {
        if (getInput())
            return true;
        return false;
    }
}
