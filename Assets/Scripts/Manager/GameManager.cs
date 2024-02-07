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

    private void Start()
    {
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
}
