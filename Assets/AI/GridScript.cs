using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField]
        private Vector2 gridSize;

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
        private void InitGrid()
        {
            cellSize = ServiceLocator.Instance.Resolve<GameSettingsProvider>().GetSettings().PathfindingCeilSize;
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
        private void GetIndexesFromPoint(Vector3 point, out int i, out int j)
        {
            j = (int)Mathf.Round(transform.position.x - cellSize * sizeX / 2);
            i = (int)Mathf.Round(transform.position.y - cellSize * sizeY / 2);
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
       
        /// <summary>
        /// Находит путь, используя алгоритм Дейкстры(A*)
        /// </summary>
        /// <param name="start">Откуда</param>
        /// <param name="finish">Куда</param>
        /// <returns>Путь по точкам</returns>
        private List<Vector3> FindPath(Vector3 start, Vector3 finish)
        {
            Dictionary<int, float> distance = new Dictionary<int, float>();
            return null;
        }

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

                        Gizmos.DrawCube(GetCoord(i, j), Vector3.one * cellSize * 0.9f);
                    }
            }
        }
    }
}