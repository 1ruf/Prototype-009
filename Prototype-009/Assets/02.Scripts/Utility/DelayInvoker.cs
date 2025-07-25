using System;
using System.Collections;
using UnityEngine;

namespace GyeolUtility
{
    public class DelayInvoker<T>
    {
        public bool IsCompleted => _currentCount >= _targetCount;

        private Action<T> _callbackAction;
        private bool _paused;
        private float _delayTime;
        private int _targetCount;
        private int _currentCount;
        private T _genericValue;

        private float _lastTimerTick = 0f;
        private float _currentTimerTick = 0f;

        /// <summary>
        /// DelayIvoker생성자
        /// </summary>
        /// <param name="action">반복할 action</param>
        /// <param name="delayTime">지연 시간</param>
        /// <param name="repeatCount">반복 횟수(없으면 무한루프)</param>
        public DelayInvoker(Action<T> action, T value, float delayTime, int repeatCount = -1)
        {
            _callbackAction = action;
            _delayTime = delayTime;
            _targetCount = repeatCount;
            _genericValue = value;

            Initialize();
        }

        public void Pause() => _paused = true;

        public void Resume()
        {
            _paused = false;
            Flow();
        }

        public void Run()
        {
            if (_callbackAction == null)
                Debug.LogError($"DelayInvoker : 실행할 action이 없습니다.");
            else if (IsCompleted == false)
                Debug.LogWarning($"DelayInvoker : 이미 실행중인 DelayInvoker가 호출되었습니다.");
            else if (_paused == true)
                Debug.LogWarning($"DelayInvoker : 일시 정지상태입니다. Run() 대신 Resume() 함수를 호출하세요.");

            Flow();
        }

        private void Initialize()
        {
            _currentCount = 0;
        }

        private void Flow()
        {
            //시간 체크

            Invoke();
        }

        private void Invoke()
        {
            if (_currentCount <= _targetCount)
            {
                Debug.LogError($"DelayInvoker : 실행 횟수 초과로 인해 action이 실행되지 않음.");
                return;
            }
            else if (_paused == true)
                return;

            _currentCount++;
            _callbackAction(_genericValue);
        }
    }
}
