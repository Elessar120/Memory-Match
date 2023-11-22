using Enum;
using UnityEngine;
    public class AnimationController : MonoBehaviour
    {
        public void StartFlipping(Animator animator)
        {
            animator.SetBool(GameManager.Instance.flipAnimationName, true);
        }

        public void StartFlippingBack(Animator animator)
        {
            animator.SetBool(GameManager.Instance.flipAnimationName, false);
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
            animator.SetTrigger(GameManager.Instance.shrinkAnimationName);
        }

        public void OnShrinkFinished()
        {
            GameManager.Instance.DestroyMatchedCards();
            GameManager.Instance.ChangeGameState(GameState.WaitingForInput);
        }
    }
