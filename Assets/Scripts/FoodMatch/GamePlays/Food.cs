// 31052023

using System;
using DG.Tweening;
using FoodMatch.UI;
using Unity.Mathematics;
using UnityEngine;

namespace FoodMatch.GamePlays
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private float positionOffset=7;
        [SerializeField] private Vector3 rotateOffset;
        
        
        
        private Rigidbody rb;
        public Rigidbody Rb => rb ??= GetComponentInChildren<Rigidbody>(true);

        private FoodCollider foodCollider;
        public FoodCollider FoodCollider => foodCollider ??= GetComponentInChildren<FoodCollider>(true);
        private Renderer renderer;
        public Renderer Renderer => renderer ??= GetComponentInChildren<Renderer>(true);
        private MeshFilter meshFilter;
        public MeshFilter MeshFilter => meshFilter ??= GetComponentInChildren<MeshFilter>(true);
        public int Index { get; set; }
        public bool IsRayHit { get; set; }
        public Action OnFoodDestroy { get; set; }
        public bool InSlot { get; set; }
        public SlotUI Slot { get; set; }

        private float outlineTarget;
        private Material _material;

        private Vector3 _positionTarget;
        private Vector3 _rotationTarget;
        private Camera _mainCam;

        private void Start()
        {
            _material = Renderer.material;
            _mainCam = Camera.main;
        }

        private void Update()
        {
            outlineTarget = 0;
            _positionTarget = Vector3.zero;
            _rotationTarget = Vector3.zero;
            if (IsRayHit)
            {
                outlineTarget = .1f;
                var dist = _mainCam.transform.position-transform.position;
                _positionTarget = dist.normalized*positionOffset;
                _rotationTarget = rotateOffset;
            }

            // var lerp = Mathf.Lerp(0, _offsetTarget, Time.deltaTime * 6f);
            MeshFilter.transform.localPosition = Vector3.Lerp(MeshFilter.transform.localPosition,  _positionTarget, Time.deltaTime * 6f);
            MeshFilter.transform.localRotation =Quaternion.Lerp(MeshFilter.transform.localRotation,quaternion.Euler(_rotationTarget), Time.deltaTime * 6f); 
                // Quaternion.Euler(Vector3.Lerp(MeshFilter.transform.localRotation.eulerAngles,  positionOffset, Time.deltaTime * 6f));
            _material.SetFloat("_Thickness",
                Mathf.Lerp(_material.GetFloat("_Thickness"), outlineTarget, Time.deltaTime * 4f));
        }

        private void LateUpdate()
        {
            IsRayHit = false;
        }

        public void SetMesh()
        {
            FoodCollider.MeshCollider.sharedMesh = MeshFilter.sharedMesh;
            FoodCollider.Food = this;
        }

        public void SetKinematic()
        {
            Rb.isKinematic = true;
            InSlot = true;
        }

        public void Release()
        {
            transform.DOMove(Vector3.up * 6f, .5f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                Slot?.Unload();
                Rb.isKinematic = false;
                InSlot = false;
            });
           
           
        }
    }
}