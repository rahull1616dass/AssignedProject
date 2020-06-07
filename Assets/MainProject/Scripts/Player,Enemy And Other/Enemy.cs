using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ISessionEvent
{
    [HideInInspector] public Player _currentPlayer;
    [HideInInspector] public Square _currentSquare;
    private Transform m_ThisTransform;
    [Range(0.5f, 1f)]
    [SerializeField] private float m_PlayerMoveTime = 0.5f;

    [Range(2f, 5f)]
    [SerializeField] private float m_EnemySearchFrequency;
    protected virtual void Start()
    {
        m_ThisTransform = transform;
        GameManager.Instance._sessionHandler._iGameSessionEvents.Add(this);
        _currentSquare._hasEnemy = true;
        InvokeRepeating("Move", 1f, m_EnemySearchFrequency);
    }

    private void OnDestroy()
    {
        GameManager.Instance._sessionHandler._iGameSessionEvents.Remove(this);
    }

    private void Move()
    {
        Square nextSquare = NextSquare(_currentSquare);
        if (nextSquare != null)
        {
            Debug.Log("HasEnemy>" + nextSquare._hasEnemy + gameObject.name);
            _currentSquare._hasEnemy = false;
            _currentSquare = nextSquare;
            _currentSquare._hasEnemy = true;
            m_ThisTransform.parent = _currentSquare.transform;
            m_ThisTransform.DOLocalMove(new Vector3(0f, 0f, 0f), m_PlayerMoveTime).onComplete = new TweenCallback(OnCompleteMove);
        }
        
    }

    protected virtual Square NextSquare(Square currentSquare)
    {
        return null;
    }

    protected bool CanGo(Square destination)
    {
        return destination != null && !destination.IsBlocked && !destination._hasEnemy;
    }

    private void OnCompleteMove()
    {
        GameManager.Instance._sessionHandler.OnEnemyMovingEnd();
    }

    #region GameSessionEvent
    public void OnEnemyMoveEnd()
    {
        if (m_ThisTransform.parent == _currentPlayer.transform.parent)
        {
            CancelInvoke();
            InfoPopup.CreateInfoPopup("GameOver\nTotalScore: " + GameManager.Instance._sessionHandler._playerScore, "RESTART",GameManager.Instance._sessionHandler.OnRestart);
        }
    }

    public void OnPlayerMoveEnd()
    {
        if (m_ThisTransform.parent == _currentPlayer.transform.parent)
        {
            CancelInvoke();
            InfoPopup.CreateInfoPopup("GameOver\nTotalScore: " + GameManager.Instance._sessionHandler._playerScore, "RESTART", GameManager.Instance._sessionHandler.OnRestart);
        }
    }

    public void OnPlayerReach() 
    {
        if (GameManager.Instance._sessionHandler._playerScore < GameManager.Instance._sessionHandler._totalDiamonds)
            return;
        InfoPopup.CreateInfoPopup("YOU WON!!!\nTotalScore: " + GameManager.Instance._sessionHandler._playerScore, "RESTART", GameManager.Instance._sessionHandler.OnRestart);
    }
    #endregion


}
