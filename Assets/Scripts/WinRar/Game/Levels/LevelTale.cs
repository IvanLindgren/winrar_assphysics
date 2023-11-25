using System.Collections;
using UnityEngine;

namespace WinRar.Game.Levels
{
    public class LevelTale : LevelController
    {
        [SerializeField] private int _timerSecondsCount;

        public void StartTimer()
        {
            StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            int timer = _timerSecondsCount;

            while (timer > 0)
            {
                yield return new WaitForSeconds(1f);
                timer--;
            }
        }
    }
}