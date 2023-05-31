using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using Managers;
using UnityEngine;

 public class IntroUI : MonoBehaviour
    {

        private GameManager _gameManager;

        private LeanWindow leanWindow;
        private WaitForSeconds _wait;
        public LeanWindow LeanWindow => leanWindow ??= GetComponentInChildren<LeanWindow>(true);
        private void OnEnable()
        {
            _wait = new WaitForSeconds(1f);
        }

        private void OnDisable()
        {
            // particleImage.onParticleFinish.RemoveListener(LoadMenu);
        }

        private IEnumerator Start()
        { 
            _gameManager = DependencyInjector.Instance.Resolve<GameManager>();
         LeanWindow.TurnOn();
         yield return _wait;
         LeanWindow.TurnOff();
         yield return _wait;
         LoadMenu();
        }

        private void LoadMenu()
        {
          
            // _gameManager. LoadSceneReference(_gameManager.gameSceneList.First(i=>i.Name=="G_LevelScene03"));
            _gameManager.LoadMainMenu();
        }
    }
