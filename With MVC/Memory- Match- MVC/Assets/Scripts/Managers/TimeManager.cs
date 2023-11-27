/*using System;
using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float totalTime = 60f; // Total time for the timer in seconds
        private float currentTime;

        private bool isTimerRunning;
        private UIManager uiManager; // Injected dependency

        public Action OnTimerExpired;

        public void Initialize(UIManager manager)
        {
            uiManager = manager;
            currentTime = totalTime;
            LevelManager.Instance.onWinLevel += StopTimer;
        }
        private void Update()
        {
            if (isTimerRunning)
            {
                UpdateTimer();
            }
        }

        private void UpdateTimer()
        {
            currentTime -= Time.deltaTime;
            uiManager.UpdateUIText(currentTime);
        
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                uiManager.UpdateUIText(currentTime);
                StopTimer();

                // Trigger event or perform actions when the timer expires
                OnTimerExpired?.Invoke();
            }
        }

        public void StartTimer()
        {
            isTimerRunning = true;
        }

        private void StopTimer()
        {
            isTimerRunning = false;
        }

        public void ResetTimer()
        {
            currentTime = totalTime;
        }

        public float GetRemainingTime()
        {
            return currentTime;
        }

        public bool IsTimerRunning()
        {
            return isTimerRunning;
        }
    }
}*/