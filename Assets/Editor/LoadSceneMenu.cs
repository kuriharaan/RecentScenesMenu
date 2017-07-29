using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class LoadSceneMenu : EditorWindow
{
    [MenuItem("Scenes/Open Main Scene")]
    static void OpenMainScene()
    {
        /*
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/A/A0.unity");
        */
    }

    [MenuItem("Window/Load Scene Menu")]
    static void OpenMenu()
    {
        //Vector2 mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Vector2 mousePosition = new Vector2(0.0f, 0.0f);

        LoadSceneMenu window = GetWindow<LoadSceneMenu>();
        window.position = new Rect(mousePosition.x, mousePosition.y, 200f, 24f);
        window.resetPosition = false;
        window.Show();
    }

    [SerializeField]
    Color m_Color = Color.white;


    bool resetPosition = false;

    void OnEnable()
    {
        titleContent = new GUIContent("GUI Color");
    }

    // a method to simplify adding menu items
    void AddMenuItemForColor(GenericMenu menu, string menuPath, Color color)
    {
        // the menu item is marked as selected if it matches the current value of m_Color
        menu.AddItem(new GUIContent(menuPath), m_Color.Equals(color), OnColorSelected, color);
    }

    // the GenericMenu.MenuFunction2 event handler for when a menu item is selected
    void OnColorSelected(object color)
    {
        m_Color = (Color)color;
    }

    void OnGUI()
    {
        if(!resetPosition && (null != Event.current) )
        {
            this.position = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 200f, 24f); ;
            resetPosition = true;
        }
        // set the GUI to use the color stored in m_Color
        GUI.color = m_Color;

        // display the GenericMenu when pressing a button
        if (GUILayout.Button("Select GUI Color"))
        {
            // create the menu and add items to it
            GenericMenu menu = new GenericMenu();



            // forward slashes nest menu items under submenus
            AddMenuItemForColor(menu, "RGB/Red", Color.red);
            AddMenuItemForColor(menu, "RGB/Green", Color.green);
            AddMenuItemForColor(menu, "RGB/Blue", Color.blue);

            // an empty string will create a separator at the top level
            menu.AddSeparator("");

            AddMenuItemForColor(menu, "CMYK/Cyan", Color.cyan);
            AddMenuItemForColor(menu, "CMYK/Yellow", Color.yellow);
            AddMenuItemForColor(menu, "CMYK/Magenta", Color.magenta);
            // a trailing slash will nest a separator in a submenu
            menu.AddSeparator("CMYK/");
            AddMenuItemForColor(menu, "CMYK/Black", Color.black);

            menu.AddSeparator("");

            AddMenuItemForColor(menu, "White", Color.white);

            // display the menu
            menu.ShowAsContext();
        }
    }
}
