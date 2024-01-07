using System.Collections;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Dialogue dialogue;

    public string[] texts;
    private int index = 0;
    
    private IEnumerator Cutscene()
    {
        for (int i = index; i < index + 3; i++)
        {
            string text = texts[i];
            dialogue.ChangeDialogue(text);
            while (true)
            {
                yield return null;
                bool confirm = Input.GetMouseButtonDown(0);
                bool done = dialogue.Finished();
                if (confirm)
                {
                    if (done)
                    {
                        break;
                    }
                    else
                    {
                        dialogue.Skip();
                    }
                }
            }
        }
        //FindObjectOfType<AudioManager>().Stop("Opening Cutscene");
        index += 3;
    }
    public void Start()
    {
        StartCoroutine(Cutscene());
    }
}