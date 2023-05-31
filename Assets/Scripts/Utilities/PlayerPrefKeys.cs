// 15042023

using System;
using UnityEngine;

namespace Utilities
{
    public static class PlayerPrefKeys
    {
        public static int FoodMatchLevel
        {
            get => PlayerPrefs.GetInt("FoodMatchLevel", 0);
            set => PlayerPrefs.SetInt("FoodMatchLevel", value);
        }
       

       
    }
}