using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Direction", menuName = "Systems/Core/Direction")]
    public class MoveDirection : ScriptableObject
    {
        public Vector3 Direction;
    }
}
