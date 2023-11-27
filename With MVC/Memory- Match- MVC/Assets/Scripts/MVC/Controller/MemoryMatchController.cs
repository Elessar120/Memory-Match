using System;
using System.Collections;
using System.Collections.Generic;
using MVC.Core;
using MVC.Model;
using MVC.View;
using UnityEngine;

namespace MVC.Controller
{
    public class MemoryMatchController: MemoryMatchElement
    {
        private int totalPossibleMatches;
        private MemoryMatchView view;
        public GameState gameState;
        public Action<Animator> onShrinkStart;
        public Action<Animator, bool> onFlipBackStart;
        public Action onWinLevel;

        [HideInInspector] public List<CardModel> selectedCards = new List<CardModel>();

        private void Start()
        {
            totalPossibleMatches = App.levelController.levels.itemsNumber;
            foreach (var view in App.memoryMatchView)
            {
                //view.onCardClicked += FlipCards;
                view.onFlipFinished += FlipCards;
                view.onShrinkFinished += DestroyMatchedCards;
            }

        }

        private void FlipCards(MemoryMatchView view)
        {
            this.view = view;
            CardModel card = view.GetComponent<CardModel>();
            if (gameState == GameState.WaitingForInput)
            {
                if (selectedCards.Count > 0) // This means it is not the first selected card
                {
                    selectedCards.Add(card);

                    if (selectedCards[0].id == selectedCards[1].id)
                    {
                        StartShrinking();
                        WinCheck();
                        Debug.Log("M");

                    }
                    else
                    {
                        Debug.Log("NM");
                        //animationController.StartFlippingBack(selectedCards[0].GetComponent<Animator>());
                       // animationController.StartFlippingBack(selectedCards[1].GetComponent<Animator>());
                       StartFlippingBack();
                    }

                    selectedCards.Clear();
                }
                else
                {
                    selectedCards.Add(card);
                }
            }
            Debug.Log("macth");
        }
        public void WinCheck()
        {
            totalPossibleMatches--;

            if (totalPossibleMatches == 0)
            {
                App.controller.SetState(GameState.GameOver);
                StartCoroutine(Delay(.5f));
            }
        }
        private IEnumerator Delay(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            onWinLevel?.Invoke();
        }
        private void StartShrinking()
        {
            foreach (CardModel card in selectedCards)
            {
                onShrinkStart(card.GetComponent<Animator>());
            }
        }

        private void StartFlippingBack()
        {
            foreach (CardModel card in selectedCards)
            {
                Animator animator = card.GetComponent<Animator>();
                onFlipBackStart(card.GetComponent<Animator>(), false);
            }
        }
        private void DestroyMatchedCards()
        {
            foreach (CardModel card in selectedCards)
            {
                Destroy(card.gameObject, .5f);
            }
        }
        public void SetState(GameState newState)
        {
            gameState = newState;
        }
    }
}