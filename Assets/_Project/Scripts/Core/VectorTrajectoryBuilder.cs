using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    // The class we need only at the beginning of each level.
    // No need to make it monobehaviour or create too many instances of the class for each moving object.
    // That's why there's a SO wrapper for it.
    public class VectorTrajectoryBuilder : ITrajectoryBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directions"></param>
        /// <param name="startPosition"></param>
        /// <returns> A list of Vectors that describe the path.</returns>
        public List<Vector3> BuildTrajectory(MoveDirection[] directions, Vector3 startPosition, float objScale)
        {
            List<Vector3> result = new List<Vector3>();
            result.Add(startPosition);

            Vector3 lastPos = startPosition;

            foreach (var direction in directions)
            {
                var newDir = DirectionToVectors(direction, lastPos);
                foreach (var pos in newDir)
                {
                    var newPoint = (pos * objScale) + lastPos;
                    result.Add(newPoint);
                    lastPos = newPoint;
                }
            }

            return result;
        }

        /// <summary>
        /// Convert Direction enum and get points to go through.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="initialPosition"></param>
        /// <returns>Returns one endpoint in case of straight movement. Returns middlepoint and endpoint in case curve movement.</returns>
        private List<Vector3> DirectionToVectors(MoveDirection direction, Vector3 initialPosition)
        {
            List<Vector3> res = new List<Vector3>(2);

            int objectSizeToDirectionLengthRatio = 2;

            // for curve directions
            float firstStepMainDir = 0.6f;
            float secondStepMainDir = 0.4f;
            float firstStepSecondaryDir = 0.2f;
            float secondStepSecondaryDir = 0.8f;

            switch(direction)
            {
                case MoveDirection.StraightLeft:
                    res.Add((new Vector3(-1,0,0) * objectSizeToDirectionLengthRatio));
                    break;
                case MoveDirection.StraightRight:
                    res.Add((new Vector3(1, 0, 0) * objectSizeToDirectionLengthRatio));
                    break;
                case MoveDirection.StraightUp:
                    res.Add((new Vector3(0, 1, 0) * objectSizeToDirectionLengthRatio));
                    break;
                case MoveDirection.StraightDown:
                    res.Add((new Vector3(0, -1, 0) * objectSizeToDirectionLengthRatio));
                    break;
                case MoveDirection.CurveLeftUp:
                    res.Add(new Vector3(-firstStepMainDir, firstStepSecondaryDir) * objectSizeToDirectionLengthRatio);
                    res.Add(new Vector3(-secondStepMainDir, secondStepSecondaryDir) * objectSizeToDirectionLengthRatio);
                    break;
                case MoveDirection.CurveRightUp:
                    res.Add(new Vector3(firstStepMainDir, firstStepSecondaryDir) * objectSizeToDirectionLengthRatio);
                    res.Add(new Vector3(secondStepMainDir, secondStepSecondaryDir) * objectSizeToDirectionLengthRatio);
                    break;
                case MoveDirection.CurveLeftDown:
                    res.Add(new Vector3(-firstStepMainDir, -firstStepSecondaryDir) * objectSizeToDirectionLengthRatio);
                    res.Add(new Vector3(-secondStepMainDir, -secondStepSecondaryDir) * objectSizeToDirectionLengthRatio);
                    break;
                case MoveDirection.CurveRightDown:
                    res.Add(new Vector3(firstStepMainDir, -firstStepSecondaryDir) * objectSizeToDirectionLengthRatio);
                    res.Add(new Vector3(secondStepMainDir, -secondStepSecondaryDir) * objectSizeToDirectionLengthRatio);
                    break;
                case MoveDirection.CurveUpLeft:
                    res.Add(new Vector3(-firstStepSecondaryDir, firstStepMainDir) * objectSizeToDirectionLengthRatio);
                    res.Add(new Vector3(-firstStepSecondaryDir, secondStepMainDir) * objectSizeToDirectionLengthRatio);
                    break;
                case MoveDirection.CurveUpRight:
                    res.Add(new Vector3(firstStepSecondaryDir, firstStepMainDir) * objectSizeToDirectionLengthRatio);
                    res.Add(new Vector3(firstStepSecondaryDir, secondStepMainDir) * objectSizeToDirectionLengthRatio);
                    break;
                case MoveDirection.CurveDownLeft:
                    res.Add(new Vector3(-firstStepSecondaryDir, -firstStepMainDir) * objectSizeToDirectionLengthRatio);
                    res.Add(new Vector3(-firstStepSecondaryDir, -secondStepMainDir) * objectSizeToDirectionLengthRatio);
                    break;
                case MoveDirection.CurveDownRight:
                    res.Add(new Vector3(firstStepSecondaryDir, -firstStepMainDir) * objectSizeToDirectionLengthRatio);
                    res.Add(new Vector3(firstStepSecondaryDir, -secondStepMainDir) * objectSizeToDirectionLengthRatio);
                    break;
            }

            return res;
        }
    }
}
