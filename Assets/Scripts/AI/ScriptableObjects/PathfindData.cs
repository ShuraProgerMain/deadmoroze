using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Pathfinder", menuName = "AI Pathfinder")]
public class PathfindData : ScriptableObject
{
    public float walkSpeed;
    public float rotationSpeed;
    [Range(0, 90)]
    public float angleFOV;
    public float angleRotation;

    public void Initialize(AIPathfinder pathfinder)
    {
        pathfinder._walkSpeed = walkSpeed;
        pathfinder._rotationSpeed = rotationSpeed;
        pathfinder._angleFOV = angleFOV;
        pathfinder._angleRotation = angleRotation;
        pathfinder._targetPosition = GameObject.FindWithTag("ChristmasTree").transform;
    }

}
