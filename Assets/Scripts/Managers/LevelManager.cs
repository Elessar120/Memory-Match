using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enum;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    [ExecuteInEditMode]
    public class LevelManager : MonoBehaviour
    {
        public GridLayoutGroup gridLayoutGroup;
        private static LevelManager instance;
        public static LevelManager Instance => instance;
        public LevelData[] levels;
        public Transform spawnPoint;
        public Action onWinLevel;
        private int totalPossibleMatches;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void Initialize(int levelNumber)
        {
            GenerateCards(levelNumber);
            totalPossibleMatches = levels[levelNumber - 1].itemsNumber;
        }

        public void WinCheck()
        {
            totalPossibleMatches--;

            if (totalPossibleMatches == 0)
            {
                GameManager.Instance.ChangeGameState(GameState.GameOver);
                StartCoroutine(Delay(GameManager.Instance.delayEndGamePopup));
            }
        }

        private IEnumerator Delay(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            onWinLevel?.Invoke();
        }

        public void GenerateCards(int level = 1)
        {
            int levelIndex = level - 1;

            // Load levels from Resources/Levels folder
            levels = Resources.LoadAll<LevelData>("Levels");

            // Extract card data from the level
            List<CardData> cards = levels[levelIndex].cards;

            ConfigureGridLayoutGroup(levels[levelIndex]);

            List<int> shuffledIndexes = GetShuffledIndexes(cards.Count);

            InstantiateCards(cards, shuffledIndexes, levels[levelIndex]);
        }

        private void ConfigureGridLayoutGroup(LevelData level)
        {
            gridLayoutGroup.cellSize = new Vector2(level.width, level.height);
            gridLayoutGroup.spacing = new Vector2(level.spacingX, level.spacingY);

            RectOffset padding = gridLayoutGroup.padding;
            padding.left = level.paddingLeft;
            padding.right = level.paddingRight;
            padding.top = level.paddingTop;
            padding.bottom = level.paddingBottom;
            gridLayoutGroup.padding = padding;

            gridLayoutGroup.constraintCount = level.desiredRows;
        }

        private List<int> GetShuffledIndexes(int count)
        {
            List<int> indexes = Enumerable.Range(0, count).ToList();
            indexes.Shuffle();
            return indexes;
        }

        private void InstantiateCards(List<CardData> cards, List<int> indexes, LevelData level)
        {
            for (int i = 0; i < level.matchCount; i++)
            {
                for (int j = 0; j < level.itemsNumber; j++)
                {
                    var newCard = Instantiate(level.card, spawnPoint.position, Quaternion.identity, spawnPoint);
                    Card card = newCard.GetComponent<Card>();
                    int index = indexes[i * level.itemsNumber + j];
                    card.sprite = cards[index].cardFrontImage;
                    card.back = cards[index].cardBackImage;
                    card.id = cards[index].index;
                }
            }
        }
    }
}
