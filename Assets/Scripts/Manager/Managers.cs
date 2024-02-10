using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIManager), typeof(DataManager))]
public class Managers : MonoBehaviour
{
    // Managers
    private static Managers instance;
    public static Managers Instance
    {
        get
        {
            Init();
            return instance;
        }
    }

    // Input Manager
    private InputManager input = new InputManager();
    public static InputManager Input
    {
        get
        {
            return instance.input;
        }
    }

    // UI Manager
    private UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }

    // Data Manager
    private DataManager dataManager;
    public DataManager DataManager { get { return dataManager; } }

    // Game Setting
    [Header("Settings")]
    [SerializeField]
    private float diffFromDest = 0.01f;
    public float DiffFromDest { get { return diffFromDest; } }

    [SerializeField]
    private float diffForRenderFlip = 0.01f;
    public float DiffForRenderFlip { get { return diffForRenderFlip; } }

    private void Awake()
    {
        Init();

        uiManager = GetComponent<UIManager>();
        dataManager = GetComponent<DataManager>();
    }

    void Update()
    {
        // InputManager
        input.OnUpdate();
    }

    public static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("Managers");

            if (go == null)
            {
                go = new GameObject { name = "Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();
        }
    }
}
