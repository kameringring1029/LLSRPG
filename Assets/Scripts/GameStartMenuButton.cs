using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartMenuButton : MonoBehaviour {

	public void onClickGame()
    {
        Debug.Log("onClickGame(0");
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().selectGame();

    }
    public void onClickRoom()
    {
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().selectRoom();

    }
    public void onClickOther()
    {
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().selectOther();

    }
}
