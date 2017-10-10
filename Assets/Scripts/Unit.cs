using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

public class Unit : MonoBehaviour {

    private int[] nowPosition = new int[2];
    private GameMgr GM;
    private int camp;

    public int staticMoveVelocity = 3;

    public int[] moveVector = new int[2];
    private int[] moveTo = new int[2];

    public Kanan_TT unitInfo;


	// Use this for initialization
	void Start () {
        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();
        
    }

    public void init(int x, int y, int c)
    {
        GM = GameObject.Find("Main Camera").GetComponent<GameMgr>();

        unitInfo = new Kanan_TT();
        

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

                GM.endUnitMoving();
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

    
}
