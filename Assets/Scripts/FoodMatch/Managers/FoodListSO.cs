// 31052023

using System.Collections.Generic;
using UnityEngine;

namespace FoodMatch.Managers
{
    [CreateAssetMenu(fileName = "FoodList", menuName = "SO/FoodMatch", order = 0)]
    public class FoodListSO : ScriptableObject
    {
        public List<GameObject> FoodList;
    }
}