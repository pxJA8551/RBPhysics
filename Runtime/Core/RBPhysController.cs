using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static RBPhys.RBPhysComputer;

namespace RBPhys
{
    public static class RBPhysController
    {
        static RBPhysComputer _mainComputer = new RBPhysComputer();

        public static RBPhysComputer MainComputer { get { return _mainComputer; } }

        static RBPhysController()
        {
            InitMainComputer();
        }

        public static async Task ChangeTimeScaleMode(TimeScaleMode timeScaleMode, int fadeLengthMs = 0)
        {
            if (_mainComputer == null) return;

            if (fadeLengthMs > 0)
            {
                if (_mainComputer.PhysTimeScaleMode == timeScaleMode) return;

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
        }

        public static void InitMainComputer()
        {
            _mainComputer?.Dispose();
            _mainComputer = new RBPhysComputer();
        }

        public static void DisposeMainComputer()
        {
            _mainComputer?.WaitSemaphore(1500);
            _mainComputer?.Dispose();
        }

        public static void ReInitializeMainComputer()
        {
            _mainComputer.ReInitializeComputer();
        }
    }
}
