using Enum;
using Managers;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public void StartFlipping(Animator animator)
    {
        SetFlipAnimation(animator, true);
    }

    public void StartFlippingBack(Animator animator)
    {
        SetFlipAnimation(animator, false);
    }

    private void SetFlipAnimation(Animator animator, bool isFlipping)
    {
        animator.SetBool(GameManager.Instance.flipAnimationName, isFlipping);
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