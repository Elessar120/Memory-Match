using System;
using MVC.Core;
using MVC.Model;
using MVC.View;
using UnityEngine;

namespace MVC.Controller
{
    public class AnimationController : MemoryMatchElement
    {
        private void Start()
        {
            App.levelController.onLevelGeneratingFinish += Initial;
            App.levelController.GenerateCards();
            App.controller.onShrinkStart += RunShrinkingAnimation;
            App.controller.onFlipBackStart += StartFlippingBack;
        }

        private void Initial()
        {
            Debug.Log(App.memoryMatchView.Count);
            foreach (var view in App.memoryMatchView)
            {
                view.onCardClicked += StartFlipAnimation;
                view.onChangeCardSprite += SpriteChanger;
            }
            Debug.Log(App.memoryMatchView.Count);
        }
        private void StartFlipAnimation(Animator animator)
        {
            //Animator animator = view.GetComponent<Animator>();
            animator.SetBool("isFlipping", true);
            Debug.Log("SET");
        }

        private void SpriteChanger(MemoryMatchView view)
        {
            Animator animator = view.GetComponent<Animator>();
            if (animator.GetBool(view.FlipCardAnimationName))
            {
                view.image.sprite = view.GetComponent<CardModel>().sprite;
            }
            else
            {
                view.image.sprite = view.GetComponent<CardModel>().back;
            }
        }
        private void RunShrinkingAnimation(Animator animator)
        {
            animator.SetTrigger(App.memoryMatchView[0].ShrinkCardAnimationName);
        }

        private void StartFlippingBack(Animator animator, bool isFlipping)
        {
            animator.SetBool(App.memoryMatchView[0].FlipCardAnimationName, isFlipping);
        }
    }
}