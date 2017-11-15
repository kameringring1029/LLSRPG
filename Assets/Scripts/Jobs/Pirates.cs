using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirates : Unit {

    public GameObject explosionPrefab;
    private Vector2 targetPosition;

    public override void targetAttack(GameObject targetUnit)
    {
        int[] nowCursolPosition = { GM.cursor.GetComponent<cursor>().nowPosition[0], GM.cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];

        targetPosition = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<Transform>().position;

        int damage = unitInfo.attack_phy[1]
        - targetUnit.GetComponent<Unit>().unitInfo.guard_phy[1];
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);


        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);

        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);

        deleteReachArea();
    }


    private void instantiateAttackEffect()
    {
        Instantiate(explosionPrefab, targetPosition, transform.rotation);
    }
}
