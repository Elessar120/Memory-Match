using System;
using System.Collections;
using System.Collections.Generic;
using Enum;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
   public Sprite sprite;
   public Sprite back;
   public cardState state;
   [HideInInspector]public Image image;
   public int id;
   private void Start()
   {
      image = GetComponent<Image>();
   }
   
}
