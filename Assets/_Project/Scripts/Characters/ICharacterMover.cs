using UnityEngine;

namespace Core.Characters
{
    public interface ICharacterMover
    {
        void Move();
        void GetTransform(Transform character);
        void SetSpeed(float value);
        float GetSpeed();
    }
}