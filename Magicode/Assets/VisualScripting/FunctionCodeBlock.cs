﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FunctionCodeBlock : CodeBlock
{
    [SerializeField]
    private TMPro.TMP_Text title;

    [SerializeField]
    private TMPro.TMP_Text description;

    [SerializeField]
    private GameObject InputFieldArea;

    private List<string> parameters = new List<string>();

    [SerializeField]
    private FloatInputField FloatInputFieldPrefab;

    [SerializeField]
    private IntegerInputField IntegerInputFieldPrefab;

    [SerializeField]
    private Vector3InputField Vector3InputFieldPrefab;

    [SerializeField]
    private List<InputField> allInputFields = new List<InputField>();

    public override string execute()
    {
        collectParams();
        // primer: GetComponent<Spell>().ActivateEffect(\"move_forward\", 5, new Vector3(0, 1, 1));
        string code = "GetComponent<Spell>().ActivateEffect(\"";
        code += title.text + "\"";
        foreach (string parameter in parameters)
        {
            code += ", " + parameter;
        }
        code += ");\n";
        //Debug.Log("function executable code: " + this.executableCode);
        return code;
    }

    private void collectParams()
    {
        Debug.Log("allInputFields count: " + allInputFields.Count);
        if (validateBlock())
        {
            parameters.Clear();
            foreach (InputField inputField in allInputFields)
            {
                parameters.Add(inputField.getInput());
            }
        }
        else
        {
            Debug.Log("INCORRECT INPUT FIELDS");
        }

    }

    public void SetTitle(string str)
    {
        title.text = str;
    }

    public void SetDescription(string str)
    {
        description.text = str;
    }

    public void SetParameters(List<VariableType> parameterTypes)
    {
        // Ilia pls napravi magiqta da se sluchi tuk.
        if(parameterTypes.Count <= 0)
        {
            Debug.Log("paramsis null, ", gameObject);
            return;
        }
        foreach (VariableType type in parameterTypes)
        {
            if (type == VariableType.Integer)
            {
                //Debug.Log("Instantiated IntegerInputFieldPrefab");
                IntegerInputField inputField = Instantiate<IntegerInputField>(IntegerInputFieldPrefab, InputFieldArea.transform);
                allInputFields.Add(inputField);
                //Debug.Log("size of allInputFields: " + allInputFields.Count);
            }
            else if (type == VariableType.Float)
            {
                FloatInputField inputField = Instantiate<FloatInputField>(FloatInputFieldPrefab, InputFieldArea.transform);
                allInputFields.Add(inputField);
            }
            else if (type == VariableType.Vector3)
            {
                Vector3InputField inputField = Instantiate<Vector3InputField>(Vector3InputFieldPrefab, InputFieldArea.transform);
                allInputFields.Add(inputField);
            }
        }
    }

    public override bool validateBlock()
    {

        foreach (InputField inputField in allInputFields)
        {
            try
            {
                //Debug.Log(inputField.getInput());
                inputField.getInput();
            }
            catch (System.ArgumentException e)
            {
                return false;
            }
        }
        return true;
    }

    

    //private void Start()
    //{
        //List<VariableType> temp = new List<VariableType>();
        //temp.Add(VariableType.Float);
        //temp.Add(VariableType.Vector3);
        //SetParameters(temp);
    //}
}