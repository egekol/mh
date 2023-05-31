// 31052023

using System.Collections.Generic;
using UnityEngine;

namespace FoodMatch.Managers
{
    [CreateAssetMenu(fileName = "FoodLevels", menuName = "SO/FoodMatchLevels", order = 0)]
    public class FoodLevels : ScriptableObject
    {
        public List<FoodListSO> LevelList;
    }
}