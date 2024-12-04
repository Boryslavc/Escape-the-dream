using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Trajectory builder", menuName = "Systems/Core")]
    public class BuilderWrapper : ScriptableObject 
    {
        private ITrajectoryBuilder trajectoryBuilder = new VectorTrajectoryBuilder();

        public List<Vector3> BuildTrajectory(MoveDirection[] direction, Vector3 startPos, float objScale)
            =>   trajectoryBuilder.BuildTrajectory(direction, startPos, objScale);   
    }
}
