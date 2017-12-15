using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;
using UnityEngine.UI;


/*
 * ユニット選択画面のマネージャ用クラス
 * WholeMgrでインスタンス化
 */
public class UnitSelect{

    public List<int> selectedUnits = new List<int>();

    GameObject wholecursorIcon;
    int wholecursor;
    UNITGROUP nowgroup = UNITGROUP.MUSE;

    private GameObject musePanel;
    private GameObject aqoursPanel;

    private GameObject[] unitButtons = new GameObject[18];
    private GameObject[] unitButtonsArea = new GameObject[18];
    private GameObject[] selectedUnitArea = new GameObject[3];
    private GameObject displayMuseButton;
    private GameObject displayAqoursButton;
    private GameObject unitSelectOkButton;


	public UnitSelect (GameObject wholecursorIcon) {

        this.wholecursorIcon = wholecursorIcon;

        // 各種ゲームオブジェクトの取得
        // ユニットボタンについては存在するものは代入されるけどそうじゃなければNULL
        for (int i = 0; i < 9; i++)
        {
            unitButtons[i] = GameObject.Find("ButtonMuse0" + (i + 1));
            unitButtons[i + 9] = GameObject.Find("ButtonAqours0" + (i + 1));

            unitButtonsArea[i] = GameObject.Find("Muse0" + (i + 1));
            unitButtonsArea[i + 9] = GameObject.Find("Aqours0" + (i + 1));

        }

        for(int i=0;i<3;i++) selectedUnitArea[i] = GameObject.Find("Selectedunit"+(i+1));
        displayMuseButton = GameObject.Find("DisplayMuseButton");
        displayAqoursButton = GameObject.Find("DisplayAqoursButton");
        unitSelectOkButton = GameObject.Find("UnitSelectOkButton");

        musePanel = GameObject.Find("UnitSelectMusePanel");
        aqoursPanel = GameObject.Find("UnitSelectAqoursPanel");

        // カーソル初期化
        displayMuse();
  
		
	}
	

    //--- 指定したユニットを選択中に ---//
    public void selectUnit(int unitid)
    {
        selectedUnits.Add(unitid);
        unitButtons[unitid - 1].GetComponent<RectTransform>().position = selectedUnitArea[selectedUnits.Count - 1].GetComponent<RectTransform>().position;
    }

    //--- 指定したユニットを非選択中に ---//
    public void unselectUnit(int unitid)
    {
        selectedUnits.Remove(unitid);
        unitButtons[unitid - 1].GetComponent<RectTransform>().position = unitButtonsArea[unitid - 1].GetComponent<RectTransform>().position;
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            unitButtons[selectedUnits[i] - 1].GetComponent<RectTransform>().position = selectedUnitArea[i].GetComponent<RectTransform>().position;
        }
    }




    //--- ユニット選択タブの切り替え ---//
    public void displayMuse()
    {
        // musepanelを親の中で最前面に
        musePanel.transform.SetAsLastSibling();
        nowgroup = UNITGROUP.MUSE;
        displayMuseButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        displayAqoursButton.GetComponent<Image>().color = new Color(155f/255f, 155f/255f, 155f/255f, 255f); 
        wholecursor = -1; pushArrow(0, 1); 
    }
    public void displayAqours()
    {
        // aqourspanelを親の中で最前面に
        aqoursPanel.transform.SetAsLastSibling();
        nowgroup = UNITGROUP.AQOURS;
        displayAqoursButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        displayMuseButton.GetComponent<Image>().color = new Color(155f/255f, 155f/255f, 155f/255f, 255f); 
        wholecursor = 18; pushArrow(0, 1); 
    }




    public void pushArrow(int x, int y) {

        GameObject nextCursorTarget = null;


        if (wholecursor < 100)
        {

            do
            {
                wholecursor += x + y;
                if (wholecursor < 0) wholecursor = 8;
                else if (wholecursor == 9 && x + y > 0) wholecursor = 0;
                else if (wholecursor == 8 && x + y < 0) wholecursor = 17;
                else if (wholecursor > 17) wholecursor = 9;

            } while (unitButtons[wholecursor] == null);

            nextCursorTarget = unitButtons[wholecursor];
            
        }
        else
        {

        }

        wholecursorIcon.GetComponent<RectTransform>().position = nextCursorTarget.GetComponent<RectTransform>().position;

    }

    public void pushA() {
        if (wholecursor < 100)
        {
            selectUnit(wholecursor);
        }
        else if(wholecursor == 100)
        {
            GameObject.Find("Main Camera").GetComponent<WholeMgr>().startGame();
        }
    }

    public void pushB()
    {
            wholecursor = 100;
            wholecursorIcon.GetComponent<RectTransform>().position = unitSelectOkButton.GetComponent<RectTransform>().position;
    }

    public void pushR()
    {
        switch (nowgroup)
        {
            case UNITGROUP.MUSE:
                displayAqours();
                break;
            case UNITGROUP.AQOURS:
                wholecursor = 100; nowgroup = UNITGROUP.ENEMY;
                wholecursorIcon.GetComponent<RectTransform>().position = unitSelectOkButton.GetComponent<RectTransform>().position;
                break;
            case UNITGROUP.ENEMY:
                displayMuse();
                break;
        }

    }

    public void pushL() {
        switch (nowgroup)
        {
            case UNITGROUP.MUSE:
                wholecursor = 100; nowgroup = UNITGROUP.ENEMY;
                wholecursorIcon.GetComponent<RectTransform>().position = unitSelectOkButton.GetComponent<RectTransform>().position;
                break;
            case UNITGROUP.AQOURS:
                displayMuse();
                break;
            case UNITGROUP.ENEMY:
                displayAqours();
                break;
        }

 }

}
