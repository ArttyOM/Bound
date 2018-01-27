using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Threading;

namespace AI
{
    public class GridScript : MonoBehaviour
    {
        /// <summary>
        /// Главный объект
        /// </summary>
        private static GridScript instance;

        /// <summary>
        /// Стандартная скорость врага
        /// </summary>
        private float standardEnemySize = 1;

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

        private int _updateSize;

        /// <summary>
        /// Пути для первого и второго
        /// </summary>
        private int[,] _prevHashFirst, _prevHashSecond;

        /// <summary>
        /// Игроки
        /// </summary>
        private GameObject[] players;

        private float _left, _top, _right, _bottom;

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
            LayerMask layer = ServiceLocator.Instance.ResolveService<GameSettingsProvider>()
                .GetSettings().NotWalkableMask;
            cellSize = ServiceLocator.Instance.ResolveService<GameSettingsProvider>()
                .GetSettings().PathfindingCellSize;
            standardEnemySize = ServiceLocator.Instance.ResolveService<GameSettingsProvider>()
                .GetSettings().StandardEnemySize;
            players = GameObject.FindGameObjectsWithTag("Player");

            sizeX = (int)Mathf.Ceil(gridSize.x / cellSize);
            sizeY = (int)Mathf.Ceil(gridSize.y / cellSize);
            print(sizeX + " " + sizeY);
            _left = transform.position.x - sizeX / 2 * cellSize;
            _top = transform.position.y + sizeY / 2 * cellSize;
            _right = transform.position.x + sizeX / 2 * cellSize;
            _bottom = transform.position.y - sizeY / 2 * cellSize;
            _updateSize = Mathf.Min(sizeX, sizeY) / 15;

            walkable = new bool[sizeY, sizeX];
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    RaycastHit2D raycastHitInfo = Physics2D.BoxCast(GetCoord(i, j),
                        new Vector2(standardEnemySize, standardEnemySize), 0, Vector2.zero, 0,layerMask:layer);

                    if (raycastHitInfo.collider == null)
                        walkable[i, j] = true;
                }
            }

            StartCoroutine(UpdateGrid());
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
        private bool GetIndexiesFromPoint(Vector3 point, out int i, out int j)
        {
            j = Mathf.RoundToInt((point.x - _left) / (_right - _left) * sizeX);
            i = Mathf.RoundToInt((point.y - _bottom) / (_top - _bottom) * sizeY);
            if (i >= 0 && i < sizeY && j >= 0 && j < sizeX)
                return true;
            return false;
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

        private int GetDistance(int i1, int j1, int i2, int j2)
        {
            return Mathf.Abs(i1 - i2) + Mathf.Abs(j1 - j2);
        }

        /// <summary>
        /// Находит путь, используя алгоритм Дейкстры
        /// </summary>
        /// <param name="start">Откуда</param>
        /// <param name="prevHash">Новый массив</param>
        /// <returns>Путь по точкам</returns>
        private void InitPathes(Vector3 start, out int[,] prevHash)
        {
            float[,] distance = new float[sizeY, sizeX];
            prevHash = new int[sizeY, sizeX];
            for (int i = 0; i < distance.GetLength(0); i++)
                for (int j = 0; j < distance.GetLength(1); j++)
                    distance[i, j] = INF;

            for (int i = 0; i < prevHash.GetLength(0); i++)
                for (int j = 0; j < prevHash.GetLength(1); j++)
                    prevHash[i, j] = -1;

            int startI, startJ;
            
            if (!GetIndexiesFromPoint(start, out startI, out startJ))
            {
                Debug.LogError("Nothing points!");
                return;
            }

            if (!walkable[startI, startJ])
            {
                return;
            }

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
                            if (GetDistance(startI, startJ, elementI + dI, elementJ + dJ) > _updateSize)
                                continue;

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
        }

        /// <summary>
        /// Находит путь НАХОДИТ ТОЛЬКО СЛЕДУЮЩИЙ ШАГ
        /// </summary>
        /// <param name="start">Откуда</param>
        /// <param name="number">Номер игрока</param>
        /// <returns></returns>
        private List<Vector3> GetPath(Vector3 start, int number)
        {
            int[,] tmp = number == 0 ? _prevHashFirst : _prevHashSecond;
            int startI, startJ;
            bool success = GetIndexiesFromPoint(start, out startI, out startJ);
            if (!success)
                return null;

            if (tmp == null)
                return null;

            if (tmp[startI, startJ] == -1)
                return null;

            List<Vector3> path = new List<Vector3>
            {
                GetPointFromIndexies(startI, startJ)
            };

            int prevI, prevJ;
            GetIndexesFromHash(tmp[startI, startJ], out prevI, out prevJ);
            path.Add(GetPointFromIndexies(prevI, prevJ));

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
                for (int i = 0; i < sizeY; i++)
                    for (int j = 0; j < sizeX; j++)
                    {
                        if (walkable[i, j])
                            Gizmos.color = Color.green;
                        else
                            Gizmos.color = Color.red;

                        Gizmos.DrawLine(GetCoord(i, j), GetCoord(i, j) + Vector3.up * cellSize * 0.9f);
                    }
            }
        }

        #region Наверно оптимизация для поиска путей

        /// <summary>
        /// Очередь агентов
        /// </summary>
        private Queue<Agent> agentsQueue = new Queue<Agent>();

        private HashSet<Agent> _currentAgents = new HashSet<Agent>();

        /// <summary>
        /// Добавить агента в очередь
        /// </summary>
        /// <param name="agent">Агент</param>
        public static void AddToQueue(Agent agent)
        {
            /*if (!instance._currentAgents.Contains(agent))
            {
                instance._currentAgents.Add(agent);
                instance.agentsQueue.Enqueue(agent);
            }

            if (!instance.isFindPathes)
                instance.StartCoroutine(instance.FindPathes());*/

            GameObject player = agent.Player;
            List<Vector3> path = instance.GetPath(agent.transform.position, player == instance.players[0] ? 0 : 1);
            agent.SetPath(path);
        }

        /// <summary>
        /// Ищет ли пути
        /// </summary>
        private bool isFindPathes = false;

        /// <summary>
        /// Обновляет сетку
        /// </summary>
        /// <returns></returns>
        private IEnumerator UpdateGrid()
        {
            yield return new WaitForSeconds(0.3f);
            while (true)
            {
                InitPathes(players[0].transform.position, out _prevHashFirst);
                yield return null;
                InitPathes(players[1].transform.position, out _prevHashSecond);
                yield return new WaitForSeconds(0.3f);
            }
        }

        private IEnumerator FindPathes()
        {
            isFindPathes = true;
            while (agentsQueue.Count != 0)
            {
                Agent agent = agentsQueue.Dequeue();
                GameObject player = agent.Player;
                List<Vector3> path = GetPath(agent.transform.position, player == instance.players[0] ? 0 : 1);
                agent.SetPath(path);
                _currentAgents.Remove(agent);
                yield return null;
            }
            isFindPathes = false;
        }

        #endregion
    }
}