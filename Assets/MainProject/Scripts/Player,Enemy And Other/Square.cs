using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private Color m_StartColor, m_EndColor;

    [HideInInspector] public Square _leftSquare;
    [HideInInspector] public Square _rightSquare;
    [HideInInspector] public Square _upSquare;
    [HideInInspector] public Square _downSquare;

    private bool m_IsStart;
    private bool m_IsEnd;
    private bool m_IsBlocked;
    [HideInInspector] public bool _hasEnemy;
    [HideInInspector] public bool _hasPlayer;
    [HideInInspector] public GameObject _diamond;

    public bool IsBlocked { get => m_IsBlocked; }
    public bool IsEnd { get => m_IsEnd; }
    public bool IsStart { get => m_IsStart; }

    public void SetAsFirstSquare()
    {
        GetComponent<SpriteRenderer>().color = m_StartColor;
        m_IsStart = true;
    }

    public void SetAsLastSquare()
    {
        GetComponent<SpriteRenderer>().color = m_EndColor;
        m_IsEnd = true;
    }

    public void SetAsBlocked()
    {
        m_IsBlocked = true;
    }
}
