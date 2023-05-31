// 31052023

using UnityEngine;

namespace FoodMatch.GamePlays
{
    public class Food : MonoBehaviour
    {
        private Rigidbody rb;
        public Rigidbody Rb => rb ??= GetComponentInChildren<Rigidbody>(true);
        private MeshCollider meshCollider;
        public MeshCollider MeshCollider => meshCollider ??= GetComponentInChildren<MeshCollider>(true);
        // private Renderer renderer;
        // public Renderer Renderer => renderer ??= GetComponentInChildren<Renderer>(true);
        private MeshFilter meshFilter;
        public MeshFilter MeshFilter => meshFilter ??= GetComponentInChildren<MeshFilter>(true);
        public int Index { get; set; }
        private void Awake()
        {
            MeshCollider.sharedMesh = MeshFilter.sharedMesh;
        }
    }
}