﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharaterStats
{
    public override void Die(){
        base.Die();

        Destroy(gameObject);
    }

}
