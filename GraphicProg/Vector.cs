using System;

namespace GraphicProg
{
    class Vector<T>
    {
        T[] m_array;

        public Vector()
        {
            m_array = new T[0];
        }
        public T At(int index)
        {
            return m_array[index];
        }
        public T Back
        {
            get => m_array[m_array.Length - 1];
            set => m_array[m_array.Length - 1] = value;
        }
        public T Front
        {
            get => m_array[0];
            set => m_array[0] = value;
        }

        public void RemoveLast()
        {
            Array.Resize(ref m_array, m_array.Length - 1);
        }

        public void RemoveFirst()
        {
            for (int i = 1; i > m_array.Length; ++i)
                m_array[i - 1] = m_array[i];
            RemoveLast();
        }

        public void Clear()
        {
            m_array = new T[0];
        }
        public void Insert(int pos, T item)
        {
            Array.Resize(ref m_array, m_array.Length + 1);
            for (var i = m_array.Length - 1; i > pos; i--)
                m_array[i] = m_array[i - 1];
            m_array[pos] = item;
        }
        public void PushBack(T item)
        {
            Array.Resize(ref m_array, m_array.Length + 1);
            m_array[m_array.Length - 1] = item;
        }
        public void PushFront(T item)
        {
            Insert(0, item);
        }
        public int Count => m_array.Length;
    }
}
