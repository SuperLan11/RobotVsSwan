using System;
using UnityEngine;

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
    
    void Awake()
    {
        instance = this;
    }

    void StartRound(int round)
    {
        roundNumber = round;
        roundState = RoundState.DIALOGUE;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(swans[i + (round-1)*3], swanSpots[i].position, Quaternion.identity);
        }
        Robot.instance.transform.position = gameRobotSpawn.transform.position;
        Robot.instance.health = Robot.instance.GetMaxHealth();
    }

    void EndDialogue()
    {
        roundState = RoundState.GAME;
    }

    public void StartNextRound()
    {
        StartRound(roundNumber + 1);
    }
    
    void StartEditor()
    {
        Robot.instance.eggs += endRoundRewards[roundNumber - 1];
        roundState = RoundState.EDITOR;
        Robot.instance.transform.position = editorRobotSpawn.transform.position;
        Robot.instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void Start()
    {
        StartRound(1);
    }

    void Update()
    {
        fightUI.SetActive(roundState == RoundState.GAME);
        editorUI.SetActive(roundState == RoundState.EDITOR);
        gameCamera.SetActive(roundState == RoundState.GAME);
        editorCamera.SetActive(roundState == RoundState.EDITOR);
        if (roundState == RoundState.GAME && FindObjectsOfType<Swan>().Length == 0)
        {
            StartEditor();
        }
    }
}
