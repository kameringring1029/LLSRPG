using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;

public class Archer : Unit {

    public GameObject explosionPrefab;
    public GameObject slashPrefab;

    public override void targetAttack(GameObject targetUnit)
    {
        int damage = unitInfo.attack_phy[1]
        - targetUnit.GetComponent<Unit>().unitInfo.guard_phy[1];
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);


        // 対象への距離によって攻撃方法を変更A
        int distance = abs(targetUnit.GetComponent<Unit>().nowPosition[0] - gameObject.GetComponent<Unit>().nowPosition[0])
                    + abs(targetUnit.GetComponent<Unit>().nowPosition[1] - gameObject.GetComponent<Unit>().nowPosition[1]);
        if (distance > 1)
        {
            damage -= 2;
            Instantiate(explosionPrefab, targetUnit.transform.position, transform.rotation);
            gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
        }
        else
        {
            Instantiate(slashPrefab, targetUnit.transform.position, transform.rotation);
            gameObject.GetComponent<Animator>().SetBool("isAttacking2", true);
        }

        deleteReachArea();
    }
    

    public override List<ACTION> getActionableList()
    {
        List<ACTION> actionlist = new List<ACTION>();
        actionlist.Add(ACTION.ATTACK);
        actionlist.Add(ACTION.WAIT);

        return actionlist;
    }
}
