using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHudEvent
{
    void OnLeft();
    void OnRight();
    void OnUp();
    void OnDown();
}
public class GameUIHud : MonoBehaviour, ISessionEvent
{
    [SerializeField] private Button[] m_AllButtons;

    public List<IHudEvent> _iHudEvent = new List<IHudEvent>();

    private void Start()
    {
        GameManager.Instance._sessionHandler._iGameSessionEvents.Add(this);
    }
    private void OnDestroy()
    {
        GameManager.Instance._sessionHandler._iGameSessionEvents.Remove(this);
    }

    #region ControlButtons
    public void OnClickLeft()
    {
        DisableButtons();
        _iHudEvent.ForEach(x => x.OnLeft());
    }

    public void OnClickRight()
    {
        DisableButtons();
        _iHudEvent.ForEach(x => x.OnRight());
    }

    public void OnClickUp()
    {
        DisableButtons();
        _iHudEvent.ForEach(x => x.OnUp());
    }

    public void OnClickDown()
    {
        DisableButtons();
        _iHudEvent.ForEach(x => x.OnDown());
    }
    #endregion
    private void EnableButtons()
    {
        Debug.Log("EnableButtons");
        foreach(Button button in m_AllButtons)
        {
            button.interactable = true;
        }
    }
    private void DisableButtons()
    {
        foreach (Button button in m_AllButtons)
        {
            button.interactable = false;
        }
    }

    #region IGameSessionEvent
    public void OnPlayerMoveEnd()
    {
        EnableButtons();
    }

    public void OnEnemyMoveEnd(){ }

    public void OnPlayerReach() { }
    #endregion
}
