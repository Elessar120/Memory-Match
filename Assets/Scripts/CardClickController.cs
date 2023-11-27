    using Enum;
    using Managers;
    using UnityEngine;
    using UnityEngine.EventSystems;
    public class CardClickController : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameManager.Instance.selectedCards.Count >= 2)
                return;
            
            if (GameManager.Instance.gameState == GameState.GameOver)
                return;
            
            Card card = eventData.pointerClick.GetComponent<Card>();
            GameManager.Instance.ChangeGameState(GameState.ResolvingMatch);
            GameManager.Instance.Flip(card);
                
            
               
        }
    }
