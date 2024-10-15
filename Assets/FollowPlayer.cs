using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [field: SerializeField] public GameObject Target
    { get; private set; }
    [field: SerializeField] public float SpeedMovement
    { get; private set; } = 2.0f;
    [field: SerializeField] public float SpeedRotation
    { get; private set; } = 50.0f;
    [field: SerializeField] public float StopMovementLimit
    { get; private set; } = 5.0f;
    [field: SerializeField] public float StopRotationLimit
    { get; private set; } = 0.0005f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Target.transform.position;
        Vector3 selfPosition = transform.position;
        Vector3 selfFacing = transform.forward;

        Vector3 directionToTarget = GetDirection(targetPosition, selfPosition);

        float distanceToTarget = GetDistance(targetPosition, selfPosition);
        float angleFromTarget = GetAngle(targetPosition, selfFacing);

        if (angleFromTarget > StopRotationLimit)
        {
            int spinModifier = 1;
            if (GetCrossProduct(selfFacing, directionToTarget.normalized).y < 0)
            {
                spinModifier = -1;
            }

            transform.Rotate(0, (SpeedRotation * spinModifier) * Time.deltaTime, 0);
        }

        if (distanceToTarget > StopMovementLimit)
        {
            transform.Translate(new Vector3(0, 0, SpeedMovement * Time.deltaTime));
        }
    }

    Vector3 GetDirection(Vector3 targetPosition, Vector3 seekerPosition, bool normalise = false)
    {
        Vector3 direction = targetPosition - seekerPosition;

        return (!normalise) ? direction : direction.normalized;
    }

    float GetDistance(Vector3 targetPosition, Vector3 otherPosition)
    {
        return Mathf.Sqrt((Mathf.Pow(otherPosition.x - targetPosition.x, 2)) + (Mathf.Pow(otherPosition.y - targetPosition.y, 2)) + (Mathf.Pow(otherPosition.z - targetPosition.z, 2)));

        // same as this in-built function: Vector3.Distance(targetPosition, otherPosition);
    }

    float GetDotProduct(Vector3 vectorToTarget, Vector3 vectorFacing)
    {
        return Vector3.Dot(vectorToTarget, vectorFacing);
    }

    // Order of operations matters when using Cross product to determine the spin of rotation.
    Vector3 GetCrossProduct(Vector3 VectorOne, Vector3 vectorTwo)
    {
        return Vector3.Cross(VectorOne, vectorTwo);
    }

    float GetAngle(Vector3 vectorToTarget, Vector3 vectorFacing)
    {
        return Vector3.Angle(vectorToTarget, vectorFacing);
    }
}
