using System;

namespace OpenBCI.Network
{
    public class RingBuffer
    {
        private readonly float[] data;
        private readonly float[] buffer;
        
        private int head;
        private int tail;
        private uint size;

        public float[] Data => GetData();

        public RingBuffer(uint capacity)
        {
            data = new float[capacity];
            buffer = new float[capacity];
            head = 0;
            tail = 0;
            size = capacity;
        }

        public void Insert(float item)
        {
            if (buffer.Length == 0) return;
        
            buffer[head] = item;
            head = (head + 1) % buffer.Length;

            if (size < buffer.Length) size++;
            else tail = (tail + 1) % buffer.Length;
        }

        private float[] GetData()
        {
            Array.Clear(data, 0, data.Length);
            if (size == 0) return data;
            
            if (tail < head)
            {
                Array.Copy(buffer, tail, data, 0, size);
            }
            else
            {
                Array.Copy(buffer, tail, data, 0, buffer.Length - tail);
                Array.Copy(buffer, 0, data, buffer.Length - tail, head);
            }

            return data;
        }
    }

}