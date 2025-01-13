using UnityEngine;

namespace Core.Characters
{
    public class HorizontalDrifting : ICharacterMover
    {
        private Transform character;

        private Vector3 moveDirection = Vector3.right;

        private float xSpeed = 2f;
        private float ySpeed = 1f;
        private int verticalMultiplicator = 1;
        private float yDeviation = 0.6f;
        private float verticalInitValue;

        public void SetSpeed(float value)
        {
            xSpeed = value;
            ySpeed = value / 2;
        }

        public float GetSpeed() => xSpeed;


        public void GetTransform(Transform character)
        {
            this.character = character;
            verticalInitValue = character.position.y;
        }

        public void Move()
        {
            if (OutsideVerticalBound())
                verticalMultiplicator *= -1;

            var step = character.position + (moveDirection * xSpeed * Time.deltaTime);
            step.y = character.position.y + (ySpeed * Time.deltaTime * verticalMultiplicator);

            character.position = step;
        }

        private bool OutsideVerticalBound() =>
            character.position.y > verticalInitValue + yDeviation ||
                character.position.y < verticalInitValue - yDeviation;
    }
}