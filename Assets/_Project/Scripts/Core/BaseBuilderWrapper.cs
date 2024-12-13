using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract class BaseBuilderWrapper : ScriptableObject
    {
        public abstract List<Vector3> BuildTrajectory(MoveDirection[] direction, Vector3 startPos, float objScale);
    }
}
