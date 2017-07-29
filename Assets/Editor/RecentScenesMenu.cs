using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class RecentScenesMenu : EditorWindow
{
    [MenuItem("Window/Recent Scenes Menu")]
    static void OpenMenu()
    {
        RecentScenesMenu window = GetWindow<RecentScenesMenu>();
        window.position = new Rect(60.0f, 60.0f, windowWidth, windowHeight);
        window.resetPosition = false;
        window.Show();
    }

    static List<string> recentScenes = new List<string>();



    static readonly int MaxDispScene = 3;

    static readonly float windowWidth  = 360.0f;
    static readonly float windowHeight = 80.0f;

    bool resetPosition = false;

    static RecentScenesMenu()
    {
        EditorApplication.hierarchyWindowChanged += OnHierarchyWindowChanged;
    }

    static void OnHierarchyWindowChanged()
    {
        UpdateRecentScenesList();
    }

    static void UpdateRecentScenesList()
    {
        var currentScenes = new List<string>();
        for (int i = 0; i < EditorSceneManager.sceneCount; ++i)
        {
            var scene = EditorSceneManager.GetSceneAt(i);
            currentScenes.Add(scene.path);
        }

        recentScenes.RemoveAll((x) => currentScenes.Contains(x));
        recentScenes.AddRange(currentScenes);
    }

    private void OnEnable()
    {
        UpdateRecentScenesList();
    }

    private void OnGUI()
    {
        if( !resetPosition && (Event.current != null ))
        {
            Vector2 mousePosition = Event.current.mousePosition;
            position = new Rect(mousePosition.x, mousePosition.y, windowWidth, windowHeight);
            resetPosition = true;
        }

        string nextLevel = null;
        for ( int i = 0; (i < MaxDispScene) && (i < recentScenes.Count); ++i )
        {
            var s = recentScenes[recentScenes.Count - i - 1];
            if (GUILayout.Button(string.Concat("Load  ", s) ) )
            {
                nextLevel = s;
            }
        }

        if( !string.IsNullOrEmpty(nextLevel) )
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(nextLevel);
            Close();
        }
    }
}
