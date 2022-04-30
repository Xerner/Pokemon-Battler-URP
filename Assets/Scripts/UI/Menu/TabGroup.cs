using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [HideInInspector]
    public List<Button> TabButtons;
    public List<GameObject> Pages;
    public List<TabPageControls> PageControls = new List<TabPageControls>();
    private TabPageControls activeControls;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    [Tooltip("First index is 0")]
    [SerializeField] private int defaultTab;
    private Button ActiveTab;

    /// <summary>
    /// Subscribes a Button to this TabGroup
    /// </summary>
    /// <param name="button"></param>
    public void Subscribe(Button button)
    {
        if (TabButtons == null)
        {
            TabButtons = new List<Button>();
        }
        TabButtons.Add(button);
        if (ActiveTab == null && button.transform.GetSiblingIndex() == defaultTab) 
        { 
            OnTabSelected(button);
        }
        TabButtons.Sort(CompareIndexes);
    }

    public static int CompareIndexes(Button button1, Button button2)
    {
        return button1.transform.GetSiblingIndex() - button2.transform.GetSiblingIndex();
    }

    /// <summary>
    /// Changes the hovered TabButtons sprite the Tab Hover sprite
    /// </summary>
    /// <param name="button"></param>
    public void OnTabEnter(Button button)
    {
        ResetTabs();
        if (ActiveTab == null || button != ActiveTab) 
        { 
            button.GetComponent<Image>().sprite = tabHover;
        }
    }

    /// <summary>
    /// Resets the TabButtons sprites to their defaults
    /// </summary>
    /// <param name="button"></param>
    public void OnTabExit(Button button)
    {
        ResetTabs();
    }

    /// <summary>
    /// Selects a TabButton
    /// </summary>
    /// <param name="button"></param>
    public void OnTabSelected(Button button)
    {
        ActiveTab = button;
        ResetTabs();
        button.GetComponent<Image>().sprite = tabActive;
        SwitchToTab(button.transform.GetSiblingIndex());
    }

    /// <summary>
    /// Resets each TabButtons sprites, except the active TabButton
    /// </summary>
    public void ResetTabs(bool hardReset = false)
    {
        foreach (Button button in TabButtons)
        {
            if (!hardReset && ActiveTab != null && button == ActiveTab) continue;
            button.GetComponent<Image>().sprite = tabIdle;
        }
    }

    /// <summary>
    /// Switches to the next TabButton. If on the last tab, warps back to the 1st tab
    /// </summary>
    public void NextTab()
    {
        int index = ActiveTab.transform.GetSiblingIndex();
        if (index == Pages.Count - 1)
        {
            OnTabSelected(TabButtons[0]);
        }
        else
        {
            OnTabSelected(TabButtons[index + 1]);
        }
    }

    /// <summary>
    /// Switch to the ith TabButton
    /// </summary>
    /// <param name="index"></param>
    private void SwitchToTab(int index)
    {
        if (index >= 0 && index < TabButtons.Count)
        {
            for (int i = 0; i < Pages.Count; i++)
            {
                if (i == index)
                {
                    Pages[i].SetActive(true);
                    
                    if (i < PageControls.Count)
                    {
                        activeControls = PageControls[i];
                        if (Pages[i].TryGetComponent(out TabGroup tabGroup))
                        {
                            if (tabGroup.ActiveTab != null) activeControls.NumberKeyButtons[tabGroup.ActiveTab.transform.GetSiblingIndex()].onClick.Invoke();
                        }
                    }
                    else
                    {
                        activeControls = null;
                    }
                }
                else
                {
                    Pages[i].SetActive(false);
                }
            }
        }
        else
        {
            throw new System.Exception(name + " Tab index out of bounds " + index + ". Make sure there are the same number of tab buttons and pages in the tab group." );
        }
    }

    /// <summary>
    /// Invokes the ith Number key button listed in the active PageControls component
    /// </summary>
    /// <param name="index"></param>
    public void OnNumberKeyPress(int index)
    {
        if (activeControls != null && index < activeControls.NumberKeyButtons.Count)
        {
            activeControls.NumberKeyButtons[index].onClick.Invoke();
        }
    }
}
