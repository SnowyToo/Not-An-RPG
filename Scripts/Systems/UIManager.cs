using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Canvas canvas;

    [SerializeField]
    private List<CanvasGroup> tabs = new List<CanvasGroup>();
    [SerializeField]
    private GridLayoutGroup skillTab;

    [SerializeField]
    private GameObject clickMenuPrefab;
    [SerializeField]
    private GameObject clickMenuButton;

    private List<TextMeshProUGUI> skillTexts = new List<TextMeshProUGUI>();

    public void Awake()
    {
        canvas = transform.GetChild(0).GetComponent<Canvas>();
    }

    public void UpdateStats()
    {
        if(skillTexts.Count == 0)
        {
            //Create the skill texts
            foreach (Skill s in GameManager.stats.skills)
            {
                TextMeshProUGUI t = new GameObject().AddComponent<TextMeshProUGUI>();
                t.transform.SetParent(skillTab.transform);
                t.fontStyle = FontStyles.Bold;
                t.fontSize = 14f;
                skillTexts.Add(t);
            }
        }
        //Update them
        for(int i = 0; i < skillTexts.Count; i++)
        {
            Skill s = GameManager.stats.skills[i];
            skillTexts[i].text = s.Type.ToString() + ": " + s.Level;
        }

    }

    private void DeactivateAll(CanvasGroup _tab)
    {
        foreach(CanvasGroup tab in tabs)
        {
            if(tab != _tab)
            {
                DisableGroup(tab);
            }
        }
    }

    public void DisableGroup(CanvasGroup tab)
    {
        tab.alpha = 0;
        tab.blocksRaycasts = false;
        tab.interactable = false;
    }

    public void EnableGroup(CanvasGroup tab)
    {
        tab.alpha = 1;
        tab.blocksRaycasts = true;
        tab.interactable = true;
    }

    public void ToggleTab(CanvasGroup tab)
    {
        DeactivateAll(tab);
        if (tab.alpha == 0)
            EnableGroup(tab);
        else
            DisableGroup(tab);
    }

    //Set up the right click menu depending on the object clicked.
    public void SetUpRightClickMenu(InteractableObject obj)
    {
        if (obj == null)
            return;
        GameObject menu = Instantiate(clickMenuPrefab, GameManager.cam.WorldToScreenPoint(obj.transform.position), Quaternion.identity, canvas.transform);
        foreach(Interaction i in obj.interactions)
        {
            Button b = Instantiate(clickMenuButton, menu.transform).GetComponent<Button>();
            //Set text to action name
            b.onClick.AddListener(i.action);
            b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.actionName;
        }
    }
}
