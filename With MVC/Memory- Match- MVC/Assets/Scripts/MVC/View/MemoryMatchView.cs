using System;
using MVC.Core;
using MVC.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MVC.View
{
    public class MemoryMatchView: MemoryMatchElement, IPointerClickHandler
    {
        public Action<Animator> onCardClicked;
        public Action<MemoryMatchView> onChangeCardSprite;
        public Action<MemoryMatchView> onFlipFinished;
        public Action onShrinkFinished;
        public Action onFlipBackStart;
        public string FlipCardAnimationName;
        public string ShrinkCardAnimationName;
        public Image image;
      
        private void Awake()
        {
            App.Add(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (App.controller.selectedCards.Count >= App.levelController.levels/*[App.uiView.selectedLevel - 1]*/.matchCount)
                return;
            if (App.controller.gameState == GameState.GameOver)
                return;
            Animator animator = GetComponent<Animator>();
            App.controller.SetState(GameState.ResolvingMatch);
            onCardClicked(animator);
            Debug.Log("Click");
        }
        public void ChangeCardSpriteEvent()
        {
            onChangeCardSprite(this);
        }
        public void OnFlipFinished()
        {
            App.controller.SetState(GameState.WaitingForInput);
            onFlipFinished(this);
        }
        public void OnShrinkFinished()
        {
            onShrinkFinished();
            App.controller.SetState(GameState.WaitingForInput);
        }

        public void OnFlipBackFinished()
        {
            App.controller.SetState(GameState.WaitingForInput);
        }
    }
}