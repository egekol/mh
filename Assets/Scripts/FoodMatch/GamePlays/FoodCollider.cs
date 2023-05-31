// 31052023

using UnityEngine;

namespace FoodMatch.GamePlays
{
    public class FoodCollider : MonoBehaviour
    {
        public Food Food { get; set; }
        private MeshCollider meshCollider;
        public MeshCollider MeshCollider => meshCollider ??= GetComponentInChildren<MeshCollider>(true);
    }
}