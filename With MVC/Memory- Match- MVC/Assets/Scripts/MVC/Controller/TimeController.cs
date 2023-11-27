using System;
using MVC.Core;
using MVC.Model;
using UnityEngine;

namespace MVC.Controller
{
    public class TimeController : MemoryMatchElement
    {
        [SerializeField] private float totalTime = 60f; // Total time for the timer in seconds
        private float currentTime;

        private bool isTimerRunning;
        //private UIManager uiManager; // Injected dependency

        public Action OnTimerExpired;
        private void Start()
        {
            currentTime = totalTime;
            App.controller.onWinLevel += StopTimer;
            StartTimer();
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
            App.uiView.UpdateUIText(currentTime);
            Debug.Log(currentTime);
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                App.uiView.UpdateUIText(currentTime);
                StopTimer();

                // Trigger event or perform actions when the timer expires
                OnTimerExpired?.Invoke();
                App.controller.SetState(GameState.GameOver);

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