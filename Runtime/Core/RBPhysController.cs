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
        static List<RBPhysComputer> _subComputers = new List<RBPhysComputer>();

        public static RBPhysComputer MainComputer { get { return _mainComputer; } }
        public static List<RBPhysComputer> SubComputers { get { return _subComputers; } }

        static RBPhysController()
        {
            InitMainComputer();
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

        public static void AddStdSolver(RBConstraints.IStdSolver solver)
        {
            _mainComputer.AddStdSolver(solver);
        }

        public static void RemoveStdSolver(RBConstraints.IStdSolver solver)
        {
            _mainComputer.RemoveStdSolver(solver);
        }

        public static void AddPhysObject(RBConstraints.IRBPhysObject physObj, bool asyncIteration = true)
        {
            _mainComputer.AddPhysObject(physObj, asyncIteration);
        }

        public static void RemovePhysObject(RBConstraints.IRBPhysObject physObj)
        {
            _mainComputer.RemovePhysObject(physObj);
        }

        public static void AddPhysValidatorObject(RBConstraints.IRBPhysObject physObj)
        {
            _mainComputer.AddPhysValidatorObject(physObj);
        }

        public static void RemovePhysValidatorObject(RBConstraints.IRBPhysObject physObj)
        {
            _mainComputer.RemovePhysValidatorObject(physObj);
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

        public static void DisposeSubComputers()
        {
            foreach (var s in _subComputers)
            {
                s?.Dispose();
            }
        }
    }
}
