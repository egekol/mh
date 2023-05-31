// 31052023

using System.Collections.Generic;
using FoodMatch.GamePlays;
using UnityEngine;

namespace FoodMatch.Managers
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] private FoodListSO foodListSo;
        [SerializeField] private Collider spawnArea;
        [Space]
        [Header("Game Settings")]
        [SerializeField] private int cloneCount=3;


        public Dictionary<int, Food> SpawnedFoodDic { get; set; } = new Dictionary<int, Food>();
        public Dictionary<int,Food> CollectedFoodDic { get; set; }= new Dictionary<int, Food>();
        
    }
}