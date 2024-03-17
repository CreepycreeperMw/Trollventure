using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject music = null;
    public static GameAssets assets;
    public static GameHandler instance;

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
        if (!GameObject.Find("MusicHandler(Clone)"))
        {
            Instantiate(music);
        }
        assets = GetComponent<GameAssets>();
    }
}
