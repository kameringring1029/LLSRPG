using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitMenu : MonoBehaviour {

    private int nowCursorPosition = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //--- Menu中のカーソルを移動 ---//
    // selectedUnit: 現在選択中のUnit
    public void moveCursor(int vector, Unit selectedUnit){

        List<Unit.ACTION> unitActionList = selectedUnit.getActionableList();

        nowCursorPosition += vector;

        // カーソル位置がオーバーフローしたとき
        if (nowCursorPosition < 0) nowCursorPosition = unitActionList.Count -1;
        if (nowCursorPosition > unitActionList.Count -1) nowCursorPosition = 0;

        gameObject.GetComponent<Text>().text = createMenuText(unitActionList);
    }

    
    //--- UnitのアクションパターンリストからMenuのテキストを作成 ---//
    // selectedUnit: 現在選択中のUnit
    // return; アクションMenuのText
    private string createMenuText(List<Unit.ACTION> unitActionList)
    {
        string menutext = "";

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
                case Unit.ACTION.ATTACK:
                    menutext += "こうげき";
                    break;

                case Unit.ACTION.HEAL:
                    menutext += "かいふく";
                    break;

                case Unit.ACTION.REACTION:
                    menutext += "うたう";
                    break;

                case Unit.ACTION.WAIT:
                    menutext += "たいき";
                    break;

                default:
                    break;
            }

            if(i != unitActionList.Count - 1)
                menutext += "\n";
            

        }


        return menutext;
    }



    public int getSelectedAction()
    {
        return nowCursorPosition;
    }
}
