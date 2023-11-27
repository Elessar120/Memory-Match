using System;
using Interface;
using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float totalTime = 60f; // Total time for the timer in seconds
        private float currentTime;
        private bool isTimerRunning;
        public Action OnTimerExpired;
        private IUpdateUI UIInstance;
        public void Start()
        {
            currentTime = totalTime;
            LevelManager.Instance.onWinLevel += StopTimer;
            UIInstance = FindObjectOfType<UIManager>();
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
            UIInstance.UpdateUIText(currentTime);
        
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                UIInstance.UpdateUIText(currentTime);
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
}