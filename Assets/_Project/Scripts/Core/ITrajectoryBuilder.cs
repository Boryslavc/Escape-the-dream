using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public interface ITrajectoryBuilder
    {
        public List<Vector3> BuildTrajectory(MoveDirection[] directions, Vector3 startPosition, float objScale);
    }
}
