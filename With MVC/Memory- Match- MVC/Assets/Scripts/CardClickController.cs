    /*
    using Enum;
    using Managers;
    using UnityEngine;
    using UnityEngine.EventSystems;
    public class CardClickController : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameManager.Instance.selectedCards.Count < LevelManager.Instance.levels[GameManager.Instance.uiManager.selectedLevel - 1].matchCount)
            {
                if (GameManager.Instance.gameState != GameState.GameOver)
                {
                    Card card = eventData.pointerClick.GetComponent<Card>();
                    GameManager.Instance.ChangeGameState(GameState.ResolvingMatch);
                    GameManager.Instance.Flip(card);
                }
            }
               
        }
    }
    */
