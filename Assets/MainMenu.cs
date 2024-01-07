using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource title;
    public void playGame()
    {
        SceneManager.LoadSceneAsync("TestScene");
        Destroy(title);
    }

    public void loadTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void loadTitle()
    {
        SceneManager.LoadSceneAsync("Title");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
