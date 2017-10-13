using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sage : Unit {

    public GameObject explosionPrefab;

    public override void targetAction(GameObject targetUnit)
    {
        int[] nowCursolPosition = { cursor.GetComponent<cursor>().nowPosition[0], cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = GM.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];

        //TODO 攻撃可能範囲

        Vector2 targetPosition = GM.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<Transform>().position;

        int damage = unitInfo.attack_magic[1]
        - targetUnit.GetComponent<Unit>().unitInfo.guard_magic[1];
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);

        Instantiate(explosionPrefab, targetPosition, transform.rotation);

        deleteReachArea();
    }
}
