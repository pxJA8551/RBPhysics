using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Graphs;

namespace RBPhys.Prediction
{
    public class PredictionCore
    {
        public struct RigidbodyPredictioninfo
        {
            public bool enablePrediction;
            public int predictionFrameOffset;

            public static RigidbodyPredictioninfo CreatePrediction(int frameOffset)
            {
                var info = new RigidbodyPredictioninfo();
                info.enablePrediction = true;
                info.predictionFrameOffset = frameOffset;

                return info;
            }

            [Obsolete("predictionFrameOffsetの直指定を推奨、これは計算方法を示すための実装")]
            public void SetPredictionFrameOffset(float timeOffset, float delta)
            {
                predictionFrameOffset = (int)(timeOffset / delta);
            }
        }
    }
}