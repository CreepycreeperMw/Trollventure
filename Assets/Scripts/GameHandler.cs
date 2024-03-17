using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject music = null;
    public static GameAssets assets;

    void Start()
    {
        if (!GameObject.Find("MusicHandler(Clone)"))
        {
            Instantiate(music);
        }
        assets = GetComponent<GameAssets>();
    }
}
