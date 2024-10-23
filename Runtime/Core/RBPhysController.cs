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

        public static void AddRigidbody(RBRigidbody rb)
        {
            _mainComputer.AddRigidbody(rb);
        }

        public static void RemoveRigidbody(RBRigidbody rb)
        {
            _mainComputer.RemoveRigidbody(rb);
        }

        public static void AddCollider(RBCollider c)
        {
            _mainComputer.AddCollider(c);
        }

        public static void RemoveCollider(RBCollider c)
        {
            _mainComputer.RemoveCollider(c);
        }

        public static void SwitchToCollider(RBCollider c)
        {
            _mainComputer.SwitchToCollider(c);
        }

        public static void SwitchToRigidbody(RBCollider c)
        {
            _mainComputer.SwitchToRigidbody(c);
        }

        public static void AddStdSolver(StdSolverInit stdInit, StdSolverIteration stdIter)
        {
            _mainComputer.AddStdSolver(stdInit, stdIter);
        }

        public static void RemoveStdSolver(StdSolverInit stdInit, StdSolverIteration stdIter)
        {
            _mainComputer.RemoveStdSolver(stdInit, stdIter);
        }

        public static void AddPhysObject(BeforeSolver beforeSolver, AfterSolver afterSolver)
        {
            _mainComputer.AddPhysObject(beforeSolver, afterSolver);
        }

        public static void RemovePhysObject(BeforeSolver beforeSolver, AfterSolver afterSolver)
        {
            _mainComputer.RemovePhysObject(beforeSolver, afterSolver);
        }

        public static void AddPhysValidatorObject(ValidatorBeforeSolver beforeSolver, ValidatorAfterSolver afterSolver)
        {
            _mainComputer.AddPhysValidatorObject(beforeSolver, afterSolver);
        }

        public static void RemovePhysValidatorObject(ValidatorBeforeSolver beforeSolver, ValidatorAfterSolver afterSolver)
        {
            _mainComputer.RemovePhysValidatorObject(beforeSolver, afterSolver);
        }

        public static void InitMainComputer()
        {
            _mainComputer?.Dispose();
            _mainComputer = new RBPhysComputer();
        }

        public static void DisposeMainComputer()
        {
            _mainComputer?.Dispose();
        }
    }
}
