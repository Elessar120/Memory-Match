using MVC.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.View
{
    public class UIView : MemoryMatchElement
    {
        public GridLayoutGroup gridLayoutGroup; 
        public Transform spawnPoint;
        public int selectedLevel;
        [SerializeField] private GameObject winGamePopup;
        [SerializeField] private GameObject looseGamePopup;
        [SerializeField] private GameObject endGameMainPanel;
        [SerializeField] private TextMeshProUGUI timerText;

        private void Start()
        {
            App.controller.onWinLevel += ShowEndGameWinPopUp;
            App.timeController.OnTimerExpired += ShowEndGameLoosePopUp;

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
    }
}