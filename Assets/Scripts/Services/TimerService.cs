using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Services
{
    public class TimerService
    {
        public Action<int> SecondsPassedCountChanged = delegate { };

        public int SecondsPassedCount
        {
            get
            {
                return _secondsPassedCount;
            }
            set
            {
                if (value == _secondsPassedCount)
                {
                    return;
                }

                Debug.Log($"Seconds passed count changed from {_secondsPassedCount} to {value}");

                _secondsPassedCount = value;

                SecondsPassedCountChanged.Invoke(_secondsPassedCount);
            }
        }

        private int _secondsPassedCount;

        private Coroutine _timer;

        public void Initialize()
        {
            GameManager.Instance.GameStateService.SelectedGameStateChanged += SelectedGameStateChanged;
        }

        private void SelectedGameStateChanged(GameStateService.GameState gameState)
        {
            if (gameState == GameStateService.GameState.NotStarted)
            {
                // clear
                if (_timer != null)
                {
                    GameManager.Instance.StopCoroutine(_timer);
                    _timer = null;

                    SecondsPassedCount = 0;
                }

                // init
                _timer = GameManager.Instance.StartCoroutine(SecondCounting());
            }
        }

        private IEnumerator SecondCounting()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                SecondsPassedCount++;
            }
        }
    }
}
