using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSpawner : MonoBehaviour
{
    private const string FIRST_PLAY = "FirstPlay";
    public static LevelSpawner Instance;
    [SerializeField] private GameObject challengeCanvas;
    [SerializeField] private GameObject challengePanel;
    [SerializeField] private List<GameObject> level;
    [SerializeField] private GameObject player;
    [SerializeField] private CanvasGroup tutorialPanel;
    [SerializeField] private GameObject secondChancePanel;
    [SerializeField] private CanvasGroup gameplayPanel;
    [SerializeField] private GameObject scoreText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SelectLevel()
    {
        GameManager.Instance.ChangeGameMode(GameMode.Challenge);

        secondChancePanel.SetActive(false);
        secondChancePanel.transform.DOKill();
        secondChancePanel.transform.DOMoveX(-7, 0f);

        challengePanel.transform.DOKill();  //
        challengePanel.transform.DOMoveY(15, 0.5f).SetUpdate(true); //
        level[0].SetActive(true);

        gameplayPanel.gameObject.SetActive(true);
        gameplayPanel.DOFade(0, 0).SetUpdate(true);
        gameplayPanel.DOFade(1, 2).SetUpdate(true);

        player.SetActive(true);
        scoreText.SetActive(false);

        if (PlayerPrefs.GetInt(FIRST_PLAY) == 1)
        {
            PlayerPrefs.SetInt(FIRST_PLAY, 2);
            tutorialPanel.gameObject.SetActive(true);
            tutorialPanel.DOFade(0, 0).SetUpdate(true);
            tutorialPanel.DOFade(1, 1).SetUpdate(true);
        }
        GameController.Instance.Init();

        SoundManager.Instance.PlaySound(Sound.whistle);
    }

    public void ExitLevelMode()
    {
        foreach(GameObject _level in level)
        {
            _level.SetActive(false);
        }
    }
}
