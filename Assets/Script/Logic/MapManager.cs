using System;
using System.Collections.Generic;
using UnityEngine;

enum MapCubeMoveableType
{
    None,
    Moveable,
    NonMovable,
}

enum MapCubeLayerType
{
    None,
    Ground,
    Wall,
}

[Serializable]
public class MapManager
{
    [SerializeField]
    private List<Vector3> mSpawnPoints = new();

    public Vector3 GetSpawnPoint(int playerIndex)
    {
        if (playerIndex < mSpawnPoints.Count)
        {
            return mSpawnPoints[playerIndex];
        }

        return Vector3.zero;
    }
}
