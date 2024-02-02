using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private static UIManager ui;
    public static UIManager UI
    {
        get
        {
            InitUIManager();
            return ui;
        }
    }

    // Stat Manager
    [Header ("Managers (Need to assign)")]
    [SerializeField]
    private StatManager stat;
    public StatManager Stat { get { return stat; } }

    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    public void Test()
    {

    }

    // UI Manager
    public static void InitUIManager()
    {
        if (ui == null)
        {
            Init();
            // 무조건 찾음
            GameObject goManagers = GameObject.Find("Managers");

            Transform trUIManager = goManagers.transform.Find("UIManager");
            GameObject goUIManager;

            if (trUIManager == null)
            {
                goUIManager = new GameObject { name = "UIManager" };
                goUIManager.AddComponent<UIManager>();

                goUIManager.transform.SetParent(goManagers.transform);
            }
            else
            {
                goUIManager = trUIManager.gameObject;
            }

            ui = goUIManager.GetComponent<UIManager>();
        }
    }
}
