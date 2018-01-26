using System.Collections.Generic;
using System;

namespace AI
{
    /// <summary>
    /// Куча
    /// </summary>
    /// <typeparam name="T">Класс, который можно сравнивать</typeparam>
    public class Heap<T> where T : IComparable<T>
    {
        private List<T> heapArray = new List<T>();

        /// <summary>
        /// Пустой конструктор 0_о
        /// </summary>
        public Heap()
        {

        }

        /// <summary>
        /// Кол-во элементов в куче
        /// </summary>
        public int Count
        {
            get { return heapArray.Count; }
        }

        /// <summary>
        /// Минимальный элемент
        /// </summary>
        public T Min
        {
            get
            {
                return heapArray[0];
            }
        }

        /// <summary>
        /// Пустая ли куча
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        /// <summary>
        /// Добавить в кучу
        /// </summary>
        /// <param name="value">Новый элемент</param>
        public void Insert(T value)
        {
            heapArray.Add(value);
            ShiftUp(Count - 1);
        }

        /// <summary>
        /// Вытащить минимальный
        /// </summary>
        /// <returns>Минимальный элемент</returns>
        public T ExtractMin()
        {
            T result = Min;

            heapArray[0] = heapArray[Count - 1];
            heapArray.RemoveAt(Count - 1);

            ShiftDown(0);

            return result;
        }

        /// <summary>
        /// Поднять
        /// </summary>
        /// <param name="index"></param>
        private void ShiftUp(int index)
        {
            int parent = GetParent(index);
            while (index > 0 && heapArray[index].CompareTo(heapArray[parent]) <= 0)
            {
                Swap(index, parent);
                index = parent;
                parent = GetParent(index);
            }
        }

        /// <summary>
        /// Опустить
        /// </summary>
        /// <param name="index"></param>
        private void ShiftDown(int index)
        {
            int[] child = GetChildren(index);
            int lowestChild = index;
            while (true)
            {
                foreach (int ch in child)
                {
                    if (ch < Count && heapArray[ch].CompareTo(heapArray[lowestChild]) <= 0)
                        lowestChild = ch;
                }

                if (lowestChild == index)
                    break;

                Swap(index, lowestChild);
                index = lowestChild;
                child = GetChildren(index);
            }
        }

        /// <summary>
        /// Получить детей
        /// </summary>
        /// <param name="index">индекс отца</param>
        /// <returns>индексы детей</returns>
        private int[] GetChildren(int index)
        {
            return new int[] { 2 * index + 1, 2 * index + 2 };
        }

        /// <summary>
        /// Получить отца
        /// </summary>
        /// <param name="index">Индекс ребенка</param>
        /// <returns>Индекс отца</returns>
        private int GetParent(int index)
        {
            return (index - 1) / 2;
        }

        /// <summary>
        /// Поменять местами
        /// </summary>
        /// <param name="a">1 индекс</param>
        /// <param name="b">2 индекс</param>
        private void Swap(int a, int b)
        {
            T tmp = heapArray[a];
            heapArray[a] = heapArray[b];
            heapArray[b] = tmp;
        }
    }
}
