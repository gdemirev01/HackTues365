using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BaseCompiler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void Compile(string source, string outputDll, out string outputErrors)
    {
        outputErrors = "";
        var provider = new CSharpCodeProvider();
        var param = new CompilerParameters();
        param.ReferencedAssemblies.Add("System.dll");
        param.ReferencedAssemblies.Add(typeof(MonoBehaviour).Assembly.Location);
        param.GenerateExecutable = false;
        param.GenerateInMemory = false;
        param.OutputAssembly = outputDll;
        var result = provider.CompileAssemblyFromSource(param, source);
        if (result.Errors.Count > 0)
        {
            var msg = new StringBuilder();
            foreach (CompilerError error in result.Errors)
            {
                msg.AppendFormat("Error ({0}): {1}\n", error.ErrorNumber, error.ErrorText);
            }
            outputErrors = msg.ToString();
            Debug.Log(msg.ToString());
            throw new Exception(msg.ToString());
        }
    }
}
