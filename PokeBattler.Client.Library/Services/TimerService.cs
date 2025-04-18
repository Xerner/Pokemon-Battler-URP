using System;

namespace AutoChess.Library.Services
{
    public class TimerService
    {
        float originalDuration = 0f;
        public float Duration { get; private set; }
        public bool Paused { get; set; }
        public Action OnComplete { get; set; }
        public Action OnStart { get; set; }
        public Action<float> OnTick { get; set; }

        public TimerService(float duration = 0f, bool paused = false)
        {
            originalDuration = duration;
            Duration = duration;
            Paused = paused;
        }

        public void Tick(float deltaTime)
        {
            if (Paused || Duration < 0f)
            {
                return;
            }
            Duration = Math.Clamp(Duration - deltaTime, 0f, float.MaxValue);
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
