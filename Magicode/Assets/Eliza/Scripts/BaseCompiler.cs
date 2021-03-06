﻿using Microsoft.CSharp;
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
using System.Threading;
using Mirror;
using System.Security.Policy;

public class Proxy : MarshalByRefObject {
    public Assembly GetAssembly(string assemblyPath)
    {
        try
        {
            return Assembly.LoadFile(assemblyPath);
        }
        catch (Exception)
        {
            return null;
            // throw new InvalidOperationException(ex);
        }
    }
}

public class BaseCompiler : MonoBehaviour
{
    string[] assemblyReferences;
    Assembly assembly;

    public void CompileFiles(string fileName)
    {
        try
        {
            Debug.Log("Compile " + fileName);
            string filePath = Application.streamingAssetsPath + "/" + fileName;
            Debug.Log("file path: " + filePath);
            var domain = System.AppDomain.CurrentDomain;
            this.assemblyReferences = domain
                .GetAssemblies()
                .Where(a => !(a is System.Reflection.Emit.AssemblyBuilder) && !string.IsNullOrEmpty(a.Location))
                .Select(a => a.Location)
                .ToArray();

            string dllName = fileName.Substring(0, fileName.LastIndexOf('.')) + ".dll";
            Debug.Log("dll" + dllName);
            var options = new CompilerParameters();
            options.GenerateExecutable = false;
            options.GenerateInMemory = false;
            Debug.Log(Application.dataPath + "/StreamingAssets/" + dllName);
            options.OutputAssembly = Application.dataPath + "/StreamingAssets/" + dllName;
            options.ReferencedAssemblies.AddRange(assemblyReferences);
            var compiler = new CSharpCompiler.CodeCompiler();
            var result = compiler.CompileAssemblyFromFile(options, filePath);
            if(result.Errors.Count > 0)
            {
                foreach (var error in result.Errors)
                {
                    Debug.Log(error);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }
    }
   
    public void CompileStreamingFolder()
    {
        DirectoryInfo d = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] files = d.GetFiles("*.txt");
        foreach(var file in files)
        {
            Debug.Log("Compiling " + file.ToString());
            CompileFiles(file.Name);
        }
    }

    static public void LoadAssembly(GameObject obj, string fileName, string behaviourName)
    {
        
        Assembly assembly = Assembly.Load(File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, fileName)));
        Type behaviourType = assembly.GetType(behaviourName);
        obj.AddComponent(behaviourType);
        


        /*
        AppDomainSetup domaininfo = new AppDomainSetup();
        domaininfo.ApplicationBase = System.Environment.CurrentDirectory;
        Evidence adevidence = AppDomain.CurrentDomain.Evidence;
        AppDomain domain = AppDomain.CreateDomain("MyDomain", adevidence, domaininfo);
        Type type = typeof(Proxy);
        var value = (Proxy)domain.CreateInstanceAndUnwrap(
            type.Assembly.FullName,
            type.FullName);
        var assembly = value.GetAssembly(@"C:\Users\ImperialWolf\Desktop\HackTues365\CODE.dll");
        Type behaviourType = assembly.GetType("TestMoveBulletForward");
        gameObject.AddComponent(behaviourType);
        */
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