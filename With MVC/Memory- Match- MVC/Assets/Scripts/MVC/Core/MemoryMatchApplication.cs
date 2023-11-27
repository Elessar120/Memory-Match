using System.Collections.Generic;
using MVC.Controller;
using MVC.Model;
using MVC.View;
using UnityEngine;

namespace MVC.Core
{
    public class MemoryMatchApplication : MonoBehaviour
    {
        // Reference to the root instances of the MVC.
        [HideInInspector] public MemoryMatchModel model;
        [HideInInspector] public CardModel cardModel;
        [HideInInspector] public List<MemoryMatchView> memoryMatchView = new List<MemoryMatchView>();
        [HideInInspector] public AnimationView animationView;
        [HideInInspector] public UIView uiView;
        [HideInInspector] public MemoryMatchController controller;
        [HideInInspector] public MemoryMatchLevelController levelController;
        [HideInInspector] public TimeController timeController;

        // Init things here
        void OnEnable() 
        {
            model = GetComponentInChildren<MemoryMatchModel>();
            cardModel = GetComponentInChildren<CardModel>();
            animationView = GetComponentInChildren<AnimationView>();
            controller = GetComponentInChildren<MemoryMatchController>();
            uiView = GetComponentInChildren<UIView>();
            levelController = GetComponentInChildren<MemoryMatchLevelController>();
            memoryMatchView = new List<MemoryMatchView>();
            timeController = GetComponentInChildren<TimeController>();
        }

        public void Add(MemoryMatchView view)
        {
            memoryMatchView.Add(view);
        }
    }
}   
