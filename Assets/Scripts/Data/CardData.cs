using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Data
{
    [Serializable]
    public class CardData
    {
        public int index;
        public Sprite cardFrontImage;
        [FormerlySerializedAs("cardRearImage")] public Sprite cardBackImage;
        
    }
}