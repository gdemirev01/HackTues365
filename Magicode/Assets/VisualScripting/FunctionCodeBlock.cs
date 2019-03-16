using System.Collections;
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

    private List<string> parameters;

    [SerializeField]
    private FloatInputField FloatInputFieldPrefab;

    [SerializeField]
    private IntegerInputField IntegerInputFieldPrefab;

    [SerializeField]
    private Vector3InputField Vector3InputFieldPrefab;

    private List<InputField> allInputFields;

    public override void execute()
    {
        // primer: GetComponent<Spell>().ActivateEffect(\"move_forward\", 5, new Vector3(0, 1, 1));
        this.executableCode = "GetComponent<Spell>().ActivateEffect(\"";
        this.executableCode += title.text + "\"";
        foreach(string parameter in parameters) {
            this.executableCode += ", " + parameter;
        }
        this.executableCode += ");\n";
        Debug.Log("function executable code: " + this.executableCode);
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
        
        foreach(VariableType type in parameterTypes)
        {
            if(type == VariableType.Integer)
            {
                IntegerInputField inputField = Instantiate<IntegerInputField>(IntegerInputFieldPrefab, InputFieldArea.transform);
                allInputFields.Add(inputField);
            }
            else if(type == VariableType.Float)
            {
                FloatInputField inputField = Instantiate<FloatInputField>(FloatInputFieldPrefab, InputFieldArea.transform);
                allInputFields.Add(inputField);
            }
            else if(type == VariableType.Vector3)
            {
                Vector3InputField inputField = Instantiate<Vector3InputField>(Vector3InputFieldPrefab, InputFieldArea.transform);
                allInputFields.Add(inputField);
            }
        }
    }

    public bool validateFields()
    {
        foreach (InputField inputField in allInputFields)
        {
            try
            {
                inputField.getInput();
            } catch (System.ArgumentException e) 
            {
                return false;
            }
        }
        return true;
    }

    private void Start()
    {
        List < VariableType > temp = new List<VariableType>();
        //temp.Add(VariableType.Float);
        temp.Add(VariableType.Vector3);
        SetParameters(temp);
    }
}
