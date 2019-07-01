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

    bool inScenario; // シナリオ進行中フラグ

    List<GameObject> emotionalBaloons;

    private void Update()
    {
        // シナリオ進行中は排他
        if (!inScenario)
        {
            // 画面クリックされたら進行(シナリオ終了まで)
            if (Input.GetMouseButtonDown(0) && currentline > 0)
            {
                if (currentline == mapinfo.mapscenarioarrays.Length)
                {
                    // ストーリー的な諸々の清算
                    endStory();

                    // SRPGの開始
                    gameObject.GetComponent<GameMgr>().startSRPG();
                }
                else
                {
                    // シナリオを一行分進行
                    StartCoroutine(advanceScenario());
                }
            }
        }

    }

    public void init(mapinfo mapinfo, int[] unitids)
    {
        emotionalBaloons = new List<GameObject>();
        this.mapinfo = mapinfo;
        if (unitids.Length == 0) unitids = UnitStatusUtil.randunit(2); // ユニット指定がされていなかったらランダムに
        this.unitids = unitids;

        messagewindow = Instantiate(Resources.Load<GameObject>("Prefab/MessageWindow"), GameObject.Find("Canvas").transform);

        // 最初のシナリオ行を実行
        currentline = 0;
        StartCoroutine( advanceScenario());
    }

    // シナリオを一行分進行
    IEnumerator advanceScenario()
    {
        inScenario = true;

        // 前回分の削除
        clearEmotion();

        // シナリオ一行の中の全アクション分を処理
        foreach (Mapscenario mapscenario in mapinfo.mapscenarioarrays[currentline].mapscenario)
        {
            updateMessageText(mapscenario);

            if (mapscenario.action == (int)STORYACTION.APPEAR)
            {
                // ユニット出現処理
                unitAppear(mapscenario);
                yield return new WaitForSeconds(0.3f);
                updateMessageSprite(null);
            }
            else
            {
                // アクション対象ユニットを取得
                GameObject actionunit = null;
                switch (mapscenario.camp)
                {
                    case 1:
                        actionunit  = gameObject.GetComponent<Map>().allyUnitList[mapscenario.unitno];
                        break;
                    case -1:
                        actionunit = gameObject.GetComponent<Map>().enemyUnitList[mapscenario.unitno];
                        break;
                }


                // 
                updateEmotion(mapscenario, actionunit);

                // メッセージウィンドウのSpriteとカーソル位置を更新
                // 今のシナリオ中に複数アクションがある場合は最後のアクションのみ実行
                if (mapscenario == mapinfo.mapscenarioarrays[currentline].mapscenario[mapinfo.mapscenarioarrays[currentline].mapscenario.Length - 1])
                {
                    yield return new WaitForSeconds(0.05f);
                    updateMessageSprite(actionunit);
                    updateCursor(actionunit);
                }
            }

        }

        currentline++;

        inScenario = false;
    }

    // ユニット出現の処理
    void unitAppear(Mapscenario mapscenario)
    {
        switch (mapscenario.camp)
        {
            case 1:
                gameObject.GetComponent<Map>().setUnitFromId(unitids[mapscenario.unitno], CAMP.ALLY);
                gameObject.GetComponent<Map>().allyUnitList[gameObject.GetComponent<Map>().allyUnitList.Count - 1].GetComponent<Animator>().SetBool("inStory",true);
                break;
            case -1:
                string enemyinfo = gameObject.GetComponent<Map>().mapinformation.enemy[gameObject.GetComponent<Map>().enemyUnitList.Count];
                int enemyunitid = int.Parse(enemyinfo.Split('-')[2]);
                gameObject.GetComponent<Map>().setUnitFromId(enemyunitid, CAMP.ENEMY);
                gameObject.GetComponent<Map>().enemyUnitList[gameObject.GetComponent<Map>().enemyUnitList.Count - 1].GetComponent<Animator>().SetBool("inStory", true);
                break;

        }
    }

    // Emotionを更新
    void updateEmotion(Mapscenario mapscenario, GameObject actionunit)
    {
        // エモーショナルバルーンを追加
        GameObject emotionalBaloon = Instantiate(Resources.Load<GameObject>("Prefab/EmotionalBaloon"), actionunit.transform);
        emotionalBaloons.Add(emotionalBaloon);

        switch (mapscenario.action)
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

        // Spriteの更新
        actionunit.GetComponent<Animator>().SetInteger("storyState", mapscenario.action);
    }

    // 出現中のエモーショナルバルーンを削除
    void clearEmotion()
    {
        while (emotionalBaloons.Count > 0)
        {
            GameObject tmpbaloon = emotionalBaloons[0];
            emotionalBaloons.RemoveAt(0);
            Destroy(tmpbaloon);
        }
    }

    // メッセージウィンドウのテキストを更新
    void updateMessageText(Mapscenario mapscenario)
    {
        messagewindow.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text
                    = mapscenario.message;
    }

    // メッセージウィンドウの画像を更新
    void updateMessageSprite(GameObject actionunit)
    {
        GameObject image = messagewindow.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Transform>().GetChild(0).gameObject; //あほっぽいから修正したい
        if (actionunit != null)
        {
            image.GetComponent<Image>().sprite = actionunit.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            // 無地イメージにしたい
        }

    }

    // カーソル位置を更新
    void updateCursor(GameObject actionunit)
    {
        gameObject.GetComponent<GameMgr>().cursor.GetComponent<cursor>().moveCursolToUnit(actionunit);
    }

    // ストーリー修了時の諸々の清算
    void endStory()
    {
        currentline = 0;
        Destroy(messagewindow);
        clearEmotion();

        foreach( GameObject ally in gameObject.GetComponent<Map>().allyUnitList)
        {
            ally.GetComponent<Animator>().SetInteger("storyState", 0);
            ally.GetComponent<Animator>().SetBool("inStory", false);
        }
        foreach (GameObject enemy in gameObject.GetComponent<Map>().enemyUnitList)
        {
            enemy.GetComponent<Animator>().SetInteger("storyState", 0);
            enemy.GetComponent<Animator>().SetBool("inStory", false);
        }
    }

}
