using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class DescriptionLevel : MonoBehaviour
{
    [SerializeField] private RectTransform scaleObject;
    [SerializeField] private TextMeshProUGUI tittleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI confirmText;
    private Level level;
    public Level Level { get => level; set => level = value; }

    private void OnEnable()
    {
        // InitStat();
        scaleObject.DOScale(new Vector3(0, 0, 0), 0f);
        scaleObject.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    // private void InitStat()
    // {
    //     string tittle = "CHALLENGE " + level.ID.ToString();
    //     string description = level.description.ToString();
    //     string confirm = !level.GetFinishState() ? "START" : "RETRY";
    //     SetContentOfLevel(tittle, description, confirm);
    // }

    // public void SetContentOfLevel(string title, string description, string confirm)
    // {
    //     tittleText.text = title;
    //     descriptionText.text = description;
    //     confirmText.text = confirm;
    // }

    public void OnClickStartBtn()
    {
        gameObject.SetActive(false);
        LevelSpawner.Instance.SelectLevel(PlayerPrefs.GetInt("LevelSelected"));
    }
    public void OnClickCancelBtn()
    {
        scaleObject.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => gameObject.SetActive(false));
    }


}
