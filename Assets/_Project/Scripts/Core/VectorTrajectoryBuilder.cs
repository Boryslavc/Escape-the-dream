using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class VectorTrajectoryBuilder : ITrajectoryBuilder
    {
        public List<Vector3> BuildTrajectory(MoveDirection[] directions, Vector3 startPosition, float objScale)
        {
            int objectSizeToDirectionLengthRatio = 2;

            List<Vector3> result = new List<Vector3>();
            result.Add(startPosition);

            Vector3 lastPos = startPosition;

            foreach (var direction in directions)
            {
                var newPoint = ((direction.Direction * objectSizeToDirectionLengthRatio) * objScale) + lastPos;
                result.Add(newPoint);

                lastPos = newPoint;
            }

            return result;
        }
    }
}
