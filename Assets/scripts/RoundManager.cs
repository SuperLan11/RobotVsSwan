using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    
    public RoundState roundState;
    public int roundNumber;

    public GameObject[] swans;
    public Transform[] swanSpots;

    public GameObject editorUI;
    public GameObject fightUI;
    public GameObject dialogueUI;

    public GameObject gameCamera;
    public GameObject editorCamera;

    public GameObject editorRobotSpawn;
    public GameObject gameRobotSpawn;

    public int[] endRoundRewards;
    public bool lastRoundWasWin;
    
    void Awake()
    {
        instance = this;
    }

    void StartRound(int round)
    {
        AudioManager.instance.Stop("editor_music");
        AudioManager.instance.Play("main_music");
        roundNumber = round;
        roundState = RoundState.DIALOGUE;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(swans[i + (round-1)*3], swanSpots[i].position, Quaternion.identity);
        }
        Robot.instance.transform.position = gameRobotSpawn.transform.position;
        Robot.instance.health = Robot.instance.GetMaxHealth();
        dialogueUI.SetActive(true);
        DialogueController.instance.StartCutscene(roundNumber);
    }

    public void EndDialogue()
    {
        roundState = RoundState.GAME;
    }

    public void StartNextRound()
    {
        StartRound(roundNumber + 1);
    }
    
    void StartEditor(bool win = true)
    {
        AudioManager.instance.Play("editor_music");
        AudioManager.instance.Stop("main_music");
        if (win) Robot.instance.eggs += endRoundRewards[roundNumber - 1];
        if (!win) roundNumber--;
        roundState = RoundState.EDITOR;
        Robot.instance.transform.position = editorRobotSpawn.transform.position;
        Robot.instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        lastRoundWasWin = win;
    }

    private void Start()
    {
        StartRound(1);
    }
    
    void Update()
    {
        fightUI.SetActive(roundState == RoundState.GAME);
        editorUI.SetActive(roundState == RoundState.EDITOR);
        gameCamera.SetActive(roundState != RoundState.EDITOR);
        editorCamera.SetActive(roundState == RoundState.EDITOR);
        dialogueUI.SetActive(roundState == RoundState.DIALOGUE);
        if (roundState == RoundState.GAME && FindObjectsOfType<Swan>().Length == 0)
        {
            if (roundNumber == 3)
            {
                SceneManager.LoadScene("Win");
            }
            else
            {
                StartEditor();
            }
        }

        if (Robot.instance.health <= 0 && roundState == RoundState.GAME)
        {
            StartEditor(win: false);
            foreach (Swan swan in FindObjectsOfType<Swan>())
            {
                Destroy(swan.gameObject);
            }
        }
    }
}
