<<<<<<< HEAD:Assets/scripts/MainMenu.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource title;
    public static MainMenu instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
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
=======
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
        Destroy(gameObject);
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
>>>>>>> c7eacf0d2b4cf42e17fc12762d5bb1f5230280bc:Assets/MainMenu.cs
