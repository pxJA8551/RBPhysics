using System;
using System.Threading.Tasks;
using UnityEngine;

namespace RBPhys
{
    public static class RBPhysController
    {
        static RBPhysComputer _mainComputer = new RBPhysComputer(false);

        public static RBPhysComputer MainComputer { get { return _mainComputer; } }

        public static Action<TimeScaleMode> onTimeScaleModeChanged;

        static RBPhysController()
        {
            InitMainComputer();
        }

        public static async Task ChangeTimeScaleModeAsync(TimeScaleMode timeScaleMode, int fadeLengthMs = 0)
        {
            if (_mainComputer == null) return;

            if (_mainComputer.PhysTimeScaleMode == timeScaleMode) return;

            if (await _mainComputer.WaitSemaphoreAsync(500))
            {
                if (fadeLengthMs > 0)
                {
                    float wt0 = Time.unscaledTime * 1000f;
                    while (true)
                    {
                        float s = (Time.unscaledTime * 1000f) - wt0;

                        float ts = ((float)s / fadeLengthMs);
                        Time.timeScale = Mathf.Lerp(1, 0, ts);

                        if (_mainComputer == null) return;

                        if (fadeLengthMs < s)
                        {
                            _mainComputer.PhysTimeScaleMode = timeScaleMode;
                            break;
                        }

                        await Task.Delay(1);
                    }

                    float wt1 = Time.unscaledTime * 1000f;
                    while (true)
                    {
                        float s = (Time.unscaledTime * 1000f) - wt1;

                        float ts = (s / fadeLengthMs);
                        Time.timeScale = Mathf.Lerp(1, 0, (1 - ts));

                        if (fadeLengthMs < s)
                        {
                            Time.timeScale = 1;
                            break;
                        }

                        await Task.Delay(1);
                        wt1++;
                    }
                }
                else
                {
                    _mainComputer.PhysTimeScaleMode = timeScaleMode;
                }

                if (onTimeScaleModeChanged != null) onTimeScaleModeChanged(timeScaleMode);

                _mainComputer.ReleaseSemaphore();
            }
            else
            {
                throw new Exception();
            }
        }

        public static void InitMainComputer()
        {
            _mainComputer?.Dispose();
            _mainComputer = new RBPhysComputer(false);
        }

        public static void DisposeMainComputer()
        {
            if (_mainComputer?.WaitSemaphore(500) ?? false)
            {
                _mainComputer.Dispose();
            }
        }

        public static void ReInitializeMainComputer()
        {
            _mainComputer.ReInitializeComputer();
        }
    }
}
