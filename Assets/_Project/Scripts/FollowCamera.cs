using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] [Range(1,10)] private float smoothing = 8f;

    private Vector3 offset = new Vector2(6, 0);

    private void Update()
    {
        var targetPos = player.position + offset;

        if (Vector2.Distance(transform.position, targetPos) > 0.1f)
            MoveToTarget(targetPos);
    }

    private void MoveToTarget(Vector3 targetPos)
    {        
        targetPos = Vector3.Lerp(transform.position, targetPos, smoothing / 10 * Time.deltaTime);
        targetPos.z = -10;

        transform.position = targetPos;
    }
}
