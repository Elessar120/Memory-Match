using System.Collections.Generic;
using Enum;
using UnityEngine;
using UnityEngine.Serialization;
namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public GameState gameState; 
        public LevelManager levelManager;
        [HideInInspector] public List<Card> selectedCards = new List<Card>();
        public Timer gameTimer;
        public AnimationController animationController;
        private Card data;
        public float delayEndGamePopup = .5f;
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
            levelManager.Init(UIManager.Instance.selectedLevel);//todo: move to UI Manager
            // Subscribe to the timer expired event
            gameTimer.OnTimerExpired += HandleTimerExpired;

            // Start the timer when the game begins
            gameTimer.StartTimer();
        }

        private void Update()
        {
            // You can check the remaining time and perform actions based on it
            float remainingTime = gameTimer.GetRemainingTime();
            if (remainingTime <= 10f)
            {
               // Debug.Log("Hurry up! Only " + remainingTime + " seconds left!");
            }
        }

        private void HandleTimerExpired()
        {
            gameState = GameState.GameOver;
            Debug.Log("Game Over! Timer Expired.");
            // Implement game over logic or other actions when the timer expires
        }
        public void MatchCheck(Card selectedCard)
        {
            data = selectedCard;
            if (gameState == GameState.WaitingForInput)
            {
                if (selectedCards.Count > 0)// this means it is not first selected cart
                {
                    selectedCards.Add(selectedCard);
                    //animationController.StartFlipping(selectedCard.GetComponent<Animator>());
                    selectedCard.state = cardState.Flipped;
                    if (selectedCards[0].id == selectedCards[1].id)
                    {
                        StartShrinking();
                        levelManager.WinCheck();
                        //todo: combo
                        AddScore();// if combo{AddScore()}, else {AddScore()}
                        ShowScorePopup();
                    }
                    else
                    {
                        animationController.StartFlippingBack(selectedCards[0].GetComponent<Animator>());
                        animationController.StartFlippingBack(selectedCards[1].GetComponent<Animator>());
                        // todo: implement combo and minus score
                    }
                    selectedCards.Clear();
                }
                else
                {
                    selectedCards.Add(selectedCard);
                    //animationController.StartFlipping(selectedCard.GetComponent<Animator>());
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
                Destroy(card.gameObject, .5f);
            }
        }
        private void ShowScorePopup()
        {
            Debug.Log("Pop Up Show");
        }

        private void AddScore()
        {
            Debug.Log("Score Added");
        }
    }
}