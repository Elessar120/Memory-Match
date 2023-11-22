using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
   [HideInInspector]public Image image;
   public Sprite sprite;
   public Sprite back;
   public int id;
   private void Start()
   {
      image = GetComponent<Image>();
   }
   
}
