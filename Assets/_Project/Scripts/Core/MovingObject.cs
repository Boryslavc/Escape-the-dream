using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] MoveDirection[] moveDirections;
        [SerializeField] private float speed;
        [SerializeField] private BuilderWrapper builder;

        int currentPoint = 0;
        List<Vector3> positionsToVisit;

        public bool DebugMode = false;

        private void Awake()
        {
            positionsToVisit = builder.BuildTrajectory(moveDirections, transform.position, transform.localScale.x);
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, positionsToVisit[currentPoint]) > 0.01f)
                transform.position = Vector2.MoveTowards(transform.position, positionsToVisit[currentPoint], speed * Time.deltaTime);
            else
                currentPoint = (currentPoint + 1) % positionsToVisit.Count;
        }

        private void FixedUpdate()
        {
            if (DebugMode)
                DrawPath();
        }
        private void DrawPath()
        {
            for (int i = 1; i < positionsToVisit.Count; i++)
                Debug.DrawLine(positionsToVisit[i - 1], positionsToVisit[i], Color.cyan);
        }
    }
}
