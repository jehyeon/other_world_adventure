using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AnimationGeneratorEditor : EditorWindow
{
    private struct AnimationInfo
    {
        public int gap;
        public int count;
        public List<string> names;

        public AnimationInfo(int _gap, int _count, string[] _names)
        {
            gap = _gap;
            count = _count;
            names = new List<string>();
            for (int i = 0; i < count; i++)
            {
                names.Add(_names[i]);
            }
        }
    };

    public GameObject obj = null;
    private string baseLoadPath = "";
    private string parentPathForLoad = "";
    private string baseSavePath = "";
    private string parentPathForSave = "";
    private int gap;
    private int count;
    private string[] names = new string[20];

    // Animation Info
    private AnimationInfo info;

    [MenuItem("Animation/AnimationGenerator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AnimationGeneratorEditor));
    }

    private static void Initialize()
    {
        AnimationGeneratorEditor window = 
            (AnimationGeneratorEditor)EditorWindow.GetWindow(
                typeof(AnimationGenerator),
                true,
                "Animation Generator Tools");

        window.position = new Rect(0, 0, 250, 150);
    }

    public void OnGUI()
    {
        EditorGUILayout.LabelField("Animation Info");
        gap = EditorGUILayout.IntField("Gap", gap);
        count = EditorGUILayout.IntField("Count", count);

        for (int i = 0; i < count; i++)
        {
            names[i] = EditorGUILayout.TextField("Animation name", names[i]);
        }

        if (GUILayout.Button("Save"))
        {
            info = new AnimationInfo(gap, count, names);
        }

        EditorGUILayout.LabelField("Load & Save Path");
        baseLoadPath = EditorGUILayout.TextField("Base load path", baseLoadPath);
        parentPathForLoad = EditorGUILayout.TextField("Folder name for load", parentPathForLoad);
        GUILayout.Space(10);
        baseSavePath = EditorGUILayout.TextField("Base save path", baseSavePath);
        parentPathForSave = EditorGUILayout.TextField("Folder name for save", parentPathForSave);

        if (GUILayout.Button("Generate"))
        {
            TryLoadAndSave();
        }
    }

    private void TryLoadAndSave()
    {
        string loadPath = baseLoadPath + "/" + parentPathForLoad;
        string savePath = baseSavePath + "/" + parentPathForSave;
        DirectoryInfo di = new DirectoryInfo(loadPath);

        foreach (FileInfo file in di.GetFiles("*.png"))
        {
            LoadAndSave(loadPath, savePath, file.Name);
        }
    }

    private void LoadAndSave(string loadPath, string savePath, string fileName)
    {
        string before = loadPath + "/" + fileName;
        string after = savePath + "/" + fileName[0];

        AnimationGenerator generator = FindObjectOfType<AnimationGenerator>();

        UnityEngine.Object[] objs = AssetDatabase.LoadAllAssetsAtPath(before);

        int count = 0;
        int animationIndex = 0;
        Debug.Log(string.Format("Sprite path: {0}", before));
        for (int i = 1; i < info.count * info.gap + 1; i++)
        {
            count++;
            generator.AddSprite(objs[i]);
            Debug.Log(objs[i]);
            if (count == info.gap)
            {
                AnimationClip animClip = generator.Generate();

                // 상위 폴더가 없는 경우
                if (!Directory.Exists(after))
                {
                    Directory.CreateDirectory(after);
                }

                AssetDatabase.CreateAsset(
                    animClip, after + "/" + info.names[animationIndex] + ".anim");
                AssetDatabase.SaveAssets();
                Debug.Log(string.Format("Animation save path: {0}", 
                    after + "/" + info.names[animationIndex]));

                generator.ClearSprites();
                count = 0;
                animationIndex++;
            }
        }
        
        AssetDatabase.Refresh();

    }
}
