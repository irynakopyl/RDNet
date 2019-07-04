using System;

namespace Arr
{
    public class MyArray<T>
    {
        int start;
        int length;

        T[] array;

        public int Lenght { get { return array.Length; } }
        public string Message { get { return "WRONG INDEXES!"; } }

        public MyArray(int startIndex, int len)
        {
            this.start = startIndex;
            length = len;
            array = new T[len];
        }

        public T this[int index]
        {
            get {
                if (index - start < 0) throw new IndexOutOfRangeException(Message);
                return array[index - start]; }
            set { array[index - start] = value; }
        }
    }
}

