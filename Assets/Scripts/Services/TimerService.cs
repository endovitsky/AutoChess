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

                _secondsPassedCount = value;

                SecondsPassedCountChanged.Invoke(_secondsPassedCount);
            }
        }

        private int _secondsPassedCount;

        private Coroutine _timer;

        public void Initialize()
        {
            _timer = GameManager.Instance.StartCoroutine(SecondCounting());
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
