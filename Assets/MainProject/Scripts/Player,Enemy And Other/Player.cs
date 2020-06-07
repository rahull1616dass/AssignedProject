using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHudEvent
{
    private Square m_CurrentSquare;
    private Transform m_ThisTransform;
    private float m_PlayerScale;
    [Range(0.5f, 1f)]
    [SerializeField] private float m_PlayerMoveTime = 0.5f;
    public Square CurrentSquare { set => m_CurrentSquare = value; }

    private void Start()
    {
        m_ThisTransform = transform;
        m_PlayerScale = m_ThisTransform.localScale.x;
        GameManager.Instance._uiHud._iHudEvent.Add(this);
    }
    private void OnDestroy()
    {
        GameManager.Instance._uiHud._iHudEvent.Remove(this);
    }

    #region IHudEvent
    public void OnDown()
    {
        if (m_CurrentSquare._downSquare != null && !m_CurrentSquare._downSquare.IsBlocked)
        {
            m_CurrentSquare._hasPlayer = false;
            m_CurrentSquare = m_CurrentSquare._downSquare;
            m_ThisTransform.localScale = new Vector3(m_PlayerScale, m_PlayerScale, m_PlayerScale);
            m_ThisTransform.localEulerAngles = new Vector3(0f, 0f, -90f);
            ChangeSquare();
        }  
        else
        {
            Debug.Log("EndSquare");
            OnCompleteMove();
        }
    }
    public void OnLeft()
    {
        if (m_CurrentSquare._leftSquare != null && !m_CurrentSquare._leftSquare.IsBlocked)
        {
            m_CurrentSquare._hasPlayer = false;
            m_CurrentSquare = m_CurrentSquare._leftSquare;
            m_ThisTransform.localScale = new Vector3(-1f*m_PlayerScale, m_PlayerScale, m_PlayerScale);
            m_ThisTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
            ChangeSquare();
        }
        else
        {
            Debug.Log("EndSquare");
            OnCompleteMove();
        }
    }

    public void OnRight()
    {
        if (m_CurrentSquare._rightSquare != null && !m_CurrentSquare._rightSquare.IsBlocked)
        {
            m_CurrentSquare._hasPlayer = false;
            m_CurrentSquare = m_CurrentSquare._rightSquare;
            m_ThisTransform.localScale = new Vector3(m_PlayerScale, m_PlayerScale, m_PlayerScale);
            m_ThisTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
            ChangeSquare();
        }
        else
        {
            Debug.Log("EndSquare");
            OnCompleteMove();
        }
    }

    public void OnUp()
    {
        if (m_CurrentSquare._upSquare != null && !m_CurrentSquare._upSquare.IsBlocked)
        {
            m_CurrentSquare._hasPlayer = false;
            m_CurrentSquare = m_CurrentSquare._upSquare;
            m_ThisTransform.localScale = new Vector3(m_PlayerScale, m_PlayerScale, m_PlayerScale);
            m_ThisTransform.localEulerAngles = new Vector3(0f, 0f, 90f);
            ChangeSquare();
        }
        else
        {
            Debug.Log("EndSquare");
            OnCompleteMove();
        }
    }
    #endregion

    private void ChangeSquare()
    {
        m_CurrentSquare._hasPlayer = true;
        m_ThisTransform.parent = m_CurrentSquare.transform;
        m_ThisTransform.DOLocalMove(new Vector3(0f, 0f, 0f), m_PlayerMoveTime).onComplete = new TweenCallback(OnCompleteMove);

        if (m_CurrentSquare._diamond != null)
        {
            GameManager.Instance._sessionHandler._playerScore++;
            Destroy(m_CurrentSquare._diamond);
        }
    }

    private void OnCompleteMove()
    {
        GameManager.Instance._sessionHandler.OnPlayerEndMoving();
        if (m_CurrentSquare.IsEnd)
        {
            GameManager.Instance._sessionHandler.OnEndReach();
        }
    }
}
