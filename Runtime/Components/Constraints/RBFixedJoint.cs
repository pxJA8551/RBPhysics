using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace RBPhys
{
    [RequireComponent(typeof(RBRigidbody))]
    public class RBFixedJoint : MonoBehaviour, RBPhysComputer.RBConstraints.IStdSolver
    {
        const float SOLVER_LINEAR_BETA = 0.25f;
        const float SOLVER_ANGULAR_BETA = 0.25f;

        [SerializeField] public new RBRigidbody rigidbody;
        [SerializeField] public RBRigidbody pairRigidbody;
        [SerializeField] public Vector3 local_rb_contact;
        [SerializeField] public Vector3 local_rb_pair_contact;
        [SerializeField] public Vector3 local_rb_joint_dir;
        [SerializeField] public Vector3 local_rb_pair_joint_dir;

        Jacobian _jN = new Jacobian();
        Jacobian _jT = new Jacobian();
        Jacobian _jB = new Jacobian();

        private void Awake()
        {
            RBPhysController.AddStdSolver(this);
        }

        private void OnDestroy()
        {
            RBPhysController.RemoveStdSolver(this);
        }

        public void StdSolverInit(float dt, bool isPrimaryInit)
        {
            //local_rb_contact_rot *= Quaternion.Euler(new Vector3(0.01f, 0, 0));

            Vector3 ws_rb_contact = rigidbody.Position + rigidbody.Rotation * local_rb_contact;
            Vector3 ws_rb_pair_contact = pairRigidbody != null ? pairRigidbody.Position + pairRigidbody.Rotation * local_rb_pair_contact : ws_rb_contact;
            Vector3 ws_jointDir = rigidbody.Rotation * (local_rb_joint_dir == Vector3.zero ? Vector3.up : local_rb_joint_dir);
            Vector3 ws_jointDir_pair = pairRigidbody.Rotation * (local_rb_pair_joint_dir == Vector3.zero ? Vector3.up : local_rb_pair_joint_dir);

            Vector3 normal = ws_rb_pair_contact - ws_rb_contact;
            Vector3 tangent = Vector3.zero;
            Vector3 binormal = Vector3.zero;

            Vector3.OrthoNormalize(ref normal, ref tangent, ref binormal);

            _jN.Init(rigidbody, pairRigidbody, ws_rb_contact, ws_rb_pair_contact, ws_jointDir, ws_jointDir_pair, normal, dt, isPrimaryInit);
            _jT.Init(rigidbody, pairRigidbody, ws_rb_contact, ws_rb_pair_contact, ws_jointDir, ws_jointDir_pair, tangent, dt, isPrimaryInit);
            _jB.Init(rigidbody, pairRigidbody, ws_rb_contact, ws_rb_pair_contact, ws_jointDir, ws_jointDir_pair, binormal, dt, isPrimaryInit);
        }

        public void StdSolverIteration(int iterCount)
        {
            Solve(out Vector3 vAdd_a, out Vector3 avAdd_a, out Vector3 vAdd_b, out Vector3 avAdd_b);

            if (rigidbody != null)
            {
                rigidbody.PhysAwake();
                rigidbody.ExpVelocity += vAdd_a;
                rigidbody.ExpAngularVelocity += avAdd_a;
            }

            if (pairRigidbody != null)
            {
                pairRigidbody.PhysAwake();
                pairRigidbody.ExpVelocity += vAdd_b;
                pairRigidbody.ExpAngularVelocity += avAdd_b;
            }

            //必要に応じてソルバー途中終了用のコードを追加
        }

        void Solve(out Vector3 aAdd, out Vector3 aAngAdd, out Vector3 bAdd, out Vector3 bAngAdd)
        {
            aAdd = Vector3.zero;
            aAngAdd = Vector3.zero;
            bAdd = Vector3.zero;
            bAngAdd = Vector3.zero;

            (aAdd, aAngAdd, bAdd, bAngAdd) = _jN.Resolve(rigidbody, pairRigidbody, aAdd, aAngAdd, bAdd, bAngAdd);
            (aAdd, aAngAdd, bAdd, bAngAdd) = _jT.Resolve(rigidbody, pairRigidbody, aAdd, aAngAdd, bAdd, bAngAdd);
            (aAdd, aAngAdd, bAdd, bAngAdd) = _jB.Resolve(rigidbody, pairRigidbody, aAdd, aAngAdd, bAdd, bAngAdd);
        }

        struct Jacobian
        {
            // Jv + b = 0

            Vector3 _va;
            Vector3 _wa;
            Vector3 _vb;
            Vector3 _wb;

            float _linearBias;
            float _angularBias;
            float _effectiveMass;

            internal void Init(RBRigidbody rb_a, RBRigidbody rb_b, Vector3 contactPoint_a, Vector3 contactPoint_b, Vector3 jointDir_a, Vector3 jointDir_b, Vector3 dir, float dt, bool initBias = true)
            {
                Vector3 rA = contactPoint_a - rb_a?.CenterOfGravityWorld ?? contactPoint_a;
                Vector3 rB = contactPoint_b - rb_b?.CenterOfGravityWorld ?? contactPoint_b;

                Vector3 dirN = dir.normalized;

                _va = dirN;
                _wa = Vector3.Cross(rA, dirN);
                _vb = -dirN;
                _wb = Vector3.Cross(rB, -dirN);

                if (initBias)
                {
                    _linearBias = 0;

                    Vector3 linearError = contactPoint_b - contactPoint_a;
                    _linearBias = -(SOLVER_LINEAR_BETA / dt) * Vector3.Dot(linearError, dirN);
                    Quaternion.FromToRotation(jointDir_a, jointDir_b).ToAngleAxis(out float angle, out Vector3 axis);
                    angle = angle > 180 ? angle - 360 : angle;
                    Vector3 v = axis * angle * Mathf.Deg2Rad;
                    float angularError = Vector3.Dot(v, dirN);
                    _angularBias = -(SOLVER_ANGULAR_BETA / dt) * angularError;
                }

                float k = 0;

                if (rb_a != null)
                {
                k += rb_a.InverseMass;
                k += Vector3.Dot(_wa, Vector3.Scale(rb_a.InverseInertiaWs, _wa));
                }

                if (rb_b != null)
                {
                k += rb_b.InverseMass;
                k += Vector3.Dot(_wb, Vector3.Scale(rb_b.InverseInertiaWs, _wb));
                }

                _effectiveMass = 1 / k;
            }

            internal (Vector3 v_a, Vector3 av_a, Vector3 v_b, Vector3 av_b) Resolve(RBRigidbody rb_a, RBRigidbody rb_b, Vector3 v_a, Vector3 av_a, Vector3 v_b, Vector3 av_b)
            {
                float jv = 0;
                jv += Vector3.Dot(_va, rb_a?.ExpVelocity ?? Vector3.zero);
                jv += Vector3.Dot(_wa, rb_a?.ExpAngularVelocity ?? Vector3.zero);
                jv += Vector3.Dot(_vb, rb_b?.ExpVelocity ?? Vector3.zero);
                jv += Vector3.Dot(_wb, rb_b?.ExpAngularVelocity ?? Vector3.zero);

                float jv_ang = Vector3.Dot(_va, rb_a?.ExpAngularVelocity ?? Vector3.zero) + Vector3.Dot(_vb, rb_b?.ExpAngularVelocity ?? Vector3.zero);

                float linearLambda = _effectiveMass * -(jv + _linearBias);
                float angularLambda = (1 / (Vector3.Dot(_va, Vector3.Scale(rb_a?.InverseInertiaWs ?? Vector3.zero, _va)) + Vector3.Dot(_vb, Vector3.Scale(rb_b?.InverseInertiaWs ?? Vector3.zero, _vb)))) * -(jv_ang + _angularBias);

                if (rb_a != null)
                {
                v_a += rb_a.InverseMass * _va * linearLambda;
                av_a += Vector3.Scale(rb_a.InverseInertiaWs, _wa) * linearLambda;
                av_a += Vector3.Scale(rb_a.InverseInertiaWs, _va) * angularLambda;
                }

                if (rb_b != null)
                {
                v_b += rb_b.InverseMass * _vb * linearLambda;
                av_b += Vector3.Scale(rb_b.InverseInertiaWs, _wb) * linearLambda;
                av_b += Vector3.Scale(rb_b.InverseInertiaWs, _vb) * angularLambda;
                }

                return (v_a, av_a, v_b, av_b);
            }
        }
    }
}