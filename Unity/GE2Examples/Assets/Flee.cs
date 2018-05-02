using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour
{
    public float fleeRange = 100.0f;
    public Vector3 target = Vector3.zero;
    public GameObject targetGameObject;

    private float originalSpeed;

    public void Start()
    {
        originalSpeed = boid.maxSpeed;
    }

    public void Update()
    {
        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
        }
    }

    public override Vector3 Calculate()
    {
        if (Vector3.Distance(transform.position, target) < fleeRange)
        {
            boid.maxSpeed = originalSpeed * 1.1f;
            Vector3 desiredVelocity;
            desiredVelocity = transform.position - target;
            desiredVelocity.Normalize();
            desiredVelocity *= boid.maxSpeed;
            return (desiredVelocity - boid.velocity);
        }
        else
        {
            boid.maxSpeed = originalSpeed;
            return Vector3.zero;
        }
    }
}
