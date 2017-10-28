using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

public class Unit : MonoBehaviour {


    protected GameMgr GM;
    protected GameObject cursor;
    public GameObject shadePrefab;
    private GameObject unitshade;

    public UnitStatus unitInfo;
    public int camp;
    public bool isActioned;

    private int staticMoveVelocity = 3;

    private int[] moveVector = new int[2];
    private int[] moveTo = new int[2];

    public int[] nowPosition = new int[2];
    private int[] prePosition = new int[2];

    public GameObject movableAreaPrefab;
    private List<GameObject> movableAreaList = new List<GameObject>();
    public GameObject reachAreaPrefab;
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
        unitshade = Instantiate(shadePrefab, transform.position, transform.rotation);

        // 陣営設定とSprite反転
        camp = c;
        changeSpriteFlip(0);

        // 変数初期化
        for (int i=0; i < 2; i++)
        {
            nowPosition[i] = 0;
            prePosition[i] = 0;
            moveVector[i] = 0;
            moveTo[i] = -1;
        }

        // 初期位置へ移動、行動権取得
        changePosition(x, y, false);
        restoreActionRight();
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
            // 移動中(演出中)はユーザ操作不可
            GM.setInEffecting(true);


            // 移動方向の決定とUnitの向きの変更
            if (moveTo[1] - nowPosition[1] == 0)
            {
                moveVector[1] = 0;
            }
            else if (moveTo[1] - nowPosition[1] > 0)
            {
                moveVector[1] = 1;
                changeSpriteFlip(-1);
            }
            else
            {
                moveVector[1] = -1;
                changeSpriteFlip(1);
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

                // Spriteの表示Orderを更新
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<SpriteRenderer>().sortingOrder + 101;

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
                changeSpriteFlip(1);
            }
            else
            {
                moveVector[0] = -1;
                changeSpriteFlip(-1);
            }


            // 目的座標まで動いたら
            //  Unitの現在座標と目的ブロックの座標を比較
            //  比較演算子の関係で移動方向を乗算
            if (gameObject.GetComponent<Transform>().position.x * moveVector[0]
                 >= GM.FieldBlocks[moveTo[0], nowPosition[1]].GetComponent<Transform>().position.x * moveVector[0])
            {
                moveVector[0] = 0;

                nowPosition[0] = moveTo[0];
                moveTo[0] = -1;


                // X,Y軸移動が完了したので-----------

                // ブロックの直上に調整
                gameObject.GetComponent<Transform>().position
                     = GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<Transform>().position;
                // 移動先BlockにUnit情報を設定
                GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = gameObject;
                // Spriteの表示Orderを更新
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<SpriteRenderer>().sortingOrder +101;
                

                GM.setInEffecting(false);
                GM.endUnitMoving();

                changeSpriteFlip(0);
                gameObject.GetComponent<Animator>().SetBool("isWalking", false);
                
                // X,Y軸移動が完了したので------------
            }

        }

        // 移動中ではない
        else
        {
            // do notiong
        }
    }


    //---- 目的地への移動を設定する ----//
    // x,y: 目的座標
    // walkflg = 1の場合、移動先座標を指定（残りはUpdateとunitMoveで実施）
    //           0の場合、ワープ
    public void changePosition(int x, int y, bool walkflg)
    {
        // 移動キャンセル用に移動前の座標を格納
        prePosition[0] = nowPosition[0]; prePosition[1] = nowPosition[1];

        // 移動元BlockからUnit情報を削除
        GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = null;

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

            gameObject.GetComponent<Transform>().position
                 = GM.FieldBlocks[x, y].GetComponent<Transform>().position;

            nowPosition[0] = x;
            nowPosition[1] = y;

            // 移動先BlockにUnit情報を設定
            GM.FieldBlocks[x, y].GetComponent<FieldBlock>().GroundedUnit = gameObject;
        }

    }


    //--- 移動キャンセル処理 ---//
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
        
        changePosition(nowCursolPosition[0], nowCursolPosition[1], true);
        deleteReachArea();
    }



    //--- 指定地点にUnitが移動できるか判定 ---//
    //  return 移動可否
    //  potision; ワールド座標
    public bool canMove(Vector3 position)
    {
        for(int i=0; i<movableAreaList.Count; i++)
        {
            if(movableAreaList[i].transform.position == position)
            {
                return true;
            }
        }

        return false;
    }

    //--- 指定地点をアクション対象とできるか判定 ---//
    //  return アクション対象可否
    //  potision; ワールド座標
    public bool canReach(Vector3 position)
    {
        for (int i = 0; i < reachAreaList.Count; i++)
        {
            if (reachAreaList[i].transform.position == position)
            {
                return true;
            }
        }

        return false;
    }


    //--- 移動/対象範囲の表示 ---//
    public void viewMovableArea()
    {

        int movable = unitInfo.movable[1];
        if (GM.gameScene == GameMgr.SCENE.UNIT_SELECT_TARGET) movable = 0;

        // 移動範囲を表示
        for (int y = -movable; y <= movable; y++)
        {
            for (int x = abs(y) - movable; x <= movable - abs(y); x++)
            {
                // マップエリア内
                if ((nowPosition[0] + x >= 0 && nowPosition[1] + y >= 0) &&
                    (nowPosition[0] + x < GM.x_mass * 2 && nowPosition[1] + y < GM.y_mass * 2))
                {
                    // ユニットが乗っかっていない or ユニットが自分の場合範囲を表示
                    if(GM.FieldBlocks[nowPosition[0] + x, nowPosition[1] + y].GetComponent<FieldBlock>().GroundedUnit == null ||
                        GM.FieldBlocks[nowPosition[0] + x, nowPosition[1] + y].GetComponent<FieldBlock>().GroundedUnit == gameObject)
                    {
                        Vector3 position = GM.FieldBlocks[nowPosition[0] + x, nowPosition[1] + y].GetComponent<Transform>().position;
                        movableAreaList.Add(Instantiate(movableAreaPrefab, position, transform.rotation));
                    }
                }
            }
        }

        GM.selectedUnit = gameObject;
        
    }


    public void viewTargetArea()
    {
        // 攻撃範囲を表示
        int reach = unitInfo.reach[1];
        for (int y = -reach; y <= reach; y++)
        {
            for (int x = abs(y) -reach; x <= reach - abs(y); x++)
            {
                // マップエリア内
                if ((nowPosition[0] + x >= 0 && nowPosition[1] + y >= 0) &&
                    (nowPosition[0] + x < GM.x_mass * 2 && nowPosition[1] + y < GM.y_mass * 2))
                {
                    Vector3 position = GM.FieldBlocks[nowPosition[0] + x, nowPosition[1] + y].GetComponent<Transform>().position;
                    reachAreaList.Add(Instantiate(reachAreaPrefab, position, transform.rotation));
                }
            }
        }
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


    //--- ダメージを受けた時の処理 ---//
    // damageval: ダメージ量
    // dealFrom: ダメージ源
    public void beDamaged(int damageval, GameObject dealFrom)
    {
        if(damageval > 0)
            unitInfo.hp[1] -= damageval;

        StartCoroutine("damageCoroutine", dealFrom);

    }


    //--- ダメージ処理用コルーチン ---//
    // dealFrom: ダメージ源
    IEnumerator damageCoroutine(GameObject dealFrom)
    {
        GM.setInEffecting(true);

        // Spriteの明滅演出
        for(int i = 0; i < 3; i++)
        {
            changeSpriteColor(255, 255, 255, 100f);
            yield return new WaitForSeconds(0.25f);
            changeSpriteColor(255, 255, 255, 255);
            yield return new WaitForSeconds(0.25f);
        }

        // 消滅処理
        if(unitInfo.hp[1] <= 0)
        {
            // Spriteを徐々に薄く
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(0.1f);
                changeSpriteColor(255, 255, 255, (float)(250 - i * 250 / 10));
            }

            // BlockからUnit情報を削除
            GM.FieldBlocks[nowPosition[0], nowPosition[1]].GetComponent<FieldBlock>().GroundedUnit = null;

            // Managerのリストから削除
            GM.removeUnitByList(gameObject);

            Destroy(unitshade);
            Destroy(gameObject);
        }

        GM.setInEffecting(false);

        
        // ダメージ源がUnitの場合
        if(dealFrom.GetComponent<Unit>()!=null)
            dealFrom.GetComponent<Unit>().endAction();
    }



    //--- Job固有の対象指定アクション ---//
    // targetUnit: アクションの対象となるUnit
    public virtual void targetAction(GameObject targetUnit)
    {
        // virtual
    }



    //--- 行動終了処理 ---//
    public void endAction()
    {
        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);

        isActioned = true;
        changeSpriteColor(180f,180f,180f,255f);
        changeSpriteFlip(0);
        GM.endUnitActioning();
    }


    //--- 行動権取得処理 ---//
    public void restoreActionRight()
    {
        isActioned = false;
        changeSpriteFlip(0);
        changeSpriteColor(255f, 255f, 255f, 255f);
    }


    //--- Spriteの反転処理 ---//
    //  1;右向き
    // -1;左向き
    //  0;ニュートラル（陣営による）
    public void changeSpriteFlip(int vector)
    {
        bool flipx = (camp == -1) ? true : false;
        switch (vector)
        {
            case 1:
                flipx = true;
                break;
            case -1:
                flipx = false;
                break;
            case 0:
                flipx = (camp == -1) ? true : false;
                break;
        }

        gameObject.GetComponent<SpriteRenderer>().flipX = flipx;
        
    }


    //--- Spriteの色の変更 ---//
    private void changeSpriteColor(float r, float g, float b, float a)
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    //--- 絶対値取得 ---//
    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

}
