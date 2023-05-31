// 31052023

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FoodMatch.GamePlays;
using Managers;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace FoodMatch.Managers
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] private FoodLevels levelListSo;
        private FoodListSO foodListSo;
        [SerializeField] private Collider spawnArea;
        [Space]
        [Header("Game Settings")]
        [SerializeField] private int cloneCount=3;
        [SerializeField] private Food foodBase;


        public Dictionary<int, List<Food>> SpawnedFoodDic { get; set; } = new Dictionary<int, List<Food>>();
        public Dictionary<int,List<Food>> CollectedFoodDic { get; set; }= new Dictionary<int, List<Food>>();
        public Action<Food> OnFoodCollect { get; set; }
        public Action OnFoodCombine { get; set; }

        private void Awake()
        {
            DependencyInjector.Instance.Register(this);
        }

        public void SpawnFoods()
        {
            for (int i = 0; i < foodListSo.FoodList.Count ; i++)
            {
                var foodList = new List<Food>();
                var collectFList = new List<Food>();
                var foodPrefab = foodListSo.FoodList[i];

                for (var j = 0; j < cloneCount; j++)
                {
                    var bounds = spawnArea.bounds;
                    var x = Random.Range(bounds.min.x, bounds.max.x);
                    var z = Random.Range(bounds.min.z, bounds.max.z);
                    var y = bounds.max.y;
                    Vector3 pos = Vector3.forward * z + Vector3.right * x+Vector3.up*y;
                    
                    var food = Instantiate(foodBase, pos, quaternion.identity, transform);
                    food.Index = i;
                    var tf = food.transform;
                    var gObject = Instantiate(foodPrefab,tf.position,tf.rotation, tf);
                    // gObject.transform.localPosition = Vector3.zero;
                    foodList.Add(food);
                    food.SetMesh();
                }
                SpawnedFoodDic.Add(i,foodList);
                CollectedFoodDic.Add(i,collectFList);
            }
        }

        private void Start()
        {
            foodListSo = levelListSo.LevelList.Mod(PlayerPrefKeys.FoodMatchLevel);
            SpawnFoods();
        }

        public void CollectFood(Food food)
        {
            CollectedFoodDic[food.Index].Add(food);
            OnFoodCollect?.Invoke(food);
        }

        public void TryCombine(Food food)
        {
            // Debug.Log("CollectedFoodDic[food.Index].Count  "+CollectedFoodDic[food.Index].Count);
            // Debug.Log("cloneCount  "+cloneCount);
            if (CollectedFoodDic[food.Index].Count>=cloneCount)
            {
                StartCoroutine(CombineFoods(food.Index));
            }
        }

        private IEnumerator CombineFoods(int foodIndex)
        {
            OnFoodCombine?.Invoke();
            var pos = 0f;
            for (var i = 0; i < CollectedFoodDic[foodIndex].Count; i++)
            {
                var food = CollectedFoodDic[foodIndex][i];
                food.transform.DOMove(food.transform.position + Vector3.up * 2, .3f).SetEase(Ease.OutQuad).SetLink(food.gameObject);
                pos += food.transform.position.x;
                Debug.Log("pos: "+pos);
            }

            pos /= CollectedFoodDic[foodIndex].Count;
            
            Debug.Log("Sum/3: "+pos);

            yield return new WaitForSeconds(.3f);
            for (var i = 0; i < CollectedFoodDic[foodIndex].Count; i++)
            {
                var food = CollectedFoodDic[foodIndex][i];
                var foodPos = food.transform.position;
                foodPos.x = pos;
                Debug.Log("foodPos.x  "+ foodPos.x);
                food.transform.DOMove(foodPos, .2f).SetEase(Ease.OutQuint).SetLink(food.gameObject);
                food.transform.DOLocalRotate(Random.insideUnitSphere*360f, .2f).SetEase(Ease.InOutSine).SetLink(food.gameObject);
                // pos += food.transform.position.x;
            }
            yield return new WaitForSeconds(.2f);
            foreach (var food in CollectedFoodDic[foodIndex])
            {
                food.OnFoodDestroy?.Invoke();
                Destroy(food.gameObject);
            }
            CollectedFoodDic[foodIndex].Clear();
            SpawnedFoodDic[foodIndex].Clear();
            CheckWinCondition();
        }


        private void CheckWinCondition()
        {
            foreach (var foodList in SpawnedFoodDic.Values)
            {
                if (foodList.Count!=0)
                {
                    return;
                }
            }
            LevelManager.InitLevelComplete();
            PlayerPrefKeys.FoodMatchLevel++;
        }

        public void Remove(Food food)
        {
            CollectedFoodDic[food.Index].Remove(food);
        }
    }
}