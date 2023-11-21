using System;
using Enum;
using UnityEngine;

namespace DefaultNamespace
{
    public class AnimationController : MonoBehaviour
    {
        private Animator animator;

        public void StartFlipping(Animator animator)
        {
            this.animator = animator;
            animator.SetBool("isFlipping", true);
            Debug.Log(animator.name);

        }

        public void StartFlippingBack(Animator animator)
        {
            Debug.Log(Time.time);
            this.animator = animator;
            animator.SetBool("isFlipping", false);// todo: write animation parameter name in separate script
        }

        public void OnFlipFinished()
        {
            GameManager.Instance.ChangeGameState(GameState.WaitingForInput);
            GameManager.Instance.MatchCheck(GetComponent<Card>());

        }
        public void OnFlipBackFinished()
        {
            GameManager.Instance.ChangeGameState(GameState.WaitingForInput);
        }
        public void RunShrinkingAnimation(Animator animator)
        {
            animator.SetTrigger("isShrinking");// todo: set string name in inspector
        }

        public void OnShrinkFinished()
        {
            GameManager.Instance.DestroyMatchedCards();
            GameManager.Instance.ChangeGameState(GameState.WaitingForInput);
        }
    }
}