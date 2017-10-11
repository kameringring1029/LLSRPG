using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Fighter:Unit
{
    public override void targetAction(GameObject groundedUnit)
    {
        int[] nowCursolPosition = { cursor.GetComponent<cursor>().nowPosition[0], cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = GM.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];

        //TODO 攻撃可能範囲

        Vector2 targetPosition = GM.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<Transform>().position;

        int damage = GM.selectedUnit.GetComponent<Unit>().unitInfo.attack_phy[1]
        - groundedUnit.GetComponent<Unit>().unitInfo.guard_phy[1];
        groundedUnit.GetComponent<Unit>().unitInfo.hp[1] -= damage;

        Instantiate(GM.explosion, targetPosition, transform.rotation);
        deleteReachArea();
    }
}


