using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
    }
    protected override Square NextSquare(Square currentSquare)
    {
        float minDistance = float.MaxValue;
        Square newSquare = null;
        if (base.CanGo(currentSquare._downSquare)&& Vector3.Distance(_currentPlayer.transform.position, currentSquare._downSquare.transform.position) < minDistance)
        {
            newSquare = currentSquare._downSquare;
            minDistance = Vector3.Distance(_currentPlayer.transform.position, currentSquare._downSquare.transform.position);
        }
        if (base.CanGo(currentSquare._upSquare)&& Vector3.Distance(_currentPlayer.transform.position, currentSquare._upSquare.transform.position) < minDistance)
        {
            newSquare = currentSquare._upSquare;
            minDistance = Vector3.Distance(_currentPlayer.transform.position, currentSquare._upSquare.transform.position);
        }
        if (base.CanGo(currentSquare._leftSquare)&& Vector3.Distance(_currentPlayer.transform.position, currentSquare._leftSquare.transform.position) < minDistance)
        {
            newSquare = currentSquare._leftSquare;
            minDistance = Vector3.Distance(_currentPlayer.transform.position, currentSquare._leftSquare.transform.position);
        }
        if (base.CanGo(currentSquare._rightSquare)&& Vector3.Distance(_currentPlayer.transform.position, currentSquare._rightSquare.transform.position) < minDistance)
        {
            newSquare = currentSquare._rightSquare;
            minDistance = Vector3.Distance(_currentPlayer.transform.position, currentSquare._rightSquare.transform.position);
        }
        return newSquare;
    }
}
