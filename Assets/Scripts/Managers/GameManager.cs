using System.Collections.Generic;
using Enum;
using UnityEngine;
public class GameManager : MonoBehaviour
    {
        public string flipAnimationName;
        public string shrinkAnimationName;
        public GameState gameState; 
        public LevelManager levelManager;
        public TimeManager gameTimeManager;
        public AnimationController animationController;
        public float delayEndGamePopup = .5f;
        public float delayDestroyingCard = .5f;
        [HideInInspector] public List<Card> selectedCards = new List<Card>();
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            gameState = GameState.WaitingForInput;
            // Subscribe to the timer expired event
            gameTimeManager.OnTimerExpired += HandleTimerExpired;
            // Start the timer when the game begins
            gameTimeManager.StartTimer();
        }
        
        private void HandleTimerExpired()
        {
            gameState = GameState.GameOver;
        }
        public void MatchCheck(Card selectedCard)
        {
            if (gameState == GameState.WaitingForInput)
            {
                if (selectedCards.Count > 0)// this means it is not first selected cart
                {
                    selectedCards.Add(selectedCard);
                    if (selectedCards[0].id == selectedCards[1].id)
                    {
                        StartShrinking();
                        levelManager.WinCheck();
                    }
                    else
                    {
                        animationController.StartFlippingBack(selectedCards[0].GetComponent<Animator>());
                        animationController.StartFlippingBack(selectedCards[1].GetComponent<Animator>());
                    }
                    selectedCards.Clear();
                }
                else
                {
                    selectedCards.Add(selectedCard);
                }
            }
           
        }

        public void ChangeGameState(GameState newState)
        {
            gameState = newState;
        }
        public void Flip(Card selectedCard)
        {
            animationController.StartFlipping(selectedCard.GetComponent<Animator>());

        }
       

        private void StartShrinking()
        {

            foreach (Card card in selectedCards)
            {
                    animationController.RunShrinkingAnimation(card.GetComponent<Animator>());
            }
        }

        public void DestroyMatchedCards()
        {
            foreach (Card card in selectedCards)
            {
                Destroy(card.gameObject, delayDestroyingCard);
            }
        }
    }