using UnityEngine;

public class RotationTriggerYMove : MonoBehaviour
{
    [SerializeField] private Transform targetToMove;  // The object to move
    [SerializeField] private float moveAmountY = 2f;  // How much to move on the Y axis
    private float initialXRotation;

    void Start()
    {
        initialXRotation = transform.eulerAngles.x;
    }

    void Update()
    {
        float currentXRotation = transform.eulerAngles.x;
        float angleDifference = Mathf.DeltaAngle(initialXRotation, currentXRotation);

        if (Mathf.Abs(angleDifference) > 80f)
        {
            Vector3 newPos = targetToMove.position;
            newPos.y += moveAmountY;
            targetToMove.position = newPos;

            enabled = false;  // Prevent it from repeating
        }
    }
}
