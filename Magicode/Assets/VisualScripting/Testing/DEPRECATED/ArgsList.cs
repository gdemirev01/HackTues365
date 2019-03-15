using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArgsList
{
    private List<VariableType> expectedVariables;
    private List<DataType> variables;

    
    ArgsList(List<VariableType> expectedVariables)
    {
        this.expectedVariables = expectedVariables;
        for(int i = 0; i < expectedVariables.Count; i++)
        {
            variables.Add(null);
        }
    }

    public void InsertVariable(int index, DataType value)
    {
        if(index < 0 || index >= expectedVariables.Count)
        {
            throw new System.IndexOutOfRangeException();
        }
        VariableType expected = expectedVariables.ElementAt(index);
        if(DataType.IsVariableTypeCorrect(expected, value))
        {
            variables.Add(value);
        }
        else
        {
            throw new UnityEngine.UnityException();
        }
    }


    public Tuple<VariableType, DataType> GetVariable(int index)
    {
        if (index < 0 || index >= variables.Count)
        {
            throw new System.IndexOutOfRangeException();
        }
        if (variables[index] == null)
        {
            throw new System.NullReferenceException();
        }

        VariableType variableType = expectedVariables[index];
        DataType dataType = variables[index];

        return new Tuple<VariableType, DataType>(variableType, dataType);
    }

    public VariableType GetExpectedVariableType(int index)
    {
        if (index < 0 || index >= expectedVariables.Count)
        {
            throw new System.IndexOutOfRangeException();
        }
        return expectedVariables[index];
    }
}
