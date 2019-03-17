using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeBlockGeneric : CodeBlock
{
    [SerializeField]
    private TMP_Text textField;

    [SerializeField]
    private string blockValue;

    [SerializeField]
    private string blockName;

    public override string execute()
    {
        return blockValue;
    }

    public override bool validateBlock()
    {
        return true;
    }

    void Start()
    {
        textField.SetText(blockName);
    }


}
