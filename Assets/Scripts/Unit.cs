using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

public class Unit : MonoBehaviour {

    private int[] nowPosition = new int[2];
    private int[] prePosition = new int[2];

    protected GameMgr GM;
    protected GameObject cursor;
    public int camp;

    public GameObject shade;

    public int staticMoveVelocity = 3;

    public int[] moveVector = new int[2];
    private int[] moveTo = new int[2];

    public UnitStatus unitInfo;
    private GameObject unitshade;

    public GameObject movableArea;
    private List<GameObject> movableAreaList = new List<GameObject>();
    public GameObject reachArea;
    private List<GameObject> reachAreaList = new List<GameObject>();


    // Use this for initialization
    void Start () {

        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();
        cursor = GameObject.Find("cursor");
    }


    //--- 初期設定 ---//
    // x,y:初期位置  c:陣営(1=味方 ,-1=敵)
    public void init(int x, int y, int c, statusTable statustable)
    {
        cursor = GameObject.Find("cursor");
        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();

        // Status設定
        unitInfo = new UnitStatus(statustable);

        // Resources/UnitAnimフォルダからグラをロード
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("UnitAnim/" + unitInfo.graphic_id);

        // 影の生成
        unitshade = Instantiate(shade, transform.position, transform.rotation);

        // 陣営設定とSprite反転
        camp = c;
        if (camp == -1) gameObject.GetComponent<SpriteRenderer>().flipX = true;

        // 変数初期化
        for (int i=0; i < 2; i++)
        {
            nowPosition[i] = 0;
            prePosition[i] = 0;
            moveVector[i] = 0;
            moveTo[i] = -1;
        }

        // 初期位置へ移動
        changePosition(x, y, false);
    }


    // Update is called once per frame
    void Update ()
    {
        // ユニットの速度設定（通常時は0）
        gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2((moveVector[0] - moveVector[1]) * staticMoveVelocity, -(moveVector[0] / 2.0f + moveVector[1] / 2.0f) * staticMoveVelocity);

        // 影の追従
        unitshade.transform.position = gameObject.transform.position;

        unitMove();
    }


    //---- 移動処理（y軸移動->x軸移動）----//
    //  移動ベクトルの決定、目的地への移動、表示系の変更等
    private void unitMove()
    {

        // y軸移動
        if (moveTo[1] != -1)
        {
            // 移動中は操作固定
            GM.gameScene = GameMgr.SCENE.UNIT_ACTIONING;
            

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
                nowPosition[1] = moveTo[1];

                moveTo[1] = -1;
                
            }

        }

        // x軸移動
        else if (moveTo[0] != -1)
        {

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

                // X,Y軸移動が完了したので
                // ブロックの直上に調整
                gameObject.GetComponent<Transform>().position
                     = GM.FieldBlocks[moveTo[0], nowPosition[1]].GetComponent<Transform>().position;
                // 移動元BlockからUnit情報を削除
                GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = null;
                // 移動先BlockにUnit情報を設定
                GM.FieldBlocks[moveTo[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = gameObject;


                moveVector[0] = 0;

                nowPosition[0] = moveTo[0];
                moveTo[0] = -1;
                

                GM.endUnitMoving();
            }


        }

        // 移動中ではない
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isWalking", false);

            bool flipx = (camp == -1) ? true : false;
            gameObject.GetComponent<SpriteRenderer>().flipX = flipx;
        }
    }


    //---- 目的地への移動を設定する ----//
    // walkflg = 1の場合、移動先座標を指定（残りはUpdateとunitMoveで実施）
    //           0の場合、ワープ
    public void changePosition(int x, int y, bool walkflg)
    {
        prePosition[0] = nowPosition[0]; prePosition[1] = nowPosition[1];

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

    //--- Unitの移動先を決定した後の処理 ---//
    public void returnPrePosition()
    {
        // 移動元BlockからUnit情報を削除
        GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = null;

        gameObject.GetComponent<Transform>().position
             = GM.FieldBlocks[prePosition[0], prePosition[1]].GetComponent<Transform>().position;

        nowPosition[0] = prePosition[0];  nowPosition[1] = prePosition[1];

        // 移動先BlockにUnit情報を設定
        GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = gameObject;

    }


    //--- Unitの移動先を決定した後の処理 ---//
    public void selectMovableArea()
    {
        int[] nowCursolPosition = { cursor.GetComponent<cursor>().nowPosition[0], cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = GM.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];

        //TODO 移動可能範囲

        changePosition(nowCursolPosition[0], nowCursolPosition[1], true);
        deleteReachArea();
    }


    //--- 移動/対象範囲の表示 ---//
    public void viewArea()
    {

        int movable = unitInfo.movable[1];
        if (GM.gameScene == GameMgr.SCENE.UNIT_SELECT_TARGET) movable = 0;

        // 移動範囲を表示
        for (int y = -movable; y <= movable; y++)
        {
            for (int x = abs(y) - movable; x <= movable - abs(y); x++)
            {
                // 中心以外かつマップエリア内
                if (!(x == 0 && y == 0)
                    && (nowPosition[0] + x >= 0 && nowPosition[1] + y >= 0) &&
                    (nowPosition[0] + x < GM.x_mass * 2 && nowPosition[1] + y < GM.y_mass * 2))
                {
                    Vector3 position = GM.FieldBlocks[nowPosition[0] + x, nowPosition[1] + y].GetComponent<Transform>().position;
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
                    (nowPosition[0] + x >= 0 && nowPosition[1] + y >= 0) &&
                    (nowPosition[0] + x < GM.x_mass * 2 && nowPosition[1] + y < GM.y_mass * 2))
                {
                    Vector3 position = GM.FieldBlocks[nowPosition[0] + x, nowPosition[1] + y].GetComponent<Transform>().position;
                    reachAreaList.Add(Instantiate(reachArea, position, transform.rotation));
                }
            }
        }

        GM.selectedUnit = gameObject;
        
    }

    //--- 対象アクション範囲/移動範囲用のパネルオブジェクトを除去 ---//
    public void deleteReachArea()
    {
        // 移動範囲オブジェクトを削除
        for (int i = 0; i < movableAreaList.Count; i++)
        {
            Destroy(movableAreaList[i]);
        }
        movableAreaList.Clear();

        // 対象アクション範囲オブジェクトを削除
        for (int i = 0; i < reachAreaList.Count; i++)
        {
            Destroy(reachAreaList[i]);
        }
        reachAreaList.Clear();
    }


    public void beDamaged(int damageval)
    {
        unitInfo.hp[1] -= damageval;

        // 消滅処理
        if(unitInfo.hp[1] <= 0)
        {   
            // BlockからUnit情報を削除
            GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = null;

            Destroy(gameObject);
            Destroy(unitshade);
        }
    }


    //--- Job固有の対象指定アクション ---//
    public virtual void targetAction(GameObject groundedUnit)
    {
        // virtual
    }


    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

}
