using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemy : Enemy
{
    private bool m_GoingLeft = true;
    protected override void Start()
    {
        base.Start();
    }
    protected override Square NextSquare(Square currentSquare)
    {
        Square tempSquare = currentSquare;
        while (base.CanGo(tempSquare._upSquare))
        {
            tempSquare = tempSquare._upSquare;
            if (tempSquare._hasPlayer)
            {
                return currentSquare._upSquare;
            }
        }
        while (base.CanGo(tempSquare._downSquare))
        {
            tempSquare = tempSquare._downSquare;
            if (tempSquare._hasPlayer)
            {
                return currentSquare._downSquare;
            }
        }
        if (m_GoingLeft)
        {
            Square square = LeftSquare(currentSquare);
            if (square == null)
                square = RightSquare(currentSquare);
            return square;
        }
        else
        {
            Square square = RightSquare(currentSquare);
            if (square == null)
                square = LeftSquare(currentSquare);
            return square;
        }
    }

    private Square LeftSquare(Square currentSquare)
    {
        if (base.CanGo(currentSquare._leftSquare))
        {
            return currentSquare._leftSquare;
        }
        else
        {
            m_GoingLeft = false;
            return base.NextSquare(currentSquare);
        }
    }

    private Square RightSquare(Square currentSquare)
    {
        if (base.CanGo(currentSquare._rightSquare))
        {
            return currentSquare._rightSquare;
        }
        else
        {
            m_GoingLeft = true;
            return base.NextSquare(currentSquare);
        }
    }
}
