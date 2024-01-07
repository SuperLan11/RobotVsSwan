using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Sprite[] swans;
    private string dialogue = "MERP";
    
    public GameObject portrait;
    public GameObject selectionIndicator;
    public int dialogueIndex = 0;

    void Start()
    {
    }
    
    public void ChangeDialogue(string newDialogue)
    {
        progress = 0;
        dialogue = newDialogue;
        dialogueIndex++;
    }

    public float progress;
    private const float PROGRESS_PER_UPDATE = 0.5f;

    void FixedUpdate()
    {
        progress += PROGRESS_PER_UPDATE;
        int length = dialogue.Length;
        progress = Mathf.Min(length, progress);
        string text = dialogue.Substring(0, (int) (progress + 0.01f));
        GetComponentInChildren<TextMeshProUGUI>().text = text;
        portrait.GetComponent<Image>().sprite = swans[dialogueIndex - 1];
        selectionIndicator.SetActive(Finished());
    }
    
    public void Skip()
    {
        progress = dialogue.Length;
    }

    public bool Finished()
    {
        return progress >= dialogue.Length - 0.01f;
    }
}