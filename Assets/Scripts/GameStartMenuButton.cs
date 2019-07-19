using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartMenuButton : MonoBehaviour {

	public void onClickGame()
    {
        Debug.Log("onClickGame(0");
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().selectMode(General.WHOLEMODE.GAME);

        GameObject.Find("Main Camera").GetComponent<GameMgr>().playFirst = true;

    }
    public void onClickRoom()
    {
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().selectMode(General.WHOLEMODE.ROOM);

    }
    public void onClickOther()
    {
        GameObject.Find("Main Camera").GetComponent<WholeMgr>().selectMode(General.WHOLEMODE.SELECTMAP);

    }

    public void onClickGameTest()
    {
        Debug.Log("onClickGameTest");

        GameObject.Find("Main Camera").GetComponent<WebsocketAccessor>().enabled = true;

        GameObject.Find("Main Camera").GetComponent<WholeMgr>().selectMode(General.WHOLEMODE.SELECT_GAMEROOM);

        GameObject.Find("Main Camera").GetComponent<GameMgr>().playFirst = false ;

    }
}
