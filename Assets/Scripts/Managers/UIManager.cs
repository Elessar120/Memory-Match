using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TimeManager gameTimeManager;
        [SerializeField] private GameObject winGamePopup;
        [SerializeField] private GameObject looseGamePopup;
        [SerializeField] private GameObject endGameMainPanel;
        [SerializeField] private TextMeshProUGUI timerText;

        public int selectedLevel = 1;
        public void Initialize()
        {
            gameTimeManager.OnTimerExpired += ShowEndGameLoosePopUp;
            LevelManager.Instance.onWinLevel += ShowEndGameWinPopUp;
            LevelManager.Instance.Initialize(selectedLevel);
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
}