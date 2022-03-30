using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ProjectBuildTool : MonoBehaviour
{
    public static List<string> Scenes = new List<string>();

    //得到项目的名称
    public static string ProjectName
    {
        get
        {
            foreach (string arg in System.Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("project", System.StringComparison.Ordinal))
                {
                    return arg.Split("-"[0])[1];
                }
            }
            return "ExportProject";
        }
    }

    public static bool CheckAndSetBuildState()
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                Scenes.Add(scene.path);
            } 
        }
        return true;
    }

    [MenuItem("Tools/ExportGradleProject")]
    public static void ExportGradleProject()
    {
        if (!CheckAndSetBuildState())
        {
             return;
        }
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
        
        BuildPipeline.BuildPlayer(Scenes.ToArray(), ProjectName, BuildTarget.Android, BuildOptions.None);
    }

}
