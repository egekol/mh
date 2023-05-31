// 31052023

using FoodMatch.Managers;
using Managers;
using UnityEngine;

namespace FoodMatch.GamePlays
{
    public class InputRay : MonoBehaviour
    {
        [SerializeField] private LayerMask navMeshLayer;
        private Camera _mainCam;
        private FoodSpawner _spawner;
        private GameManager _gameManager;

        private void OnEnable()
        {
            IsEnabled = true;
            _mainCam = Camera.main;
        }

        private void Start()
        {
            _spawner = DependencyInjector.Instance.Resolve<FoodSpawner>();
            _gameManager = DependencyInjector.Instance.Resolve<GameManager>();
            _gameManager.Background.TurnOff();
            _gameManager.DefaultUI.TurnOn();
        }

        private void Update()
        {
            IsButtonDown = Input.GetMouseButtonDown(0);
            IsButton = Input.GetMouseButton(0);
            IsButtonUp = Input.GetMouseButtonUp(0);

            if (!IsEnabled)
            {
                return;
            }

            var ray = _mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, navMeshLayer))
            {
                // Debug.Log("Ray"+hit.transform.name,hit.transform);
                if (hit.transform.TryGetComponent(out Food food))
                {
                    // Debug.Log("hit: " + food, food);
                    if (IsButton)
                    {
                        food.IsRayHit = true;
                    }

                    if (IsButtonUp)
                    {
                        if (food.InSlot)
                        {
                            food.Release();
                            _spawner.Remove(food);
                        }
                        else
                        {
                            _spawner.CollectFood(food);
                        }
                    }
                }

                transform.position = hit.point;
            }
        }

        public bool IsButton { get; set; }

        public bool IsButtonUp { get; set; }

        public bool IsButtonDown { get; set; }

        public bool IsEnabled { get; set; } = true;
    }
}