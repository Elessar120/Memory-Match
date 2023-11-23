using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TimeManager gameTimeManager;
    [SerializeField] private GameObject winGamePopup;
    [SerializeField] private GameObject looseGamePopup;
    [SerializeField] private GameObject endGameMainPanel;

    public int selectedLevel = 1;

    private static UIManager instance;

    public static UIManager Instance => instance;

    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameTimeManager.OnTimerExpired += ShowEndGameLoosePopUp;
        LevelManager.Instance.onWinLevel += ShowEndGameWinPopUp;
        LevelManager.Instance.Init(selectedLevel);
    }

    private void ShowEndGameLoosePopUp()
    {
        ShowEndGameMainPanel();
        looseGamePopup.gameObject.SetActive(true);
    }

    public void SetSelectedLevel(int levelNumber)// call when selecting levels on main menu
    {
        selectedLevel = levelNumber;
    }

    private void ShowEndGameWinPopUp()
    {
        ShowEndGameMainPanel();
        winGamePopup.gameObject.SetActive(true);
    }

    private void ShowEndGameMainPanel()
    {
        endGameMainPanel.SetActive(true);
    }

    public void UpdateUIText(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timerText != null)
        {
            timerText.text = timerString;
        }
    }

    private void OnDestroy()
    {
        gameTimeManager.OnTimerExpired -= ShowEndGameLoosePopUp;
        LevelManager.Instance.onWinLevel -= ShowEndGameWinPopUp;
    }
}