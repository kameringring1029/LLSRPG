using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using General;


/*
 * SRPGゲーム中に表示するユニット行動メニューを制御する
 */

public class UnitMenu : MonoBehaviour {

    private int nowCursorPosition = 0;
    private List<ACTION> actionList = new List<ACTION>();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //--- Menu中のカーソルを移動 ---//
    // selectedUnit: 現在選択中のUnit
    public void moveCursor(int vector, Unit selectedUnit){

        List<ACTION> unitActionList = selectedUnit.getActionableList();

        nowCursorPosition += vector;

        // カーソル位置がオーバーフローしたとき
        if (nowCursorPosition < 0) nowCursorPosition = unitActionList.Count -1;
        if (nowCursorPosition > unitActionList.Count -1) nowCursorPosition = 0;

        gameObject.GetComponent<Text>().text = createMenuText(unitActionList);
    }

    
    //--- UnitのアクションパターンリストからMenuのテキストを作成 ---//
    // selectedUnit: 現在選択中のUnit
    // return; アクションMenuのText
    private string createMenuText(List<ACTION> unitActionList)
    {
        string menutext = "";
        actionList.Clear();

        // アクションパターンリストを上から並べる
        for (int i = 0; i < unitActionList.Count; i++)
        {
            if (nowCursorPosition == i)
            {
                menutext += "⇒";
            }
            else
            {
                menutext += "　";
            }
                      

            switch (unitActionList[i])
            {
                case ACTION.ATTACK:
                    menutext += "こうげき";
                    actionList.Add(ACTION.ATTACK);
                    break;

                case ACTION.HEAL:
                    menutext += "かいふく";
                    actionList.Add(ACTION.HEAL);
                    break;

                case ACTION.REACTION:
                    menutext += "うたう";
                    actionList.Add(ACTION.REACTION);
                    break;

                case ACTION.WAIT:
                    menutext += "たいき";
                    actionList.Add(ACTION.WAIT);
                    break;

                default:
                    break;
            }

            if(i != unitActionList.Count - 1)
                menutext += "\n";
            

        }


        return menutext;
    }



    public ACTION getSelectedAction()
    {
        return actionList[nowCursorPosition];
    }
}
