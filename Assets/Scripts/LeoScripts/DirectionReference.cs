using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionReference: MonoBehaviour 
{
    [SerializeField]
    Vector3 _w = Vector3.forward;

    [SerializeField]
    Vector3 _a = Vector3.left;

    [SerializeField]
    Vector3 _s = Vector3.back;

    [SerializeField]
    Vector3 _d = Vector3.right;

    public Vector3 W { get { return _w; } }
    public Vector3 A { get { return _a; } }
    public Vector3 S { get { return _s; } }
    public Vector3 D { get { return _d; } }

    public Vector3 ScreenDirectionToWorldDirecton(Vector2 screenDirection)
    {
        switch(screenDirection)
        {
            case Vector2 v when v.Equals(Vector2.up):
                return W;
            case Vector2 v when v.Equals(Vector2.down):
                return S;
            case Vector2 v when v.Equals(Vector2.left):
                return A;
            case Vector2 v when v.Equals(Vector2.right):
                return D;
            default:
                return Vector3.zero;
        }
    }

}
