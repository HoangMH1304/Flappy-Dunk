using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTabHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] underlines;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private GameObject[] tabs;

    enum Tabs
    {
        balls = 0,
        wings,
        hoops,
        flames
    }
    private void OnEnable()
    {
        OnClickBallsTab();
    }

    private void InitialState()
    {
        for(int i = 0; i < tabs.Length; i++)
        {
            underlines[i].SetActive(false);
            tabs[i].SetActive(false);
            texts[i].color = Color.black;
        }
    }
    private void OnClick(int index)
    {
        InitialState();
        underlines[index].SetActive(true);
        texts[index].color = Color.cyan;
        tabs[index].SetActive(true);
    }


    public void OnClickBallsTab()
    {
        int index = (int)Tabs.balls;
        OnClick(index);
    }


    public void OnClickWingsTab()
    {
        int index = (int)Tabs.wings;
        OnClick(index);
    }

    public void OnClickHoopsTab()
    {
        int index = (int)Tabs.hoops;
        OnClick(index);
    }

    public void OnClickFlamesTab()
    {
        int index = (int)Tabs.flames;
        OnClick(index);
    }
}
