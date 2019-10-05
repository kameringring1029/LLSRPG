using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using General;
using Information;
using UnityEngine.UI;

public class NogyoMgr : MonoBehaviour
{
    PlayerData playerdata;
    public int selectedBlockId;

    public enum NOWMODE { Main, OpeningMenu}
    NOWMODE mode;

    GameObject openingItemMenu;

    // Start is called before the first frame update
    void Start()
    {
        mode = NOWMODE.Main;
        openingItemMenu = null;
        fortest();
    }

    void Update()
    {
        // itemメニュー画面が開いているとき
        if (openingItemMenu)
        {
            //itemが指定されたら
            if (openingItemMenu.GetComponent<CareMenu>().selected != "")
            {
                if (openingItemMenu.GetComponent<CareMenu>().selected != "END") // キャンセルじゃなかったら
                {
                   NogyoItem selecteditem = null; // アイテムIDからアイテム情報を取得
                   foreach (KeyValuePair<NogyoItem, int> pair in playerdata.itembox.items)
                   {
                      if (pair.Key.id == openingItemMenu.GetComponent<CareMenu>().selected) selecteditem = pair.Key;
                   }

                   // care種別で処理分岐
                   switch (openingItemMenu.GetComponent<CareMenu>().care)
                    {
                       case NogyoItem.NogyoItemGroup.Seed:
                            playerdata.itembox.changeItemNum(selecteditem, -1);

                            playerdata.balconies[BalconyState.BALCONY.Balcony1].plantProduce(selectedBlockId, selecteditem.producetype);
                            renewBalcorySprites(BalconyState.BALCONY.Balcony1);
 
                            break;
                    }
               }

                Destroy(openingItemMenu.transform.parent.gameObject);
                openingItemMenu = null;
                mode = NOWMODE.Main;
            }
        }
    }

    void fortest()
    {
       // プレイヤーデータ新規作成
        playerdata = new PlayerData();
        
        // てすとバルコニー作成
        List<int[]> plantpos = new List<int[]>();
        plantpos.Add(new int[] { 0, 0 }); plantpos.Add(new int[] { 1, 0 }); plantpos.Add(new int[] { 2, 0 });
        playerdata.createBalcony(BalconyState.BALCONY.Balcony1, plantpos);
               
        // プラントSpriteを配置
        gameObject.GetComponent<Garden>().positioningPlants();

        // Sprite更新
        renewBalcorySprites(BalconyState.BALCONY.Balcony1);


        // てすとアイテムリストの入力
        playerdata.itembox.changeItemNum(playerdata.itembox.db.db["Seed_GMary"], 3);
        playerdata.itembox.changeItemNum(playerdata.itembox.db.db["Seed_WClover"], 3);
        playerdata.itembox.changeItemNum(playerdata.itembox.db.db["Seed_WClover"], 3);
        playerdata.itembox.showItems();

    }


    /*対象のBalconyすべての作物のSriteをユーザデータをもとに更新 */
    void renewBalcorySprites(BalconyState.BALCONY balconystr)
    {
        for (int i = 0; i < playerdata.balconies[balconystr].plantpos.Count; i++)
        {
            BalconyState balcony = playerdata.balconies[balconystr];
            if (balcony.produces.ContainsKey(i))
            {
                gameObject.GetComponent<Garden>().renewProduce(balcony.plantpos[i], balcony.produces[i].type, balcony.produces[i].status);

            }
            else
            {
                gameObject.GetComponent<Garden>().renewProduce(balcony.plantpos[i], Produce.PRODUCE_TYPE.Not, Produce.PRODUCE_STATE.Vanish);
            }
        }

        renewViewInfo();
    }


    /* ViewPanelの情報更新 */
    void renewViewInfo()
    {
        /* View画像の変更 */
        int[] pos = playerdata.balconies[BalconyState.BALCONY.Balcony1].plantpos[selectedBlockId];
        Sprite spr = gameObject.GetComponent<Garden>().FieldBlocks[pos[0], pos[1]].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;

        if (spr != null)
            GameObject.Find("ProduceView").GetComponent<Image>().sprite = spr;

    }

    /* プラント選択 */
    public void selectPlant(int selected)
    {
        selectedBlockId = selected;
        renewViewInfo();
    }

    /* 一日の終りだよ */
    public void endDay()
    {
        playerdata.balconies[BalconyState.BALCONY.Balcony1].proceedProduceState(0);
        playerdata.balconies[BalconyState.BALCONY.Balcony1].proceedProduceState(1);
        playerdata.balconies[BalconyState.BALCONY.Balcony1].proceedProduceState(2);
        renewBalcorySprites(BalconyState.BALCONY.Balcony1);
    }



    /* おせわめにゅーひらく */
    void openCareMenu(NogyoItem.NogyoItemGroup care)
    {
        if(mode == NOWMODE.Main)
        {
            mode = NOWMODE.OpeningMenu;
            GameObject ItemMenuPanel = Instantiate(Resources.Load<GameObject>("Prefab/Nogyo/CareMenuPanel"), GameObject.Find("Canvas").transform);
            openingItemMenu = ItemMenuPanel.transform.GetChild(0).gameObject;
            openingItemMenu.GetComponent<CareMenu>().Activate(care, playerdata.itembox);
        }

    }
    public void openSeedMenu()
    {
        if (!playerdata.balconies[BalconyState.BALCONY.Balcony1].produces.ContainsKey(selectedBlockId)) // 作物が植えられていない場合のみ
        {
            openCareMenu(NogyoItem.NogyoItemGroup.Seed);
        }
        else if(playerdata.balconies[BalconyState.BALCONY.Balcony1].produces[selectedBlockId].status == Produce.PRODUCE_STATE.Harvest) //収穫
        {
            Produce harv = playerdata.balconies[BalconyState.BALCONY.Balcony1].harvestProduce(selectedBlockId);
            playerdata.itembox.changeItemNum(playerdata.itembox.db.getItemFromPType(NogyoItem.NogyoItemGroup.Flower, harv.type), 1);
            renewBalcorySprites(BalconyState.BALCONY.Balcony1);
        }
    }
    public void openWaterMenu()
    {
        openCareMenu(NogyoItem.NogyoItemGroup.Water);
    }
    public void openChemiMenu()
    {
        openCareMenu(NogyoItem.NogyoItemGroup.Chemi);
    }
}
