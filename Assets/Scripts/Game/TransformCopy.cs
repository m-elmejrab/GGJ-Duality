using UnityEngine;
using System;

[Obsolete]
public class TransformCopy
{

    public Vector3 position;
    public Quaternion rotation;

    public TransformCopy(Transform t)
    {
        position = t.position;
        rotation = t.rotation;
    }

    public TransformCopy(GameObject g)
    {
        position = g.transform.position;
        rotation = g.transform.rotation;
    }


    public GameObject InstantiateObjectWithThis(GameObject g)
    {
        return GameObject.Instantiate(g, position, rotation);
    }
}

