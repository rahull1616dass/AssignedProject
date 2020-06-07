using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
    }
    protected override Square NextSquare(Square currentSquare)
    {
        if (base.CanGo(currentSquare._leftSquare)&& currentSquare._leftSquare._hasPlayer)
        {
            return currentSquare._leftSquare;
        }
        else if (base.CanGo(currentSquare._rightSquare) && currentSquare._rightSquare._hasPlayer)
        {
            return currentSquare._rightSquare;
        }
        else if (base.CanGo(currentSquare._upSquare) && currentSquare._upSquare._hasPlayer)
        {
            return currentSquare._upSquare;
        }
        else if (base.CanGo(currentSquare._downSquare) && currentSquare._downSquare._hasPlayer)
        {
            return currentSquare._downSquare;
        }
        else
            return base.NextSquare(currentSquare);
    }
}
