using System.Collections.Generic;
using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level", order = 0)]
    public class LevelData : ScriptableObject
    {
        public int itemsNumber;
        public int matchCount = 2;
        public int desiredRows; 
        public GameObject card;
        [Header("spacing Settings")]
        public int spacingX;
        public int spacingY;
        
        [Header("Padding Settings")]
        public int paddingLeft;
        public int paddingRight;
        public int paddingTop;
        public int paddingBottom;

        [Header("Cell Size")] 
        public int width;
        public int height;
        public List<CardData> cards;

    }
}