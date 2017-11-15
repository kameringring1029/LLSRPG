using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Unit {


    public GameObject explosionPrefab;

    public override void targetAttack(GameObject targetUnit)
    {
        int[] nowCursolPosition = { GM.cursor.GetComponent<cursor>().nowPosition[0], GM.cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];
        
        Vector2 targetPosition = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<Transform>().position;

        int damage = unitInfo.attack_magic[1]
        - targetUnit.GetComponent<Unit>().unitInfo.guard_magic[1];
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);

        Instantiate(explosionPrefab, targetPosition, transform.rotation);

        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);

        deleteReachArea();
    }

    public override void targetHeal(GameObject targetUnit)
    {
        int[] nowCursolPosition = { GM.cursor.GetComponent<cursor>().nowPosition[0], GM.cursor.GetComponent<cursor>().nowPosition[1] };
        GameObject nowBlock = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]];

        Vector2 targetPosition = map.FieldBlocks[nowCursolPosition[0], nowCursolPosition[1]].GetComponent<Transform>().position;

        int heal = unitInfo.attack_magic[1]/2;
        targetUnit.GetComponent<Unit>().beHealed(heal, gameObject);

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);

        Instantiate(explosionPrefab, targetPosition, transform.rotation);


        gameObject.GetComponent<Animator>().SetBool("isHealing", true);

        deleteReachArea();
    }

    public override List<ACTION> getActionableList()
    {
        List<ACTION> actionlist = new List<ACTION>();
        actionlist.Add(ACTION.ATTACK);
        actionlist.Add(ACTION.HEAL);
        actionlist.Add(ACTION.WAIT);

        return actionlist;
    }
}
