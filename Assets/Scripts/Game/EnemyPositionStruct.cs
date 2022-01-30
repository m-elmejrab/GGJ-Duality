using UnityEngine;
using System;

[Obsolete]
public struct EnemyPositionStruct
{


    public EnemyPositionStruct(IAmAnEnemy en)
    {
        enemyPrefab = en.GetGameObject();
        enemyTransformCopy = new TransformCopy(en.GetGameObject());
    }

    public EnemyPositionStruct(GameObject en, Vector2 pos)
    {
        enemyPrefab = en;
        enemyTransformCopy = new TransformCopy(en);
    }

    public GameObject enemyPrefab;

    public TransformCopy enemyTransformCopy;
}

