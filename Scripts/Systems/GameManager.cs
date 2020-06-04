using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool doesStartFresh;

    public static PlayerStats stats;
    public static Inventory inv;
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
        uiManager = GetComponent<UIManager>();
        clickManager = GetComponent<ClickManager>();
        playerMovement = GetComponent<PlayerMovement>();
        dialogueManager = GetComponent<DialogueManager>();
        questManager = GetComponent<QuestManager>();


        //Load stats
        if (SaveManager.Load<PlayerStats>() != null && !doesStartFresh)
            stats = SaveManager.Load<PlayerStats>();
        else
            stats = new PlayerStats();

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
