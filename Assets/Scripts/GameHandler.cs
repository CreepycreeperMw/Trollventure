using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject music = null;
    [SerializeField] GameAssets assetsHandler = null;
    public static GameAssets assets;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("MusicHandler(Clone)"))
        {
            Instantiate(music);
        }
        if (!GameObject.Find("GameAssets(Clone)"))
        {
            assets = Instantiate(assetsHandler);
        }
    }
}
