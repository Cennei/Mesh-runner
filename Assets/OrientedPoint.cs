using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OrientedPoint 
{

    public Vector3 position;
    public Quaternion rotation;

    public OrientedPoint(Vector3 point, Quaternion rotation)
    {
        this.position = point;
        this.rotation = rotation;
    }

    public OrientedPoint(Vector3 position, Vector3 forward)
    {
        this.position = position;
        this.rotation = Quaternion.LookRotation(forward);
    }

    public Vector3 LocalToWorldPosition(Vector3 localSpacePosition)
    {

        return position + rotation* localSpacePosition;

    }

    public Vector3 LocalToWorldVector(Vector3 localSpacePosition)
    {

        return rotation * localSpacePosition;

    }

}
