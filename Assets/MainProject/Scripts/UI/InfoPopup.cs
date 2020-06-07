using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfoPopup : BasePopup
{

    private static InfoPopup Instance;
    public static void CreateInfoPopup(string Info, string ButtonText, Action OnClose = null)
    {
        if (Instance == null)
            Instance = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/UIInfoPopupCanvas")).GetComponent<InfoPopup>();
        Instance.StructurePopup(Info, ButtonText, OnClose);

    }

    public void StructurePopup(string Info, string ButtonText, Action OnClose)
    {
        base.CreateBasePopUp(Info, ButtonText, OnClose);
    }
}
