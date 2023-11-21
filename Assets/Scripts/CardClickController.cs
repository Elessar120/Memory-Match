    using System;
    using System.Collections;
    using System.Collections.Generic;
    using DefaultNamespace;
    using Enum;
    using ScriptableObjects;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class CardClickController : MonoBehaviour, IPointerClickHandler
        
    {
        private Card data;

        // Start is called before the first frame update
        void Start()
        {
            data = GetComponentInParent<Card>();
        }
        
        // Update is called once per frame

        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameManager.Instance.selectedCards.Count < LevelManager.Instance.levels[0].matchCount)//todo: add index of selected level in place of 0
            {
                Card card = eventData.pointerClick.GetComponent<Card>();
                //GameManager.Instance.selectedCards.Add(card);
                GameManager.Instance.gameState = GameState.ResolvingMatch;
                GameManager.Instance.Flip(card);
                Debug.Log(GameManager.Instance.selectedCards.Count);
                //data.image.material.SetTexture();
            }
               
        }
    }
