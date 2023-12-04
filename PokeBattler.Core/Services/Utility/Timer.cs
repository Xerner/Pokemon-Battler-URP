using System;
using UnityEngine;

namespace PokeBattler.Common
{
    public class Timer
    {
        float originalDuration = 0f;
        public float Duration { get; private set; }
        public bool Paused { get; set; }
        public Action OnComplete { get; set; }
        public Action OnStart { get; set; }
        public Action<float> OnTick { get; set; }

        public Timer(float duration = 0f, bool paused = false)
        {
            originalDuration = duration;
            Duration = duration;
            Paused = paused;
        }

        public void Tick()
        {
            if (Paused || Duration < 0f)
            {
                return;
            }
            Duration = Mathf.Clamp(Duration - Time.deltaTime, 0f, float.MaxValue);
            OnTick?.Invoke(Duration);
            if (Duration < 0f)
            {
                OnComplete?.Invoke();
            }
        }

        public void Pause() => Paused = true;
        public void Resume() => Paused = false;
        public void Toggle() => Paused = !Paused;

        public void SetDuration(float duration)
        {
            originalDuration = duration;
            Duration = duration;
            FireCompleteOrStart();
        }

        public void Reset()
        {
            Duration = originalDuration;
            FireCompleteOrStart();
        }

        public float CurrentTime()
        {
            return Duration;
        }

        public bool IsComplete()
        {
            return Duration <= 0f;
        }

        void FireCompleteOrStart()
        {
            if (Duration < 0f)
            {
                OnComplete?.Invoke();
                return;
            }
            if (Duration > 0f)
            {
                OnStart?.Invoke();
            }
        }
    }
}
