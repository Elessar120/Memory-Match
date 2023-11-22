using UnityEngine;
    public class CardImageChanger : MonoBehaviour
    {
        private Card card;

        private void Start()
        {
            card = GetComponent<Card>();
        }

        public void ChangeImage()           
        {
            if (GetComponent<Animator>().GetBool(GameManager.Instance.flipAnimationName))
            {
                card.image.sprite = card.sprite;
            }
            else
            {
                card.image.sprite = card.back;
            }
        }
    }