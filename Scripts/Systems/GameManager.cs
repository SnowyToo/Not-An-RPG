using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool doesStartFresh;

    public static PlayerStats stats;
    public static Inventory inv;
    public static Transform player;

    public static UIManager uiManager;
    public static ClickManager clickManager;
    public static PlayerMovement playerMovement;
    public static DialogueManager dialogueManager;
    public static QuestManager questManager;
    public static Camera cam;

    public PlayerStats debug;

    void Awake()
    {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player").transform;

        uiManager = GetComponent<UIManager>();
        clickManager = GetComponent<ClickManager>();
        playerMovement = GetComponent<PlayerMovement>();
        dialogueManager = GetComponent<DialogueManager>();
        questManager = GetComponent<QuestManager>();


        //Loading stats
        if (SaveManager.Load<PlayerStats>() != null && !doesStartFresh)
            stats = SaveManager.Load<PlayerStats>();
        else
        {
            stats = new PlayerStats();
            SaveManager.Save(stats);
        }

        //Load Inventory
        inv = new Inventory();

        debug = stats;
    }

    void Start()
    {
        uiManager.UpdateStats();
    }

    void Update()
    {
        stats = debug;
    }

}
