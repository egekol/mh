// 31052023

using FoodMatch.GamePlays;
using UnityEngine;

namespace FoodMatch.UI
{
    public class SlotUI : MonoBehaviour
    {
        public bool IsFull { get; set; }
        public Food LoadedFood { get; set; }

        public void Set(Food food)
        {
            IsFull = true;
            LoadedFood = food;
            food.OnFoodDestroy += Unload;
            food.Slot = this;
        }

        public void Unload()
        {
            IsFull = false;
            LoadedFood = null;
        }
    }
}