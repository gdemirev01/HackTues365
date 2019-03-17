using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScriptFetcher : MonoBehaviour
{
    public static List<string> scripts = new List<string>();

    void FetchStreamingAssets()
    {
        DirectoryInfo d = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] files = d.GetFiles("*.txt");
        int i = 0;
        scripts.Clear();
        foreach(var file in files)
        {
            scripts.Add(file.Name.Substring(0, file.Name.LastIndexOf('.')));
            i++;
        }
    }

    private void Start()
    {
        scripts = new List<string>();
        FetchStreamingAssets();
        GetComponent<Dropdown>().options.Clear();
        foreach(var script in scripts)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData();
            optionData.text = script;
            GetComponent<Dropdown>().options.Add(optionData);
        }
    }
}
