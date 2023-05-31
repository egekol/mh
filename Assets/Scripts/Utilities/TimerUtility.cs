using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class TimerUtility
    {
        private bool isPaused;
        private bool onlyOneTime;
        public float timer;
        public float countdownTime;


        public Action OnTimeEnded;

        public TimerUtility(float duration, bool onlyOneTime = true)
        {
            this.onlyOneTime = onlyOneTime;
            countdownTime = duration;
            InitializeTimer();
        }

        public void InitializeTimer()
        {
            isPaused = false;
            if (countdownTime == 0) return;
            timer = countdownTime;
            if (onlyOneTime)
            {
                OnTimeEnded += StopForOnlyTime;
            }
        }

        private void StopForOnlyTime()
        {
            isPaused = true;
            OnTimeEnded -= StopForOnlyTime;
        }

        public void CountTimer()
        {
            if (isPaused)
            {
                return;
            }

            if (timer <= -.01f)
            {
                timer += countdownTime;
                OnTimeEnded?.Invoke();
            }


            timer -= Time.deltaTime;
        }

        public void ResetTimer()
        {
            timer = countdownTime;
            isPaused = false;
        }
    }
}