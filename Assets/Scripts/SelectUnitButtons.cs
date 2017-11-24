using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * SRPGゲームに入る前のユニット選択画面での処理
 */

public class SelectUnitButtons : MonoBehaviour {

    public int unitid;
    private bool isselected;

    private void Start()
    {
        isselected = false;
    }

    public void onClicked()
    {
        if (!isselected)
        {
            GameObject.Find("Main Camera").GetComponent<WholeMgr>().selectUnit(unitid);
            isselected = true;
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<WholeMgr>().unselectUnit(unitid);
            isselected = false;
        }
    }

    public void onClickOk()
    {
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().startGame();
    }

    public void onClickMuse()
    {
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().displayMuse();
    }

    public void onClickAqours()
    {
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().displayAqours();
    }

}
