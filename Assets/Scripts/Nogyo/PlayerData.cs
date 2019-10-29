using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * セーブデータ用のクラス
 * これもシングルトンにしました
 */

public sealed class PlayerData:MonoBehaviour
{
    // 唯一のインスタンス
    private static PlayerData instance = new PlayerData(false);

    // インスタンスのゲッタ
    public static PlayerData getinstance()
    {
        return instance;
    }


    public int day { get; private set; }
    public Wallet wallet;
    public Dictionary<BalconyState.BALCONY, BalconyState> balconies;
    public ItemBox itembox;

    private PlayerData(bool load)
    {
        switch (load)
        {
            case true:
                loadGame();
                break;
            case false:
                newGame();
                break;
        }


        fortest();
    }


    /* にゅーげーむ */
    void newGame()
    {
        day = 0;
        wallet = new Wallet(1000);
        balconies = new Dictionary<BalconyState.BALCONY, BalconyState>();
        itembox = new ItemBox();

        // newバルコニー作成
        List<int[]> plantpos = new List<int[]>();
        plantpos.Add(new int[] { 0, 0 }); plantpos.Add(new int[] { 1, 0 }); plantpos.Add(new int[] { 2, 0 });
        createBalcony(BalconyState.BALCONY.Balcony1, plantpos);
    }

    /* ろーどげーむ */
    void loadGame()
    {

    }



    /*
     * 新規に農園を用意
     */
    private void createBalcony(BalconyState.BALCONY name, List<int[]> plantpos)
    {
        balconies.Add(name, new BalconyState(plantpos));
    }



    void fortest()
    {
        // てすとアイテムリストの入力
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Water_Normal"], 99);
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Chemi_Normal"], 3);
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Seed_GMary"], 3);
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Seed_WClover"], 3);
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Harv_GMary"], 1);
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Harv_WClover"], 2);
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Harv_Mikan"], 9);
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Harv_SBerry"], 3);
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Harv_Apple"], 1);
        itembox.changeItemNum(NogyoItemDB.getinstance().db["Harv_Grape"], 1);
        itembox.showItems();

    }


    /* 所持アイテム一覧を表示 */
    public void openItemListMenu()
    {
        GameObject ItemList = Instantiate(Resources.Load<GameObject>("Prefab/Nogyo/ItemMenuPanel"), GameObject.Find("NogyoCanvas").transform);
        ItemList.GetComponent<ItemMenu>().Activate(itembox);
    }
}
