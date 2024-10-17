using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthPoint 
{  
  
    public void TakeDamage(int damage,bool critial,DamageType type,Vector3 hurtPosition);

    
}
