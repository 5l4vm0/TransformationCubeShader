using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTransformation : Transformation
{
    public Quaternion rotation = Quaternion.identity; // Use Quaternion instead of Vector3

    public override Matrix4x4 Matrix
    {
        get
        {
            // Convert the quaternion to a rotation matrix
            return Matrix4x4.Rotate(rotation);
        }
    }
}
