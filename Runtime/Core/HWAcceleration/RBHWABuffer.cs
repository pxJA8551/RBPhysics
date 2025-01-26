using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RBPhys
{
    public class RBHWABuffer<T> : IDisposable
    {
        GraphicsBuffer _graphicsBuffer;
        int _count;

        int _stride;

        public int Count { get { return _count; } }

        public RBHWABuffer(GraphicsBuffer.Target target, int count)
        {
            _count = count;
            _graphicsBuffer = new GraphicsBuffer(target, count, Marshal.SizeOf(typeof(T)));
        }

        public RBHWABuffer(int count)
        {
            _count = count;
            _stride = Marshal.SizeOf(typeof(T));
            _graphicsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, count, _stride);
        }

        public GraphicsBuffer GetGraphicsBuffer()
        {
            return _graphicsBuffer;
        }

        public void SetData(T[] data)
        {
            _graphicsBuffer.SetData(data);
        }

        public void GetData(T[] data)
        {
            _graphicsBuffer.GetData(data);
        }

        public T[] GetData()
        {
            T[] data = new T[_count];
            _graphicsBuffer.GetData(data);

            return data;
        }

        public void Dispose()
        {
            _graphicsBuffer.Dispose();
            _count = -1;
        }
    }
}