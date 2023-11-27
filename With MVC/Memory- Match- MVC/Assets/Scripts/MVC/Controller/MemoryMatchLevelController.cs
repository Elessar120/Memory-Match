using System;
using System.Collections.Generic;
using System.Linq;
using MVC.Core;
using MVC.Data;
using MVC.Model;
using MVC.Scriptable_Object;
using MVC.View;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Controller
{
    public class MemoryMatchLevelController : MemoryMatchElement
    {
        public Action onLevelGeneratingFinish;
        /*private void Start()
        {
            GenerateCards();
        }*/

        public LevelData levels;
         public void GenerateCards(int level = 1)
        {
            int levelIndex = level - 1;

            // Load levels from Resources/Levels folder
            //levels = Resources.Load<LevelData>("Levels/Level " + level);

            // Extract card data from the level
            List<CardData> cards = levels.cards;

            ConfigureGridLayoutGroup(levels);

            List<int> shuffledIndexes = GetShuffledIndexes(cards.Count);

            InstantiateCards(cards, shuffledIndexes, levels);
        }

        private void ConfigureGridLayoutGroup(LevelData level)
        {
            GridLayoutGroup gridLayoutGroup = App.uiView.gridLayoutGroup;
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
                    var newCard = Instantiate(level.card, App.uiView.spawnPoint.position, Quaternion.identity, App.uiView.spawnPoint);
                    MemoryMatchView cardView = newCard.GetComponent<MemoryMatchView>();
                    CardModel cardModel = newCard.GetComponent<CardModel>();
                    int index = indexes[i * level.itemsNumber + j];
                    cardModel.sprite = cards[index].cardFrontImage;
                    cardModel.back = cards[index].cardBackImage;
                    cardModel.id = cards[index].index;
                }
            }
            onLevelGeneratingFinish();
        }
    }
}