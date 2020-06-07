using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISessionEvent
{
    void OnPlayerMoveEnd();
    void OnEnemyMoveEnd();
    void OnPlayerReach();
}
public class GameSessionHandler : MonoBehaviour
{
    public List<ISessionEvent> _iGameSessionEvents = new List<ISessionEvent>();
    [HideInInspector] public int _playerScore;
    public void OnPlayerEndMoving()
    {
        _iGameSessionEvents.ForEach(x => x.OnPlayerMoveEnd());
    }
    public void OnEnemyMovingEnd()
    {
        _iGameSessionEvents.ForEach(x => x.OnEnemyMoveEnd());
    }

    public void OnEndReach()
    {
        _iGameSessionEvents.ForEach(x => x.OnPlayerReach());
    }
    public void OnRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
