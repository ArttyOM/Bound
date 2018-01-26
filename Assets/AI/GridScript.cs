using System.Collections.Generic;
using UnityEngine;
using System;

namespace AI
{
    public class GridScript : MonoBehaviour
    {

        /// <summary>
        /// Главный объект
        /// </summary>
        private static GridScript instance;

        /// <summary>
        /// Размер сетки
        /// </summary>
        public Vector2 gridSize;

        /// <summary>
        /// Размер клетки   
        /// </summary>
        private float cellSize;

        /// <summary>
        /// Размеры массивов
        /// </summary>
        private int sizeX, sizeY;

        /// <summary>
        /// Для каждой клетки можно ли её пройти
        /// </summary>
        private bool[,] walkable;

        public Vector3 start;
        public Vector3 finish;

        // Use this for initialization
        void Start()
        {
            // может быть только одна сетка
            if (instance == null)
            {
                instance = this;
                InitGrid();
            }
        }


        /// <summary>
        /// Задаёт сетку
        /// </summary>
        public void InitGrid()
        {
            cellSize = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings().PathfindingCellSize;
            sizeX = (int)Mathf.Ceil(gridSize.x / cellSize);
            sizeY = (int)Mathf.Ceil(gridSize.y / cellSize);

            walkable = new bool[sizeY, sizeX];
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    RaycastHit2D raycastHitInfo = Physics2D.BoxCast(GetCoord(i, j),
                        new Vector2(cellSize, cellSize), 0, Vector2.zero);

                    if (raycastHitInfo.collider == null)
                        walkable[i, j] = true;
                }
            }
        }

        /// <summary>
        /// Получает координаты клетки
        /// </summary>
        /// <param name="i">номер строки</param>
        /// <param name="j">номер столбца</param>
        /// <returns>точка, где центр клетки</returns>
        private Vector3 GetCoord(int i, int j)
        {
            return new Vector3(transform.position.x + (-sizeX / 2 + j) * cellSize,
                transform.position.y + (-sizeY / 2 + i) * cellSize);
        }

        /// <summary>
        /// Находит точку по координате
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="i">Строка</param>
        /// <param name="j">Столбец</param>
        private void GetIndexiesFromPoint(Vector3 point, out int i, out int j)
        {
            j = Mathf.RoundToInt((point.x /  (transform.position.x + sizeX * cellSize)) * sizeX) + sizeX / 2;
            i = Mathf.RoundToInt((point.y / (transform.position.y + sizeY * cellSize)) * sizeY) + sizeY / 2;
        }

        /// <summary>
        /// Получает точку по индексам
        /// </summary>
        /// <param name="i">строка</param>
        /// <param name="j">столбец</param>
        /// <returns>Точка</returns>
        private Vector3 GetPointFromIndexies(int i, int j)
        {
            return new Vector3(transform.position.x + (j - sizeX / 2) * cellSize,
                transform.position.y + (i - sizeY / 2) * cellSize);
        }

        /// <summary>
        /// Получает индексы по хэшу
        /// </summary>
        /// <param name="hash">хэщ</param>
        /// <param name="i">строка</param>
        /// <param name="j">столбцы</param>
        private void GetIndexesFromHash(int hash, out int i, out int j)
        {
            i = hash / walkable.GetLength(1);
            j = hash % walkable.GetLength(1);
        }

        /// <summary>
        /// Получает хэш по индексам
        /// </summary>
        /// <param name="i">строка</param>
        /// <param name="j">столбец</param>
        /// <returns>хэш</returns>
        private int GetHashFromIndexes(int i, int j)
        {
            return i * walkable.GetLength(1) + j;
        }

#region Поиск пути
        const float INF = 10000f;

        /// <summary>
        /// Нужна для сортировки
        /// </summary>
        struct DijkstraInfo : IComparable<DijkstraInfo>
        {

            public int hash;
            public float dist;

            public int CompareTo(DijkstraInfo other)
            {
                if (dist < other.dist)
                    return -1;
                else if (dist > other.dist)
                    return 1;
                return 0;
            }

            public DijkstraInfo(int hash, float dist)
            {
                this.hash = hash;
                this.dist = dist;
            }
        }

        /// <summary>
        /// Сравнивает флоаты
        /// </summary>
        /// <param name="a">первый</param>
        /// <param name="b">второй</param>
        /// <returns>равны ли</returns>
        bool EqualFloat(float a, float b)
        {
            return Mathf.Abs(a - b) < 0.00001f;
        }

        /// <summary>
        /// Находит путь, используя алгоритм Дейкстры(A*)
        /// </summary>
        /// <param name="start">Откуда</param>
        /// <param name="finish">Куда</param>
        /// <returns>Путь по точкам</returns>
        private List<Vector3> FindPath(Vector3 start, Vector3 finish)
        {
            float[,] distance = new float[sizeY, sizeX];
            int[,] prevHash = new int[sizeY, sizeX];
            for (int i = 0; i < distance.GetLength(0); i++)
                for (int j = 0; j < distance.GetLength(1); j++)
                    distance[i, j] = INF;

            for (int i = 0; i < prevHash.GetLength(0); i++)
                for (int j = 0; j < prevHash.GetLength(1); j++)
                    prevHash[i, j] = -1;

            int startI, startJ;
            GetIndexiesFromPoint(start, out startI, out startJ);
            int finishI, finishJ;
            GetIndexiesFromPoint(finish, out finishI, out finishJ);

            if (!walkable[startI, startJ] || !walkable[finishI, finishJ])
                return null;

            distance[startI, startJ] = 0;
            Heap<DijkstraInfo> heap = new Heap<DijkstraInfo>();
            heap.Insert(new DijkstraInfo(GetHashFromIndexes(startI, startJ), 0));

            // минимальные пути
            while (!heap.IsEmpty)
            {
                DijkstraInfo minElement = heap.ExtractMin();
                int elementI, elementJ;
                GetIndexesFromHash(minElement.hash, out elementI, out elementJ);
   
                if (!EqualFloat(minElement.dist, distance[elementI, elementJ]))
                    continue;
                float updateDist = distance[elementI, elementJ];
                int[] delta = { -1, 0, 1 };
                foreach (int dI in delta)
                {
                    foreach (int dJ in delta)
                    {
                        if (dI == 0 && dJ == 0)
                            continue;

                        if (elementI + dI >= 0 && elementI + dI < sizeY 
                            && elementJ + dJ >= 0 && elementJ + dJ < sizeX)
                        {
                            if (!walkable[elementI + dI, elementJ + dJ])
                                continue;

                            if (distance[elementI + dI, elementJ + dJ] > updateDist + Mathf.Sqrt(dI * dI + dJ * dJ))
                            {
                                distance[elementI + dI, elementJ + dJ] = updateDist + Mathf.Sqrt(dI * dI + dJ * dJ);
                                prevHash[elementI + dI, elementJ + dJ] = GetHashFromIndexes(elementI, elementJ);
                                heap.Insert(new DijkstraInfo(GetHashFromIndexes(elementI + dI, elementJ + dJ),
                                    distance[elementI + dI, elementJ + dJ]));
                            }
                        }
                    }
                }
            }

            // восстановление путей    
            if (prevHash[finishI, finishJ] == -1)
                return null;

            int currI = finishI, currJ = finishJ;
            List<Vector3> path = new List<Vector3>
            {
                GetPointFromIndexies(currI, currJ)
            };

            while (!(currI == startI && currJ == startJ))
            {
                //print(currI + " " + currJ + "\n" + startI + " " + startJ); 
                int tmpHash = prevHash[currI, currJ];
                GetIndexesFromHash(tmpHash, out currI, out currJ);
                path.Add(GetPointFromIndexies(currI, currJ));
            }
            path.Reverse();
            return path;
        }
        #endregion

        /// <summary>
        /// Рисует сетку
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 0.1f));
            
            if (walkable != null)
            {
                int ii, iii, jj, jjj;
                GetIndexiesFromPoint(start, out ii, out jj);
                GetIndexiesFromPoint(finish, out iii, out jjj);
                for (int i = 0; i < sizeY; i++)
                    for (int j = 0; j < sizeX; j++)
                    {
                        if (i == ii && j == jj)
                            Gizmos.color = Color.black;
                        else if (i == iii && j == jjj)
                            Gizmos.color = Color.cyan;
                        else if (walkable[i, j])
                            Gizmos.color = Color.green;
                        else
                            Gizmos.color = Color.red;

                        Gizmos.DrawCube(GetCoord(i, j), Vector3.one * cellSize * 0.9f);
                    }

                List<Vector3> path = FindPath(start, finish);
                if (path != null)
                {
                    foreach(Vector3 point in path)
                    {
                        Gizmos.color = Color.magenta;
                        Gizmos.DrawCube(point, Vector3.one * cellSize * 0.9f);
                    }
                }
            }
            Gizmos.color = Color.black;
            Gizmos.DrawCube(start, Vector3.one * 0.2f);
        }
    }
}