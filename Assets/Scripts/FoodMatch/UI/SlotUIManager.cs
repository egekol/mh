// 31052023

using System;
using System.Collections.Generic;
using DG.Tweening;
using FoodMatch.GamePlays;
using FoodMatch.Managers;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace FoodMatch.UI
{
    public class SlotUIManager : MonoBehaviour
    {
        [SerializeField] private List<SlotUI> slotList;
        private FoodSpawner _foodSpawner;
        public TimerUtility comboTimer;
        [SerializeField] private Image timerUI;
        [SerializeField] private Image timerFill;
        [SerializeField] private TextMeshProUGUI timerText;
        private int _comboCount;
        private void Start()
        {
            _foodSpawner = DependencyInjector.Instance.Resolve<FoodSpawner>();
            _foodSpawner.OnFoodCollect += LoadSlot;
            _foodSpawner.OnFoodCombine += Combo;
            comboTimer.OnTimeEnded += StopCombo;
            LevelManager.LevelComplete += StopCombo;
        }

        private void StopCombo()
        {
            timerUI.gameObject.SetActive(false);
        }

        private void Combo()
        {
            if (_comboCount==0)
            {
                timerUI.gameObject.SetActive(true);
            }
            comboTimer.ResetTimer();
            _comboCount++;
            timerText.text = "x"+_comboCount;
        }

        private void Update()
        {
            if (_comboCount!=0)
            {
                comboTimer.CountTimer();
                timerFill.fillAmount = comboTimer.timer / comboTimer.countdownTime;
            }
        }

        private void LoadSlot(Food obj)
        {
            foreach (var slotUI in slotList)
            {
                if (slotUI.IsFull)
                    continue;
                slotUI.Set(obj);
                obj.SetKinematic();
                obj.transform.DOMove(slotUI.transform.position,.3f).SetEase(Ease.OutQuad).OnComplete(() => _foodSpawner.TryCombine(obj)).SetLink(obj.gameObject);
                return;
            }
        }
    }
}