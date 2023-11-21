using System;
using UnityEngine;

namespace DefaultNamespace
{
    using UnityEngine;

    public class Timer : MonoBehaviour
    {
        public float totalTime = 60f; // Total time for the timer in seconds
        private float currentTime;

        private bool isTimerRunning = false;

        public Action OnTimerExpired;
        private void Start()
        {
            currentTime = totalTime;
            LevelManager.Instance.OnWinLevel += StopTimer;
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
            UIManager.Instance.UpdateUIText(currentTime);
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                UIManager.Instance.UpdateUIText(currentTime);
                StopTimer();

                // Trigger event or perform actions when the timer expires
                OnTimerExpired ?.Invoke();// todo: remove it later
                
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