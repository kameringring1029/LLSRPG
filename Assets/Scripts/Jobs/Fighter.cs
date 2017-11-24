using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;

class Fighter:Unit
{
    public GameObject explosionPrefab;
    private GameObject targetunit;
        
    public override void targetAttack(GameObject targetUnit)
    {
        targetunit = targetUnit;

        int damage = unitInfo.attack_phy[1]
        - targetUnit.GetComponent<Unit>().unitInfo.guard_phy[1];
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);


        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);

        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
        Instantiate(explosionPrefab, targetUnit.transform.position, transform.rotation);

        deleteReachArea();
    }


    private void instantiateAttackEffect()
    {
        Instantiate(explosionPrefab, targetunit.transform.position, transform.rotation);
    }
}


