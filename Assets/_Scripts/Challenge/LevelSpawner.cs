using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSpawner : MonoBehaviour
{
    private const string FIRST_PLAY = "FirstPlay";
    public static LevelSpawner Instance;
    [SerializeField] private CanvasGroup challengeCanvas;
    [SerializeField] private GameObject challengePanel;
    [SerializeField] private List<GameObject> level;
    [SerializeField] private Player player;
    [SerializeField] private CanvasGroup tutorialPanel;
    [SerializeField] private GameObject secondChancePanel;
    [SerializeField] private CanvasGroup gameplayPanel;
    [SerializeField] private GameObject scoreText;
    private Vector2 direction;
    // public Level currentLevel;

    private void Start() {
        direction = player.Direction;   
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }
    public void SelectLevel(int levelID)
    {
        GameManager.Instance.ChangeGameMode(GameMode.Challenge);
        // currentLevel = _level;
        secondChancePanel.SetActive(false);
        secondChancePanel.transform.DOKill();
        secondChancePanel.transform.DOMoveX(-7, 0f);


        challengePanel.transform.DOKill();  //
        challengePanel.transform.DOMoveY(15, 0f).SetUpdate(true); // 0.5f

        // challengeCanvas.DOFade(0, 0).SetUpdate(true);
        // challengeCanvas.DOFade(1, 1).SetUpdate(true);

        level[levelID].SetActive(true);

        if(levelID == 2)
        {
            player.Direction = new Vector2(1.5f, 8);
        }

        gameplayPanel.gameObject.SetActive(true);
        gameplayPanel.DOFade(0, 0).SetUpdate(true);
        gameplayPanel.DOFade(1, 2).SetUpdate(true);

        player.gameObject.SetActive(true);
        scoreText.SetActive(false);

        if (PlayerPrefs.GetInt(FIRST_PLAY) == 1)
        // if(PlayerPrefs.GetInt("HighScore") == 0)
        {
            PlayerPrefs.SetInt(FIRST_PLAY, 2);
            tutorialPanel.gameObject.SetActive(true);
            tutorialPanel.DOFade(0, 0).SetUpdate(true);
            tutorialPanel.DOFade(1, 2).SetUpdate(true);
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
        player.Direction = direction;
    }

    public void CheckBtn()
    {
        Logger.LogWarning("Clickable");
    }
}
