using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB2D
{
    public Vector2 minPoint;
    public Vector2 maxPoint;

    public float Width()
    {
        return maxPoint.x - minPoint.x;
    }

    public float Height()
    {
        return maxPoint.y - minPoint.y;
    }

    public bool Intersects(AABB2D aabb)
    {
        return maxPoint.x >= aabb.minPoint.x && maxPoint.y >= aabb.minPoint.y && aabb.maxPoint.x >= minPoint.x && aabb.maxPoint.y >= minPoint.y;
    }

    public bool Contains(AABB2D aabb)
    {
        return aabb.minPoint.x >= minPoint.x && aabb.minPoint.y >= minPoint.y && aabb.maxPoint.x <= maxPoint.x && aabb.maxPoint.y <= maxPoint.y;
    }

    public bool IsDegenerate()
    {
        return minPoint.x >= maxPoint.x || minPoint.y >= maxPoint.y;
    }

    public bool HasNegativeVolume()
    {
        return maxPoint.x < minPoint.x || maxPoint.y < minPoint.y;
    }

    public static AABB2D operator +(AABB2D aabb, Vector2 point)
    {
        AABB2D a = new AABB2D();
        a.minPoint = aabb.minPoint + point;
        a.maxPoint = aabb.maxPoint + point;
        return a;
    }

    public static AABB2D operator -(AABB2D aabb, Vector2 point)
    {
        AABB2D a = new AABB2D();
        a.minPoint = aabb.minPoint - point;
        a.maxPoint = aabb.maxPoint - point;
        return a;
    }
}