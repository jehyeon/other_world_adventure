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
        public List<bool> loops;

        public AnimationInfo(int _gap, int _count, string[] _names, bool[] _loops)
        {
            gap = _gap;
            count = _count;
            names = new List<string>();
            loops = new List<bool>();

            for (int i = 0; i < count; i++)
            {
                names.Add(_names[i]);
            }

            for (int i = 0; i < count; i++)
            {
                loops.Add(_loops[i]);
            }
        }
    };

    public GameObject obj = null;
    private string baseLoadPath = "Assets/Art/Sprites/sanctumpixel";
    private string parentPathForLoad = "";
    private string baseSavePath = "Assets/Resources/Animations";
    private string parentPathForSave = "";
    private int gap;
    private int count;
    private string[] names = new string[20];
    private bool[] loops = new bool[20];

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
            loops[i] = EditorGUILayout.Toggle("loop", loops[i]);
        }

        if (GUILayout.Button("Save"))
        {
            info = new AnimationInfo(gap, count, names, loops);
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
        // 순서대로 sprite가 안 오는 경우가 있어서 정렬
        Array.Sort(objs, (a, b) => ConvertForSort(a.name) < ConvertForSort(b.name) ? -1 : 1);

        int count = 0;
        int animationIndex = 0;
        Debug.Log(string.Format("Sprite path: {0}", before));
        for (int i = 0; i < info.count * info.gap; i++)
        {
            count++;
            generator.AddSprite(objs[i]);
            Debug.Log(objs[i].name);

            //Debug.Log(objs[0].);
            if (count == info.gap)
            {
                AnimationClip animClip = generator.Generate(info.loops[animationIndex]);
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

    private int ConvertForSort(string name)
    {
        string[] splited = name.Split("_");

        if (splited.Length > 1)
        {
            return int.Parse(splited[splited.Length - 1]);
        }

        // sheet data인 경우 (ex. not 1_16, just 1, 2)
        return 10000;
    }
}
