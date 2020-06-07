using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameSessionHandler m_SessionHandler;
    [SerializeField] private GameUIHud m_UIHud;
    [SerializeField] private GameplayBoard m_Board;

    public GameSessionHandler _sessionHandler { get => m_SessionHandler; }
    public GameUIHud _uiHud { get => m_UIHud; }
    public GameplayBoard _board { get => m_Board; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
