using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RBHWABuffer<T> : IDisposable
{
    GraphicsBuffer _graphicsBuffer;
    int _count;

    public int Count { get { return _count; } }

    public RBHWABuffer(GraphicsBuffer.Target target, int count)
    {
        _count = count;
        _graphicsBuffer = new GraphicsBuffer(target, count, Marshal.SizeOf(typeof(T)));
    }

    public RBHWABuffer(int count)
    {
        _count = count;
        _graphicsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, count, Marshal.SizeOf(typeof(T)));
    }

    public GraphicsBuffer GetGraphicsBuffer()
    {
        return _graphicsBuffer;
    }

    public void SetData(T[] data)
    {
        _graphicsBuffer.SetData(data);
    }

    public void GetData(ref T[] data)
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
