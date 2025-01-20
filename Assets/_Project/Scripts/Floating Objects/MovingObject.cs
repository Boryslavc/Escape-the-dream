using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IStoppable
    {
        void Stop();
        void Resume();
    }
    public class MovingObject : MonoBehaviour , ISpellSusceptible , IStoppable
    {
        [SerializeField] MoveDirection[] moveDirections;
        [SerializeField] private float speed;
        [SerializeField] private BaseBuilderWrapper builder;

        public void SetSpeed(float value) => speed = value;

        int currentPoint = 0;
        List<Vector3> positionsToVisit;

        private bool isMoving = true;

        private const string layerName = "Moving Object";

        private void Awake()
        {
            this.gameObject.layer = LayerMask.NameToLayer(layerName);
            positionsToVisit = builder.BuildTrajectory(moveDirections, transform.position);
        }

        private void Update()
        {
            if(isMoving) 
                MoveSelf();
        }

        private void MoveSelf()
        {
            if (Vector2.Distance(transform.position, positionsToVisit[currentPoint]) > 0.01f)
                transform.position = Vector2.MoveTowards(transform.position, positionsToVisit[currentPoint], speed * Time.deltaTime);
            else
                currentPoint = (currentPoint + 1) % positionsToVisit.Count;
        }

        public void React(PooledSpellSettings spellSettings)
        {
            speed *= spellSettings.SpeedAffectMultiplicator;
        }

        public void Stop()
        {
            isMoving = false;
        }

        public void Resume()
        {
            isMoving = true;
        }
    }
}
