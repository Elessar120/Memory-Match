using MVC.Core;
using UnityEngine;

namespace MVC.Model
{
    public class MemoryMatchModel: MemoryMatchElement
    {
        [HideInInspector] public CardModel cardModel;
        private void OnEnable()
        {
            cardModel = FindObjectOfType<CardModel>();
        }
    }
}