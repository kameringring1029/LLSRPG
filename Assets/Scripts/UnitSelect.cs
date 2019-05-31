using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;
using UnityEngine.UI;


/*
 * ユニット選択画面のマネージャ用クラス
 * WholeMgrでインスタンス化
 */
public class UnitSelect : MonoBehaviour
{

    public List<int> selectedUnits = new List<int>();

    private List<GameObject> unitselectcursor = new List<GameObject>();

    GameObject wholecursorIcon;
    int wholecursor;
    UNITGROUP nowgroup = UNITGROUP.MUSE;

    private GameObject musePanel;
    private GameObject aqoursPanel;

    private GameObject[] unitButtons = new GameObject[20];
    private GameObject[] unitButtonsArea = new GameObject[20];
    private GameObject[] selectedUnitArea = new GameObject[3];
    private GameObject displayMuseButton;
    private GameObject displayAqoursButton;
    private GameObject unitSelectOkButton;



    public UnitSelect(GameObject wholecursorIcon)
    {

        wholecursorIcon.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 1), -90);
        wholecursorIcon.GetComponent<RectTransform>().localScale = wholecursorIcon.GetComponent<RectTransform>().localScale / 2;
        this.wholecursorIcon = wholecursorIcon;
        


        // 各種ゲームオブジェクトの取得
        // ユニットボタンについては存在するものは代入されるけどそうじゃなければNULL
        for (int i = 1; i <= 9; i++)
        {
            unitButtons[i] = GameObject.Find("ButtonMuse0" + (i));
            unitButtons[i + 1 + 9] = GameObject.Find("ButtonAqours0" + (i));

            unitButtonsArea[i] = GameObject.Find("Muse0" + (i));
            unitButtonsArea[i + 1 + 9] = GameObject.Find("Aqours0" + (i));

        }

        for (int i = 0; i < 3; i++) selectedUnitArea[i] = GameObject.Find("Selectedunit" + (i + 1));
        displayMuseButton = GameObject.Find("DisplayMuseButton");
        displayAqoursButton = GameObject.Find("DisplayAqoursButton");
        unitSelectOkButton = GameObject.Find("UnitSelectOkButton");

        musePanel = GameObject.Find("UnitSelectMusePanel");
        aqoursPanel = GameObject.Find("UnitSelectAqoursPanel");

        // カーソル初期化
        displayMuse();

    }


    //--- 指定したユニットが選択中かどうか確認して選択メソッドか選択外しメソッドに移行 ---//
    public void switchselectUnit(int unitid)
    {
        if (!selectedUnits.Contains(unitid))
        {
            selectUnit(unitid);
        }
        else
        {
            unselectUnit(unitid);
        }
    }


    //--- 指定したユニットを選択中に ---//
    public void selectUnit(int unitid)
    {
        Debug.Log("select unitid :"+unitid);

        if (selectedUnits.Count < 3)
        {
            // リスト上の変更
            selectedUnits.Add(unitid);
            // UI上の変更
            //unitButtons[unitid].GetComponent<RectTransform>().position = selectedUnitArea[selectedUnits.Count - 1].GetComponent<RectTransform>().position;


            // Resources/Unitフォルダから選択中グラをロード
            string imggrp = unitButtons[unitid].GetComponent<Image>().sprite.name.Split('_')[0];
            if (Resources.Load<Sprite>("Unit/" + imggrp + "/" + imggrp + "_charchip_reaction") != null)
            {
                unitButtons[unitid].GetComponent<Image>().sprite
                = Resources.Load<Sprite>("Unit/" + imggrp + "/" + imggrp + "_charchip_reaction");
            }

            // 選択中ユニットに選択中マークをつけるよ
            GameObject tmpcursor = Instantiate(Resources.Load<GameObject>("Prefab/unitcursor"));
            tmpcursor.transform.SetParent(unitButtons[unitid].transform,false);
            tmpcursor.name = "unitselectcursor" + unitid;
            unitselectcursor.Add(tmpcursor);

        }
    }

    //--- 指定したユニットを非選択中に ---//
    public void unselectUnit(int unitid)
    {
        Debug.Log("unselect unitid :" + unitid);

        // リスト上の変更
        selectedUnits.Remove(unitid);
        // UI上の変更
        /*
        unitButtons[unitid].GetComponent<RectTransform>().position = unitButtonsArea[unitid].GetComponent<RectTransform>().position;
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            Debug.Log(unitButtons[selectedUnits[i]]);
            unitButtons[selectedUnits[i]].GetComponent<RectTransform>().position = selectedUnitArea[i].GetComponent<RectTransform>().position;
        }
        */

        // Resources/Unitフォルダから通常グラをロード
        string imggrp = unitButtons[unitid].GetComponent<Image>().sprite.name.Split('_')[0];
        if (Resources.Load<Sprite>("Unit/" + imggrp + "/" + imggrp + "_charchip_stand") != null)
        {
            unitButtons[unitid].GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Unit/" + imggrp + "/" + imggrp + "_charchip_stand");
        }

        // 選択中マークを外すよ
        for (int i = 0; i < unitselectcursor.Count; i++)
        {
            GameObject tmpcursor = unitselectcursor[i];
            if (tmpcursor.name == "unitselectcursor" + unitid)
            {
                unitselectcursor.RemoveAt(i);
                Destroy(tmpcursor);
            }
        }
    }




    //--- ユニット選択タブの切り替え ---//
    public void displayMuse()
    {
        nowgroup = UNITGROUP.MUSE;

        // musepanelを親の中で最前面に, タブボタンを明るく
        musePanel.transform.SetAsLastSibling();
        musePanel.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        aqoursPanel.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        displayAqoursButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        displayMuseButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);

        // カーソル移動処理
        wholecursor = -1; pushArrow(0, 1);
    }
    public void displayAqours()
    {
        nowgroup = UNITGROUP.AQOURS;

        // aqourspanelを親の中で最前面に, タブボタンを明るく
        aqoursPanel.transform.SetAsLastSibling();
        aqoursPanel.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        musePanel.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        displayMuseButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        displayAqoursButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);

        // カーソル移動処理
        wholecursor = 18; pushArrow(0, 1);
    }



    //==== コントローラ対応制御 ====//


    public void pushArrow(int x, int y)
    {

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
        wholecursorIcon.GetComponent<RectTransform>().position 
            = nextCursorTarget.GetComponent<RectTransform>().position + new Vector3(0,nextCursorTarget.GetComponent<RectTransform>().sizeDelta[1]/5,0);

    }

    public void pushA()
    {
        if (wholecursor < 100)
        {
            // ユニット選択

            switchselectUnit(wholecursor);
        }
        else if (wholecursor == 100)
        {
            // ユニット選択完了
            GameObject.Find("Main Camera").GetComponent<WholeMgr>().startGame();
        }
        else if (wholecursor > 100)
        {
            switchselectUnit(wholecursor - 100);

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
        if (wholecursor < 101 && selectedUnits.Count > 0)
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

    public void pushL()
    {
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
