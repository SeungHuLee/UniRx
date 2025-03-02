﻿#if UNITY_EDITOR

using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class PackageExporter
{
    [MenuItem("Tools/Export Unitypackage")]
    public static void Export()
    {
        var root = "Plugins/UniRx";
        var version = GetVersion(root);
        var name = root.Split('/').Last();
        var fileName = string.IsNullOrEmpty(version) ? $"{name}.unitypackage" : $"{name}.{version}.unitypackage";
        var exportPath = $"./{fileName}";
        var path = Path.Combine(Application.dataPath, root);

        var assets = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
            .Where(x => Path.GetExtension(x) is ".cs" or ".asmdef" or ".json" or ".meta" or ".unity")
            .Select(x => "Assets" + x.Replace(Application.dataPath, "").Replace(@"\", "/"))
            .ToArray();

        AssetDatabase.ExportPackage(
            assets,
            exportPath,
            ExportPackageOptions.Default);

        Debug.Log("Export complete: " + Path.GetFullPath(exportPath));
    }

    static string GetVersion(string root)
    {
        var version = Environment.GetEnvironmentVariable("UNITY_PACKAGE_VERSION");
        var versionJson = Path.Combine(Application.dataPath, root, "package.json");

        if (File.Exists(versionJson))
        {
            var v = JsonUtility.FromJson<Version>(File.ReadAllText(versionJson));

            if (!string.IsNullOrEmpty(version))
            {
                if (v.version != version)
                {
                    var msg = $"package.json and env version are mismatched. UNITY_PACKAGE_VERSION:{version}, package.json:{v.version}";

                    if (Application.isBatchMode)
                    {
                        Console.WriteLine(msg);
                        Application.Quit(1);
                    }

                    throw new Exception("package.json and env version are mismatched.");
                }
            }

            version = v.version;
        }

        return version;
    }

    public class Version
    {
        public string version;
    }
}

#endif