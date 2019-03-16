using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using CSharpCompiler;
using System.Linq;
using System.Collections.Generic;

public class BaseCompiler : MonoBehaviour
{
    string[] assemblyReferences;
    Assembly assembly;

    public void CompileFiles(string filePath)
    {
        filePath = Path.Combine(Application.streamingAssetsPath, filePath);
        var domain = System.AppDomain.CurrentDomain;
        this.assemblyReferences = domain
            .GetAssemblies()
            .Where(a => !(a is System.Reflection.Emit.AssemblyBuilder) && !string.IsNullOrEmpty(a.Location))
            .Select(a => a.Location)
            .ToArray();

        var options = new CompilerParameters();
        options.GenerateExecutable = false;
        options.GenerateInMemory = false;
        options.OutputAssembly = @"C:\Users\ImperialWolf\Desktop\HackTues365\CODE.dll";
        options.ReferencedAssemblies.AddRange(assemblyReferences);
        var compiler = new CSharpCompiler.CodeCompiler();
        var result = compiler.CompileAssemblyFromFile(options, filePath);
        //this.assembly = result.CompiledAssembly;
    }


    //static string outputErrors;

    //void Start()
    //{

    //}

    //private void Update()
    //{

    //}

    //public void CompileCode()
    //{
    //    string source = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "code.txt"));
    //    string output = Path.Combine("C:/Users/ImperialWolf/Desktop/HackTues365/", "CODE.dll");
    //    Compile(source, output);
    //}

    //public static void Compile(string source, string outputDll)
    //{
    //    outputErrors = "No errors";
    //    try
    //    {
    //        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text += "Creating codeprovider\n";
    //        var provider = new CSharpCodeProvider();
    //        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text += "Creating parameters\n";
    //        var param = new CompilerParameters();
    //        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text += "Adding system\n";
    //        param.ReferencedAssemblies.Add("System.dll");
    //        //not sure if this one is gonna work in standalone build but w/e
    //        //if not i am just gonna add dlls in the Resources folder
    //        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text += "Adding unity assemblies\n";
    //        param.ReferencedAssemblies.Add(typeof(MonoBehaviour).Assembly.Location);
    //        param.ReferencedAssemblies.Add(typeof(Collider).Assembly.Location);
    //        param.ReferencedAssemblies.Add(typeof(BaseMinionBehaviour).Assembly.Location);
    //        param.ReferencedAssemblies.Add(typeof(NetworkBehaviour).Assembly.Location);
    //        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text += "setting parameters\n";
    //        param.GenerateExecutable = false;
    //        param.GenerateInMemory = false;
    //        param.OutputAssembly = outputDll;
    //        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text += "compiling\n";
    //        var result = provider.CompileAssemblyFromSource(param, source);
    //        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text += "compiled\n";
    //        if (result.Errors.Count > 0)
    //        {
    //            var msg = new StringBuilder();
    //            foreach (CompilerError error in result.Errors)
    //            {
    //                msg.AppendFormat("Error ({0}): {1}\n", error.ErrorNumber, error.ErrorText);
    //            }
    //            outputErrors = msg.ToString();
    //            throw new Exception(msg.ToString());
    //        }
    //        else
    //        {
    //            GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text += result.PathToAssembly;
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>().text += e.Message;
    //    }
    //}

}
