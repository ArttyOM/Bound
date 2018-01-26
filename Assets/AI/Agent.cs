using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class Agent : MonoBehaviour {

        private Vector3 destination;
        private List<Vector3> path, newPath;
        private bool isUpdated;
        private int index;
        private float lastUpdateTime = -10;
        private float delta = 0.3f;

        // Use this for initialization
        void Start() {
            
        }

        private IEnumerator FollowPath()
        {
            int currentIndex = 0;
            while (transform.position != path[path.Count - 1])
            {
                if (path[currentIndex] == transform.position)
                    currentIndex++;
                transform.position = Vector3.MoveTowards(transform.position, path[currentIndex],
                    Speed * Time.deltaTime);
                yield return null;
            }
        }

        /// <summary>
        /// Скорость
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Направление
        /// </summary>
        public Vector3 Destination
        {
            get
            {
                return destination;
            }
            set
            {
                if (lastUpdateTime + delta < Time.time)
                {
                    destination = value;
                    lastUpdateTime = Time.time;
                    StopAllCoroutines();
                    path = GridScript.Path(transform.position, destination);
                    if (path != null && path.Count != 0)
                        StartCoroutine(FollowPath());
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (path != null)
            {
                foreach (Vector3 p in path)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(p, Vector3.one / 2);
                }
            }
            Gizmos.color = Color.green;
            Gizmos.DrawCube(destination, Vector3.one / 2);
        }
    }
}