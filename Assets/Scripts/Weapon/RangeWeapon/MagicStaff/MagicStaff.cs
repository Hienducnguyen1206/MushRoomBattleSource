using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStaff : AbstractWeapon
{
    

    public override void Attack()
    {     

    }

    
    public override void FlipWhenNotHaveEnemyInRange()
    {
        transform.rotation = baseRotation;
        transform.localScale = new Vector3(normalScale.x, normalScale.y, normalScale.z);
    }
    

}
