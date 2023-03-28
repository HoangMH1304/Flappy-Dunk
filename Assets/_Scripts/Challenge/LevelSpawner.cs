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
    [SerializeField] private Object[] levels;
    [SerializeField] private Player player;
    [SerializeField] private CanvasGroup tutorialPanel;
    [SerializeField] private GameObject secondChancePanel;
    [SerializeField] private CanvasGroup gameplayPanel;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private Transform parent;
    private Vector2 direction;
    private GameObject currentLevel;
    // public Level currentLevel;

    private void Start() {
        levels = Resources.LoadAll("", typeof(GameObject));
        direction = player.Direction;   
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void SelectLevel(int levelID)
    {
        GameManager.Instance.ChangeGameMode(GameMode.Challenge);

        secondChancePanel.SetActive(false);
        secondChancePanel.transform.DOKill();
        secondChancePanel.transform.DOMoveX(-7, 0f);


        challengePanel.transform.DOKill();  //
        challengePanel.transform.DOMoveY(15, 0f); // 0.5f

        // challengeCanvas.DOFade(0, 0).SetUpdate(true);
        // challengeCanvas.DOFade(1, 1).SetUpdate(true);

        currentLevel = Instantiate(levels[levelID] as GameObject, parent);
        //levels[levelID].SetActive(true);

        if (levelID == 2)
        {
            player.Direction = new Vector2(1.5f, 8);
        }

        gameplayPanel.gameObject.SetActive(true);
        gameplayPanel.DOFade(0, 0).SetUpdate(true);
        gameplayPanel.DOFade(1, 2).SetUpdate(true);

        player.gameObject.SetActive(true);
        scoreText.SetActive(false);

        if (PlayerPrefs.GetInt(FIRST_PLAY) == 1)
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
        Destroy(currentLevel);
        player.Direction = direction;
    }
}
