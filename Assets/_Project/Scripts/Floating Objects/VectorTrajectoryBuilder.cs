using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class VectorTrajectoryBuilder : ITrajectoryBuilder
    {
        public List<Vector3> BuildTrajectory(MoveDirection[] steps, Vector3 startPosition)
        {
            List<Vector3> result = new List<Vector3>();
            result.Add(startPosition);

            Vector3 lastPos = startPosition;

            foreach (var step in steps)
            {
                var newPoint = lastPos + (step.Direction.normalized * step.Length);
                result.Add(newPoint);

                lastPos = newPoint;
            }

            return result;
        }
    }
}
