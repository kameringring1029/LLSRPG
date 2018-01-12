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
        if(selectedUnits.Count < 3)
        {
            // リスト上の変更
            selectedUnits.Add(unitid);
            // UI上の変更
            unitButtons[unitid - 1].GetComponent<RectTransform>().position = selectedUnitArea[selectedUnits.Count - 1].GetComponent<RectTransform>().position;


        }

    }

    //--- 指定したユニットを非選択中に ---//
    public void unselectUnit(int unitid)
    {
        // リスト上の変更
        selectedUnits.Remove(unitid);
        // UI上の変更
        unitButtons[unitid - 1].GetComponent<RectTransform>().position = unitButtonsArea[unitid - 1].GetComponent<RectTransform>().position;
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            unitButtons[selectedUnits[i] - 1].GetComponent<RectTransform>().position = selectedUnitArea[i].GetComponent<RectTransform>().position;
        }
    }




    //--- ユニット選択タブの切り替え ---//
    public void displayMuse()
    {
        nowgroup = UNITGROUP.MUSE;

        // musepanelを親の中で最前面に, タブボタンを明るく
        musePanel.transform.SetAsLastSibling();
        displayMuseButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        displayAqoursButton.GetComponent<Image>().color = new Color(155f/255f, 155f/255f, 155f/255f, 255f); 

        // カーソル移動処理
        wholecursor = -1; pushArrow(0, 1); 
    }
    public void displayAqours()
    {
        nowgroup = UNITGROUP.AQOURS;

        // aqourspanelを親の中で最前面に, タブボタンを明るく
        aqoursPanel.transform.SetAsLastSibling();
        displayAqoursButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        displayMuseButton.GetComponent<Image>().color = new Color(155f/255f, 155f/255f, 155f/255f, 255f); 

        // カーソル移動処理
        wholecursor = 18; pushArrow(0, 1); 
    }



    //==== コントローラ対応制御 ====//


    public void pushArrow(int x, int y) {

        GameObject nextCursorTarget = null;

        if (wholecursor < 100)
        {
            // 選択できるユニットが見つかるまでカーソル移動
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

        // カーソル移動
        wholecursorIcon.GetComponent<RectTransform>().position = nextCursorTarget.GetComponent<RectTransform>().position;

    }

    public void pushA() {
        if (wholecursor < 100)
        {
            // ユニット選択
            selectUnit(wholecursor+1);
        }
        else if(wholecursor == 100)
        {
            // ユニット選択完了
            GameObject.Find("Main Camera").GetComponent<WholeMgr>().startGame();
        }
        else if(wholecursor > 100)
        {
            unselectUnit(wholecursor - 100 +1);

            if (selectedUnits.Count == 0)
            {
                switch (nowgroup)
                {
                    case UNITGROUP.MUSE:
                        displayMuse();
                        break;
                    case UNITGROUP.AQOURS:
                        displayAqours();
                       break;
                   case UNITGROUP.ENEMY:
                        wholecursor = 100;
                        wholecursorIcon.GetComponent<RectTransform>().position = unitSelectOkButton.GetComponent<RectTransform>().position;
                        break;
                }
            }
       }
    }

    public void pushB()
    {
        //TODO 決定キャンセルにしたい
        if(wholecursor < 101 && selectedUnits.Count>0)
        {
            wholecursor = 101;
            wholecursorIcon.GetComponent<RectTransform>().position = selectedUnitArea[0].GetComponent<RectTransform>().position;
        }
        else
        {
            switch (nowgroup)
            {
                case UNITGROUP.MUSE:
                    displayMuse();
                    break;
                case UNITGROUP.AQOURS:
                    displayAqours();
                    break;
                case UNITGROUP.ENEMY:
                    wholecursor = 100;
                    wholecursorIcon.GetComponent<RectTransform>().position = unitSelectOkButton.GetComponent<RectTransform>().position;
                    break;
            }

        }
    }

    public void pushR()
    {
        // グループの切り替え　ENEMYは決定ボタン
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
        // グループの切り替え(逆順)　ENEMYは決定ボタン
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


    //==== コントローラ対応制御ここまで ====//
}
