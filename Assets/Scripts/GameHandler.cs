using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject music = null;
    public static GameAssets assets;
    public static GameHandler instance;
    public static bool watchedID1 = true;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
        assets = GetComponent<GameAssets>();
    }
}
