using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace KevinCastejon.EditorToolbox
{
    /// <summary>
    /// Automatically set the Max Texture Size on multiple Texture2D assets according to their real sizes.
    /// </summary>
    public class TexturesMaxSizesSetter : EditorWindow
    {
        private static readonly int[] sizes = new int[] { 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384 };

        private string[] _assets;

        private Vector2 _scrollPos;

        private bool _closeASAP;

        private void Awake()
        {
            minSize = new Vector2(500, 300);
            List<string> validPaths = new List<string>();
            for (int i = 0; i < Selection.objects.Length; i++)
            {
                string path = AssetDatabase.GetAssetPath(Selection.objects[i]);
                if (AssetDatabase.GetMainAssetTypeAtPath(path) == typeof(Texture2D))
                {
                    validPaths.Add(path);
                }
                else if (AssetDatabase.IsValidFolder(path))
                {
                    validPaths.AddRange(GetAssetsPathsInDir(path));
                }
            }
            _assets = validPaths.ToArray();
            if (_assets.Length == 0)
            {
                Debug.Log("No texture detected into selection, please select a texture asset or a folder containing texture assets.");
                _closeASAP = true;
            }
        }

        private void Update()
        {
            if (_closeASAP)
            {
                Close();
            }
        }

      
        void OnGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("This will set Max Size textures import settings for all of these assets according to their real sizes:", new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.Space(10);
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            foreach (string assetPath in _assets)
            {
                EditorGUILayout.LabelField(" - " + assetPath);
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Ok"))
            {
                Close();
                SetTexturesSizes(_assets);
            }
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Cancel"))
            {
                Close();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
        }

        private List<string> GetAssetsPathsInDir(string dir)
        {
            List<string> validPaths = new List<string>();
            List<string> paths = new List<string>(AssetDatabase.GetAllAssetPaths()
         .Where(s => s.StartsWith(dir, StringComparison.OrdinalIgnoreCase) && s != dir));
            for (int i = 0; i < paths.Count; i++)
            {
                if (AssetDatabase.GetMainAssetTypeAtPath(paths[i]) == typeof(Texture2D))
                {
                    validPaths.Add(paths[i]);
                }
                else if (AssetDatabase.IsValidFolder(paths[i]))
                {
                    validPaths.AddRange(GetAssetsPathsInDir(paths[i]));
                }
            }
            return validPaths;
        }

        [MenuItem("Assets/EditorTools/Set Automatic Textures Sizes")]
        private static void OpenWindow()
        {
            TexturesMaxSizesSetter window = GetWindow(typeof(TexturesMaxSizesSetter)) as TexturesMaxSizesSetter;
        }

        private static void SetTexturesSizes(string[] paths)
        {
            foreach (string texturePath in paths)
            {
                TextureImporter ti = AssetImporter.GetAtPath(texturePath) as TextureImporter;
                object[] args = new object[2] { 0, 0 };
                MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
                mi.Invoke(ti, args);
                int width = (int)args[0];
                int height = (int)args[1];
                int originalMaxSize = Mathf.Max(width, height);
                int maxSizeIndex = 0;
                int maxSize = sizes[maxSizeIndex];
                while (maxSize < originalMaxSize && maxSizeIndex < sizes.Length)
                {
                    maxSizeIndex++;
                    maxSize = sizes[maxSizeIndex];
                }
                ti.maxTextureSize = maxSize;
                AssetDatabase.WriteImportSettingsIfDirty(texturePath);
                AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);
            }
        }
        //[MenuItem("Assets/Tools/Set Automatic Textures Sizes", true)]
        //private static bool SetTexturesSizesValidation()
        //{
        //    if (Selection.activeObject == null)
        //    {
        //        return false;
        //    }
        //    string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
        //    Type selectedType = AssetDatabase.GetMainAssetTypeAtPath(AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID()));
        //    bool isTextureAsset = selectedType == typeof(Texture2D);
        //    bool isDefaultAsset = selectedType == typeof(DefaultAsset);
        //    bool isNotBlank = selectedPath != "Assets" && selectedPath != "Packages";
        //    return isNotBlank && (isTextureAsset || isDefaultAsset);
        //}
    }
}