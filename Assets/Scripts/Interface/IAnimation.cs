using UnityEngine;

namespace Interface
{
    public interface IAnimation
    {
        void StartFlippingBack(Animator animator);
        void StartFlipping(Animator animator);
        void RunShrinkingAnimation(Animator animator);
    }
}