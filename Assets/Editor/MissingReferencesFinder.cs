using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class MissingReferencesFinder : EditorWindow
    {
        private const int WidthFields = 200;
        private Vector2 _scrollPos;
        private static List<MissingRefInfo> _objects = new();

        [MenuItem("Custom Windows/Missing References Finder")]
        private static void ShowWindow()
        {
            var window = GetWindow<MissingReferencesFinder>();
            window.titleContent = new GUIContent("Missing References Finder");
            window.Show();
            Scan();
        }

        private void OnGUI()
        {
            GUILayout.Space(30);
            
            if (GUILayout.Button("Scan the project for missing references!"))
            {
                Scan();
            }

            GUILayout.Space(30);

            EditorGUILayout.BeginVertical();
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, false, false);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("№", GUILayout.Width(30));
            GUILayout.Label("Object", GUILayout.Width(WidthFields));
            GUILayout.Label("Component", GUILayout.Width(WidthFields));
            GUILayout.Label("Property Path", GUILayout.Width(WidthFields));
            
            EditorGUILayout.EndHorizontal();

            for (var i = 0; i < _objects.Count; i++)
            {
                var item = _objects[i];
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label($"{i+1}. ", GUILayout.Width(30));
                EditorGUILayout.ObjectField(item.Prefab, item.Prefab.GetType(), true, GUILayout.Width(WidthFields));
                EditorGUILayout.BeginVertical();

                foreach (var field in item.FieldInfos)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(field.Component, field.Component.GetType(), true, GUILayout.Width(WidthFields));
                    GUILayout.Label(field.PropertyPath);
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(10);
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private static void Scan()
        {
            using var finder = new ScannerObjects();
            _objects.Clear();
            _objects = finder.ScanProject();
            EditorUtility.DisplayDialog("Scan Project", "Success", "OK");
        }
    }
}