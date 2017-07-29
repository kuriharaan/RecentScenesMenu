using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class LoadSceneObserver
{
    static List<string> recentScenes = new List<string>();

    static LoadSceneObserver()
    {
        for( int i = 0; i < EditorSceneManager.sceneCount; ++i )
        {
            var scene = EditorSceneManager.GetSceneAt(i);
            recentScenes.Add(scene.path);
        }

        EditorApplication.hierarchyWindowChanged += OnHierarchyWindowChanged;
    }

    static void OnHierarchyWindowChanged()
    {
        var currentScenes = new List<string>();
        for (int i = 0; i < EditorSceneManager.sceneCount; ++i)
        {
            var scene = EditorSceneManager.GetSceneAt(i);
            currentScenes.Add(scene.path);
        }

        recentScenes.RemoveAll((x) => currentScenes.Contains(x));
        recentScenes.AddRange(currentScenes);

        foreach( var s in recentScenes)
        {
            Debug.Log(s);
        }
    }
}
