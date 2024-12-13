using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Trajectory builder", menuName = "Systems/Core/Trajectory Builders/Vector Builder")]
    public class VectorBuilderWrapper : BaseBuilderWrapper 
    {
        private ITrajectoryBuilder trajectoryBuilder = new VectorTrajectoryBuilder();

        public override List<Vector3> BuildTrajectory(MoveDirection[] direction, Vector3 startPos, float objScale)
            =>   trajectoryBuilder.BuildTrajectory(direction, startPos, objScale);   
    }
}
