using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
    {
        [SerializeField] TimeManager gameTimeManager;
        [SerializeField] GameObject winGamePopup;
        [SerializeField] GameObject looseGamePopup;
        [SerializeField] GameObject endGameMainPanel;
        [SerializeField] LevelManager LevelManager;
        public int selectedLevel = 1;
        private static UIManager instance;

        public static UIManager Instance => instance;

        [SerializeField] TextMeshProUGUI timerText;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            gameTimeManager.OnTimerExpired += ShowEndGameLoosePopUp;
            LevelManager.onWinLevel += ShowEndGameWinPopUp;
            LevelManager.Instance.Init(selectedLevel);

        }
        private void ShowEndGameLoosePopUp()
        {
            ShowEndGameMainPanel();
            looseGamePopup.gameObject.SetActive(true);
        }

        public void SetSelectedLevel(int levelNumber)
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
            // Calculate minutes and seconds from total seconds
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            // Format the time as "MM:SS"
            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Update the UI Text with the formatted time
            if (timerText != null)
            {
                timerText.text = timerString;
            }
        }

        private void OnDestroy()
        {
            gameTimeManager.OnTimerExpired -= () => ShowEndGameLoosePopUp();
            LevelManager.onWinLevel -= ShowEndGameWinPopUp;
        }
    }