<<<<<<< HEAD:Assets/scripts/Tutorial.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
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

public class Tutorial : MonoBehaviour
{
    public void loadTitle()
    {
        SceneManager.LoadSceneAsync("Title");
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> c7eacf0d2b4cf42e17fc12762d5bb1f5230280bc:Assets/Tutorial.cs
