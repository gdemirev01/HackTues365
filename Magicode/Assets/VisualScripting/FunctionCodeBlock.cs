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
            }
            else if(type == VariableType.Float)
            {
                FloatInputField inputField = Instantiate<FloatInputField>(FloatInputFieldPrefab, InputFieldArea.transform);
            }
            else if(type == VariableType.Vector3)
            {
                // spawn vector3 field
            }
        }
    }

    private void Start()
    {
        List < VariableType > temp = new List<VariableType>();
        temp.Add(VariableType.Float);
        temp.Add(VariableType.Integer);
        SetParameters(temp);
    }
}
