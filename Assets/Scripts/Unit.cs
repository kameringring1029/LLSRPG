using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

public class Unit : MonoBehaviour {

    private int[] nowPosition = new int[2];
    protected GameMgr GM;
    protected GameObject cursor;
    private int camp;

    public int staticMoveVelocity = 3;

    public int[] moveVector = new int[2];
    private int[] moveTo = new int[2];

    public Unit_info unitInfo;


    public GameObject movableArea;
    private List<GameObject> movableAreaList = new List<GameObject>();
    public GameObject reachArea;
    private List<GameObject> reachAreaList = new List<GameObject>();


    // Use this for initialization
    void Start () {

        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();
        cursor = GameObject.Find("cursor");
    }

    public void init(int x, int y, int c, Unit_info unitinfo)
    {
        cursor = GameObject.Find("cursor");
        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();

        unitInfo = unitinfo; ;
        

        camp = c;
        if (camp == -1) gameObject.GetComponent<SpriteRenderer>().flipX = true;

        nowPosition[0] = 0;
        nowPosition[1] = 0;
        moveVector[0] = 0;
        moveVector[1] = 0;
        moveTo[0] = -1;
        moveTo[1] = -1;
        changePosition(x, y, false);
    }


    // Update is called once per frame
    void Update ()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2((moveVector[0] - moveVector[1]) * staticMoveVelocity, -(moveVector[0] / 2.0f + moveVector[1] / 2.0f) * staticMoveVelocity);


        //---- 移動処理（y軸移動->x軸移動）----//

        // y軸移動
        if (moveTo[1] != -1)
        {
            GM.gameScene = GameMgr.SCENE.UNIT_ACTIONING;


            // 移動元BlockからUnit情報を削除
            GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = null;


            // 移動方向の決定とUnitの向きの変更
            if (moveTo[1] - nowPosition[1] == 0)
            {
                moveVector[1] = 0;
            }
            else if (moveTo[1] - nowPosition[1] > 0)
            {
                moveVector[1] = 1;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                moveVector[1] = -1;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            

            // 目的座標まで動いたら
            //  Unitの現在座標と目的ブロックの座標を比較
            //  比較演算子の関係で移動方向を乗算
            if (gameObject.GetComponent<Transform>().position.y * moveVector[1]
                 <= GM.FieldBlocks[nowPosition[0], moveTo[1]].GetComponent<Transform>().position.y * moveVector[1])
            {
                moveVector[1] = 0;

                // ブロックの直上に調整
                gameObject.GetComponent<Transform>().position
                    = GM.FieldBlocks[nowPosition[0], moveTo[1]].GetComponent<Transform>().position;

                nowPosition[1] = moveTo[1];

                // 移動先BlockにUnit情報を設定
                GM.FieldBlocks[nowPosition[0], moveTo[1]].GetComponent<FieldBlock>().GroundedUnit = gameObject;
                
                moveTo[1] = -1;

                endUnitMoving();
            }

        }

        // x軸移動
        else if (moveTo[0] != -1)
        {
            GM.gameScene = GameMgr.SCENE.UNIT_ACTIONING;


            // 移動元BlockからUnit情報を削除
            GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = null;


            // 移動方向の決定とUnitの向きの変更
            if (moveTo[0] - nowPosition[0] == 0)
            {
                moveVector[0] = 0;
            }
            else if (moveTo[0] - nowPosition[0] > 0)
            {
                moveVector[0] = 1;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                moveVector[0] = -1;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            


            // 目的座標まで動いたら
            //  Unitの現在座標と目的ブロックの座標を比較
            //  比較演算子の関係で移動方向を乗算
            if (gameObject.GetComponent<Transform>().position.x * moveVector[0]
                 >= GM.FieldBlocks[moveTo[0], nowPosition[1]].GetComponent<Transform>().position.x * moveVector[0])
            {

                moveVector[0] = 0;

                // ブロックの直上に調整
                gameObject.GetComponent<Transform>().position
                     = GM.FieldBlocks[moveTo[0], nowPosition[1]].GetComponent<Transform>().position;

                nowPosition[0] = moveTo[0];

                // 移動先BlockにUnit情報を設定
                GM.FieldBlocks[moveTo[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = gameObject;

                moveTo[0] = -1;

                GM.gameScene = GameMgr.SCENE.UNIT_MENU;
                GM.changeInfoWindow();
            }


        }

        // 移動中ではない
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isWalking", false);
           // gameObject.GetComponent<SpriteRenderer>().flipX = false;

            bool flipx = (camp == -1) ? true : false;
            gameObject.GetComponent<SpriteRenderer>().flipX = flipx;
        }

        //---- 移動処理（y軸移動->x軸移動）ここまで----//
    }



    public void changePosition(int x, int y, bool walkflg)
    {

        // 歩行の有無
        if (walkflg)
        {
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            
            // 移動先座標の指定
            moveTo[0] = x;
            moveTo[1] = y;
            // 残りの移動処理はUpdateにて //


        }
        // 歩行しない場合
        else
        {
            // 移動元BlockからUnit情報を削除
            GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = null;

            gameObject.GetComponent<Transform>().position
                 = GM.FieldBlocks[x, y].GetComponent<Transform>().position;

            nowPosition[0] = x;
            nowPosition[1] = y;

            // 移動先BlockにUnit情報を設定
            GM.FieldBlocks[x, y].GetComponent<FieldBlock>().GroundedUnit = gameObject;
        }

    }



    // 移動/攻撃範囲の表示
    public void viewArea()
    {
        int[] nowCursolPosition = { cursor.GetComponent<cursor>().nowPosition[0], cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = GM.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];

        int movable = unitInfo.movable[1];
        if (GM.gameScene == GameMgr.SCENE.UNIT_SELECT_TARGET) movable = 0;

        // 移動範囲を表示
        for (int y = -movable; y <= movable; y++)
            {
                for (int x = abs(y) - movable; x <= movable - abs(y); x++)
                {
                    // 中心以外かつマップエリア内
                    if (!(x == 0 && y == 0)
                        && (nowCursolPosition[0] + x >= 0 && nowCursolPosition[1] + y >= 0) &&
                        (nowCursolPosition[0] + x < GM.x_mass * 2 && nowCursolPosition[1] + y < GM.y_mass * 2))
                    {
                        Vector3 position = GM.FieldBlocks[nowCursolPosition[0] + x, nowCursolPosition[1] + y].GetComponent<Transform>().position;
                        movableAreaList.Add(Instantiate(movableArea, position, transform.rotation));
                    }
                }
            }

            // 攻撃範囲を表示
            int reach = unitInfo.reach[1];
            for (int y = -(movable + reach); y <= movable + reach; y++)
            {
                for (int x = abs(y) - (movable + reach); x <= (movable + reach) - abs(y); x++)
                {
                    // 移動範囲以外かつマップエリア内
                    if ((abs(x) + abs(y) > movable) &&
                        (nowCursolPosition[0] + x >= 0 && nowCursolPosition[1] + y >= 0) &&
                        (nowCursolPosition[0] + x < GM.x_mass * 2 && nowCursolPosition[1] + y < GM.y_mass * 2))
                    {
                        Vector3 position = GM.FieldBlocks[nowCursolPosition[0] + x, nowCursolPosition[1] + y].GetComponent<Transform>().position;
                        reachAreaList.Add(Instantiate(reachArea, position, transform.rotation));
                    }
                }
            }

            GM.selectedUnit = gameObject;
        
    }





    // Unitの移動範囲を決定
    public void selectMovableArea()
    {
        int[] nowCursolPosition = { cursor.GetComponent<cursor>().nowPosition[0], cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = GM.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];

        //TODO 移動可能範囲

        changePosition(nowCursolPosition[0], nowCursolPosition[1], true);
        deleteReachArea();
    }


    // 移動完了時の処理
    public void endUnitMoving()
    {
        GM.gameScene = GameMgr.SCENE.UNIT_MENU;
        GM.changeInfoWindow();
        GM.unitMenu.SetActive(true);
    }


    // 攻撃処理
    public virtual void targetAction(GameObject groundedUnit)
    {
        /*
        int[] nowCursolPosition = { cursor.GetComponent<cursor>().nowPosition[0], cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = GM.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];

        //TODO 攻撃可能範囲

        Vector2 targetPosition = GM.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<Transform>().position;

            int damage = GM.selectedUnit.GetComponent<Unit>().unitInfo.attack_phy[1]
            - groundedUnit.GetComponent<Unit>().unitInfo.guard_phy[1];
            groundedUnit.GetComponent<Unit>().unitInfo.hp[1] -= damage;
        
        Instantiate(GM.explosion, targetPosition, transform.rotation);
        deleteReachArea();

    */
    }



    // 攻撃範囲/移動範囲用のパネルオブジェクトを除去
    protected void deleteReachArea()
    {
        // 移動範囲オブジェクトを削除
        for (int i = 0; i < movableAreaList.Count; i++)
        {
            Destroy(movableAreaList[i]);
        }
        movableAreaList.Clear();

        // 攻撃範囲オブジェクトを削除
        for (int i = 0; i < reachAreaList.Count; i++)
        {
            Destroy(reachAreaList[i]);
        }
        reachAreaList.Clear();
    }



    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

}
