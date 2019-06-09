using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Information;
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
    /*(選択ユニットのSpriteを移動していた、廃止)
    private GameObject[] selectedUnitArea = new GameObject[3];
    */
    private GameObject displayMuseButton;
    private GameObject displayAqoursButton;
    private GameObject unitSelectOkButton;

    private GameObject unitDiscriptionTextStatus;
    private GameObject unitDiscriptionTextChara;
    private GameObject unitDiscriptionAnim;

    private GameObject joinStatus;



    public UnitSelect(GameObject wholecursorIcon)
    {

        wholecursorIcon.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 1), -90);
        wholecursorIcon.GetComponent<RectTransform>().localScale = wholecursorIcon.GetComponent<RectTransform>().localScale / 2;
        this.wholecursorIcon = wholecursorIcon;
        

        // 各種ゲームオブジェクトの取得
        // ユニットボタンについては存在するものは代入されるけどそうじゃなければNULL

        // ユニットボタンのオブジェクトを配列の該当unitidの位置に準備
        for (int i = 1; i <= 9; i++)
        {
            unitButtons[i] = GameObject.Find("ButtonMuse0" + (i));
            unitButtons[i + 1 + 9] = GameObject.Find("ButtonAqours0" + (i));

            unitButtonsArea[i] = GameObject.Find("Muse0" + (i));
            unitButtonsArea[i + 1 + 9] = GameObject.Find("Aqours0" + (i));

        }

        /*(選択ユニットのSpriteを移動していた、廃止)
        for (int i = 0; i < 3; i++) selectedUnitArea[i] = GameObject.Find("Selectedunit" + (i + 1));
        */

        displayMuseButton = GameObject.Find("DisplayMuseButton");
        displayAqoursButton = GameObject.Find("DisplayAqoursButton");
        unitSelectOkButton = GameObject.Find("UnitSelectOkButton");

        musePanel = GameObject.Find("UnitSelectMusePanel");
        aqoursPanel = GameObject.Find("UnitSelectAqoursPanel");

        unitDiscriptionTextStatus = GameObject.Find("UnitDiscriptionTextStatus");
        unitDiscriptionTextChara = GameObject.Find("UnitDiscriptionTextChara");
        unitDiscriptionAnim = GameObject.Find("UnitDiscriptionAnim");

        joinStatus = GameObject.Find("JoinStatus");

        // カーソル初期化
        displayAqours();

    }


    //--- 指定したユニットが選択中かどうか確認して選択メソッドか選択外しメソッドに移行 ---//
    public void switchselectUnit(int unitid)
    {
        // ユニット説明ウィンドウの更新
        //unitDiscriptionAnim.GetComponent<Animator>().SetInteger("unitid", unitid);

        // 各メソッドへ移行
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

            // UI上の変更(選択ユニットのSpriteを移動していた、廃止)
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

            // Discription欄の参加中表示
            joinStatus.GetComponent<Image>().color = Color.red;

        }
    }

    //--- 指定したユニットを非選択中に ---//
    public void unselectUnit(int unitid)
    {
        Debug.Log("unselect unitid :" + unitid);

        // リスト上の変更
        selectedUnits.Remove(unitid);

        // UI上の変更
        /*(選択ユニットのSpriteを移動していた、廃止)
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
 
        // Discription欄の非参加表示
        joinStatus.GetComponent<Image>().color = Color.blue;

   }




    //--- ユニット選択タブの切り替え ---//
    public void displayMuse()
    {
        nowgroup = UNITGROUP.MUSE;

        // aqourspanelを親の中で最背面に, タブボタンを明るく
        aqoursPanel.transform.SetAsFirstSibling();
        //musePanel.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        //aqoursPanel.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        displayAqoursButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        displayMuseButton.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);

        // カーソル移動処理
        wholecursor = -1; pushArrow(0, 1);
    }
    public void displayAqours()
    {
        nowgroup = UNITGROUP.AQOURS;

        // musepanelを親の中で最背面に, タブボタンを明るく
        musePanel.transform.SetAsFirstSibling();
       // aqoursPanel.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
       // musePanel.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
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
                if (wholecursor <= 0) wholecursor = 9;
                else if (wholecursor == 10 && x + y > 0) wholecursor = 1;
                else if (wholecursor == 11 && x + y < 0) wholecursor = 19;
                else if (wholecursor > 19) wholecursor = 11;

            } while (unitButtons[wholecursor] == null);

            nextCursorTarget = unitButtons[wholecursor];

        }
        else
        {

        }

        // カーソル移動
        wholecursorIcon.GetComponent<RectTransform>().position 
            = nextCursorTarget.GetComponent<RectTransform>().position + new Vector3(0,nextCursorTarget.GetComponent<RectTransform>().sizeDelta[1]/6,0);

        // ユニット説明ウィンドウの更新
        unitDiscriptionAnim.GetComponent<Animator>().SetInteger("unitid", wholecursor);
        unitDiscriptionTextStatus.GetComponent<TextMeshProUGUI>().text = UnitStatusUtil.outputUnitInfo(wholecursor);
        unitDiscriptionTextChara.GetComponent<TextMeshProUGUI>().text
            = "<size=24><b>とくちょう</b></size>\n\n" + UnitStatusUtil.search(wholecursor).status_description();

    }


    public void pushUnitButton(int unitid)
    {

        GameObject nextCursorTarget = null;

        wholecursor = unitid;
        nextCursorTarget = unitButtons[unitid];
            

        // カーソル移動
        wholecursorIcon.GetComponent<RectTransform>().position
            = nextCursorTarget.GetComponent<RectTransform>().position + new Vector3(0, nextCursorTarget.GetComponent<RectTransform>().sizeDelta[1] / 6, 0);

        // ユニット説明ウィンドウの更新
        unitDiscriptionAnim.GetComponent<Animator>().SetInteger("unitid", wholecursor);
        unitDiscriptionTextStatus.GetComponent<TextMeshProUGUI>().text = UnitStatusUtil.outputUnitInfo(wholecursor);
        unitDiscriptionTextChara.GetComponent<TextMeshProUGUI>().text
            = "<size=24><b>とくちょう</b></size>\n\n" + UnitStatusUtil.search(wholecursor).status_description();
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
        /*(選択ユニットのSpriteを移動していた、廃止)
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
            */
    }

    public void pushB()
    {
        //TODO 決定キャンセルにしたい

        // 決定ボタンにカーソル移動
        wholecursor = 100; nowgroup = UNITGROUP.ENEMY;
        wholecursorIcon.GetComponent<RectTransform>().position = unitSelectOkButton.GetComponent<RectTransform>().position;

        /*(選択ユニットのSpriteを移動していた、廃止)
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
            */
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
