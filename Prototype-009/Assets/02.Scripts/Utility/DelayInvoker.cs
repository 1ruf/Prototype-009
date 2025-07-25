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
        /// DelayIvoker������
        /// </summary>
        /// <param name="action">�ݺ��� action</param>
        /// <param name="delayTime">���� �ð�</param>
        /// <param name="repeatCount">�ݺ� Ƚ��(������ ���ѷ���)</param>
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
                Debug.LogError($"DelayInvoker : ������ action�� �����ϴ�.");
            else if (IsCompleted == false)
                Debug.LogWarning($"DelayInvoker : �̹� �������� DelayInvoker�� ȣ��Ǿ����ϴ�.");
            else if (_paused == true)
                Debug.LogWarning($"DelayInvoker : �Ͻ� ���������Դϴ�. Run() ��� Resume() �Լ��� ȣ���ϼ���.");

            Flow();
        }

        private void Initialize()
        {
            _currentCount = 0;
        }

        private void Flow()
        {
            //�ð� üũ

            Invoke();
        }

        private void Invoke()
        {
            if (_currentCount <= _targetCount)
            {
                Debug.LogError($"DelayInvoker : ���� Ƚ�� �ʰ��� ���� action�� ������� ����.");
                return;
            }
            else if (_paused == true)
                return;

            _currentCount++;
            _callbackAction(_genericValue);
        }
    }
}
