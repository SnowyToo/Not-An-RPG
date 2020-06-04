using System;
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
    private VerticalLayoutGroup skillTab;

    [SerializeField]
    private Transform inventory;
    private Image[] invImg;
    private TextMeshProUGUI[] invText;

    [SerializeField]
    private GameObject clickMenuPrefab;
    [SerializeField]
    private GameObject clickMenuButton;

    

    private List<TextMeshProUGUI> skillTexts = new List<TextMeshProUGUI>();

    public void Awake()
    {
        canvas = transform.GetChild(0).GetComponent<Canvas>();
        invImg = new Image[GameManager.inv.items.Length];
        invText = new TextMeshProUGUI[GameManager.inv.items.Length];
        for (int i = 0; i < invImg.Length; i++)
        {
            invImg[i] = inventory.GetChild(i).GetComponent<Image>();
            invText[i] = inventory.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        UpdateInventory();
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
                t.enableAutoSizing = true;
                t.fontSizeMax = 28;
                t.transform.localScale = new Vector3(1, 1, 1);
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

    public void UpdateInventory()
    {
        for(int i = 0; i < invImg.Length; i++)
        {
            ItemStack stack = GameManager.inv.items[i];
            if (stack.obj != null)
            {
                invImg[i].sprite = stack.obj.sprite;
                invImg[i].color = Color.white;
            }
            invText[i].text = GetInvString(stack.count);
        }
    }



    private void DeactivateAllExcept(CanvasGroup _tab)
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
        DeactivateAllExcept(tab);
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
            b.onClick.AddListener(delegate { obj.Interact(i); });
            b.onClick.AddListener(delegate { menu.GetComponent<RightClickMenu>().DoAction(); });
            b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = obj.name + ": " + i.actionName;
        }
    }

    public string GetInvString(int i)
    {
        if (i == 0)
            return "";
        if (i >= 1000000000)
            return Math.Round(i / 1000000000f, 1) + "b";
        if (i >= 10000000)
            return Math.Round(i / 1000000f, 1) + "m";
        if (i >= 10000)
            return Math.Round(i / 1000f, 1) + "k";
        return i.ToString();
    }
}
