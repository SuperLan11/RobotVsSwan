using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void play()
    {
        Destroy(MainMenu.instance.gameObject);
        SceneManager.LoadScene("TestScene");
    }
}
