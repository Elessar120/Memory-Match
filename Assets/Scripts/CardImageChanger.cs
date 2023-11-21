using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CardImageChanger : MonoBehaviour
    {
        private Card card;

        private void Start()
        {
            card = GetComponent<Card>();
        }

        public void ChangeImage()           
        {
            if (GetComponent<Animator>().GetBool("isFlipping"))
            {
                card.image.sprite = card.sprite;
                
            }
            else
            {
                card.image.sprite = card.back;
            }
        }
    }
}