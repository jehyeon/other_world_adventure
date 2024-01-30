using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            Init();
            return instance;
        }
    }

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        Debug.Log("TEST");
    }

    // Update is called once per frame
    void Update()
    {
        // TEST
        // 임시 씬이동 키 입력
    }

    public static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("GameManager");

            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<GameManager>();
        }
    }

    public void Test()
    {
        Debug.Log("TEST!!!");
    }
}
