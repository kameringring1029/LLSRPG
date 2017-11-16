using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singer : Unit {

    public GameObject explosionPrefab;

    public override void targetAttack(GameObject targetUnit)
    {
        int[] nowCursolPosition = { GM.cursor.GetComponent<cursor>().nowPosition[0], GM.cursor.GetComponent<cursor>().nowPosition[1] };

        Vector2 targetPosition = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<Transform>().position;

        int damage = unitInfo.attack_phy[1]
        - targetUnit.GetComponent<Unit>().unitInfo.guard_phy[1];
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);

        Instantiate(explosionPrefab, targetPosition, transform.rotation);

        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);

        deleteReachArea();
    }


    public override void targetReaction(GameObject targetUnit)
    {
        int[] nowCursolPosition = { GM.cursor.GetComponent<cursor>().nowPosition[0], GM.cursor.GetComponent<cursor>().nowPosition[1] };

        Vector2 targetPosition = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<Transform>().position;

        int damage = 0;
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);
        targetUnit.GetComponent<Unit>().restoreActionRight();

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);

        Instantiate(explosionPrefab, targetPosition, transform.rotation);

        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);

        deleteReachArea();
    }


    public override List<ACTION> getActionableList()
    {
        List<ACTION> actionlist = new List<ACTION>();
        actionlist.Add(ACTION.ATTACK);
        actionlist.Add(ACTION.REACTION);
        actionlist.Add(ACTION.WAIT);

        return actionlist;
    }
}
