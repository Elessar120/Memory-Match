using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using CardData = DefaultNamespace.Data.CardData;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [ExecuteInEditMode] // This attribute allows the script to run in Edit mode
    public class LevelManager : MonoBehaviour
    {
        public GridLayoutGroup gridLayoutGroup;
        private static LevelManager instance; 
        public static LevelManager Instance => instance;
        public Timer gameTimer;
        public LevelData[] levels;
        public Transform spawnPoint;
        public Action OnWinLevel;
        private int totalPossibleMatches;// todo: choose a better name
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

  public void GenerateCards(int level = 1)
{
    int levelIndex = level - 1;

    // Load levels from Resources/Levels folder
    levels = Resources.LoadAll("Levels", typeof(LevelData)).Cast<LevelData>().ToArray();
    Debug.Log(levels.Length);

    // Create a list to hold the cards
    List<CardData> cards = new List<CardData>();
    cards = levels[levelIndex].cards;

    // Create a list to hold the shuffled cards
    List<CardData> shuffledCards = new List<CardData>();
// Assuming you have a List<CardData> named cards
  

    Vector2 cellSize = gridLayoutGroup.cellSize;
    cellSize.x = levels[levelIndex].width;
    gridLayoutGroup.cellSize = cellSize;

    Vector2 spacing = gridLayoutGroup.spacing;
    spacing.x = levels[levelIndex].spacingX;
    spacing.y = levels[levelIndex].spacingY;
    gridLayoutGroup.spacing = spacing;

    var padding = gridLayoutGroup.padding;
    padding.left = levels[levelIndex].paddingLeft;
    padding.right = levels[levelIndex].paddingRight;
    padding.top = levels[levelIndex].paddingTop;
    padding.bottom = levels[levelIndex].paddingBottom;
    gridLayoutGroup.padding = padding;

    gridLayoutGroup.constraintCount = levels[levelIndex].desiredRows;
    List<int> indexes = new List<int>();
    for (int i = 0; i < cards.Count; i++)
    {
        indexes.Add(i);
    }

// Fisher-Yates shuffle
    System.Random rng = new System.Random();
    int n = indexes.Count;
    while (n > 1)
    {
        n--;
        int k = rng.Next(n + 1);
        int value = indexes[k];
        indexes[k] = indexes[n];
        indexes[n] = value;
    }

// Instantiate the cards
    for (int i = 0; i < levels[levelIndex].matchCount; i++)
    {
        for (int j = 0; j < levels[levelIndex].itemsNumber; j++)
        {
            var newCard = Instantiate(levels[levelIndex].card.gameObject, spawnPoint.transform.position, Quaternion.identity);
            newCard.transform.SetParent(spawnPoint);

            Card card = newCard.GetComponent<Card>();
            int index = indexes[i * levels[levelIndex].itemsNumber + j];
            // Set the card properties from the shuffled cards list
            card.sprite = cards[index].cardFrontImage;
            card.back = cards[index].cardBackImage;
            card.id = cards[index].index;
        }
    }
}
        public void Init(int levelNumber)
        {
            GenerateCards(levelNumber);
            totalPossibleMatches = levels[levelNumber - 1].itemsNumber;
        }

        public void WinCheck()
        {
            totalPossibleMatches--;
            Debug.Log(totalPossibleMatches);
            if (totalPossibleMatches == 0)
            {
                StartCoroutine(Delay(GameManager.Instance.delayEndGamePopup));
            }
        }

        IEnumerator Delay(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            OnWinLevel();
        }
    }
}