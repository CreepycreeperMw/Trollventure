using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    Rigidbody2D rigP;
    [SerializeField] private GameObject optionsButton;
    private void Start()
    {
        rigP = gameObject.GetComponent<Rigidbody2D>();
    }
    public async void StartGame()
    {
        rigP.bodyType = RigidbodyType2D.Dynamic;
        rigP.velocity  = (new Vector2(0f, 200f));
        await Task.Delay(1000);
        SceneManager.LoadSceneAsync(1);

    }
}
