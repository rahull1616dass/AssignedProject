using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BasePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_IntroText;
    [SerializeField] private TextMeshProUGUI m_ButtonText;
    [SerializeField] private Button m_CloseButton;

    public void CreateBasePopUp(string Info, string ButtonText, Action OnClose = null)
    {
        m_ButtonText.text = ButtonText;
        m_IntroText.text = Info;
        m_CloseButton.onClick.AddListener(() => { OnClose?.Invoke(); DestroyPopup(); });
    }

    public void DestroyPopup()
    {
        Destroy(gameObject);
    }
}
