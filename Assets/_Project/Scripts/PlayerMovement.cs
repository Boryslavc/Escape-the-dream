using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float verticalSpeed = 1.0f;
    [SerializeField] private float verticalDeviation = 0.5f;

    public bool IsMoving { get; private set; } = true;
    public void ChangeMoveState() => IsMoving = !IsMoving;


    private Vector3 horizontalDireciton = new Vector3(1, 0);
    
    private float verticalInitValue;
    private int verticalMultiplicator = 1;


    private void Awake()
    {
        verticalInitValue = transform.position.y;
    }


    private void Update()
    {
        if(IsMoving)
        {
            if(OutsideVerticalBound())
                verticalMultiplicator *= -1;

            var step = transform.position + (horizontalDireciton * horizontalSpeed * Time.deltaTime);
            step.y = transform.position.y + (verticalSpeed * Time.deltaTime * verticalMultiplicator);

            transform.position = step;
        }
    }

    private bool OutsideVerticalBound() =>
        transform.position.y > verticalInitValue + verticalDeviation ||
            transform.position.y < verticalInitValue - verticalDeviation;
}
