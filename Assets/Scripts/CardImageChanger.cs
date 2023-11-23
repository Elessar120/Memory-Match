using Managers;
using UnityEngine;
    public class CardImageChanger : MonoBehaviour
    {
        [SerializeField] Card card;
        [SerializeField] private Animator animator;
        public void ChangeCardImage()           
        {
            if (animator.GetBool(GameManager.Instance.flipAnimationName))
            {
                card.image.sprite = card.sprite;
            }
            else
            {
                card.image.sprite = card.back;
            }
        }
    }