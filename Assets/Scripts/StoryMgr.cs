using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Information;
using General;
using UnityEngine.UI;

public class StoryMgr : MonoBehaviour
{
    GameObject messagewindow;
    mapinfo mapinfo;
    int[] unitids; // 指定した参加ユニット
    int currentline = 0; // シナリオの現在の進行数

    List<GameObject> emotionalBaloons;

    private void Update()
    {
        // 画面クリックされたら進行(シナリオ終了まで)
        if (Input.GetMouseButtonDown(0) && currentline > 0) {
            if (currentline == mapinfo.mapscenarioarrays.Length) {
                // ストーリー的な諸々の清算
                currentline = 0;
                Destroy(messagewindow);
                clearEmotion();

                // SRPGの開始
                gameObject.GetComponent<GameMgr>().startSRPG();
            }
            else
            {
                advanceScenario();
            }
        }
    }

    public void init(mapinfo mapinfo, int[] unitids)
    {
        this.mapinfo = mapinfo;
        this.unitids = unitids;


       // gameObject.GetComponent<Map>().positioningAllyUnits(unitids);
        gameObject.GetComponent<Map>().positioningEnemyUnits();

        messagewindow = Instantiate(Resources.Load<GameObject>("Prefab/MessageWindow"), GameObject.Find("Canvas").transform);

        emotionalBaloons = new List<GameObject>();

        currentline = 0;
        advanceScenario();
    }

    // シナリオ進行
    void advanceScenario()
    {
        updateMessageText();

        if (mapinfo.mapscenarioarrays[currentline].mapscenario[0].action == (int)STORYACTION.APPEAR)
        {
            gameObject.GetComponent<Map>().setUnitFromId(unitids[mapinfo.mapscenarioarrays[currentline].mapscenario[0].unitno], CAMP.ALLY);
        }
        else
        {
            GameObject actionunit = gameObject.GetComponent<Map>().allyUnitList[mapinfo.mapscenarioarrays[currentline].mapscenario[0].unitno];

            updateMessageSprite(actionunit);
            updateCursor(actionunit);
            updateEmotion(actionunit);
        }

        currentline++;
    }

    // メッセージウィンドウのテキストを更新
    void updateMessageText()
    {
        messagewindow.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text
                    = mapinfo.mapscenarioarrays[currentline].mapscenario[0].message;
    }

    // メッセージウィンドウの画像を更新
    void updateMessageSprite(GameObject actionunit)
    {
        GameObject image = messagewindow.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Transform>().GetChild(0).gameObject; //あほっぽいから修正したい
        int membernum = actionunit.GetComponent<Unit>().unitInfo.member_number;

        image.GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Unit/aqours0" + membernum + "-GF/aqours0"+membernum+"-GF_charchip_stand");
    }

    // カーソル位置を更新
    void updateCursor(GameObject actionunit)
    {
        gameObject.GetComponent<GameMgr>().cursor.GetComponent<cursor>().moveCursolToUnit(actionunit);
    }

    void updateEmotion(GameObject actionunit)
    {
        // 前回分の削除
        clearEmotion();

        // 追加

        GameObject emotionalBaloon = Instantiate( Resources.Load<GameObject>("Prefab/EmotionalBaloon"), actionunit.transform);
        emotionalBaloons.Add(emotionalBaloon);

        switch (mapinfo.mapscenarioarrays[currentline].mapscenario[0].action)
        {
            case 2:
                emotionalBaloon.GetComponent<Animator>().runtimeAnimatorController
                    = Resources.Load<RuntimeAnimatorController>("Emotion/drown");
                break;
            case 3:
                emotionalBaloon.GetComponent<Animator>().runtimeAnimatorController
                    = Resources.Load<RuntimeAnimatorController>("Emotion/fine");
                break;
        }
    }

    void clearEmotion()
    {
        while (emotionalBaloons.Count > 0)
        {
            GameObject tmpbaloon = emotionalBaloons[0];
            emotionalBaloons.RemoveAt(0);
            Destroy(tmpbaloon);
        }
    }

}
