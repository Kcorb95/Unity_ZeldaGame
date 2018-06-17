﻿using UnityEngine;
public class WeaponBomb : MonoBehaviour
    }
    {
        for (int i = 0; i < hitColliders.Length; i++) //loop through this list
        {
            //We want colliders which are attackable or destroyable
            AttackableBase attackable = hitColliders[i].gameObject.GetComponent<AttackableBase>();
            //If any, do thing
            if (destroyable != null) Destroy(destroyable.gameObject); //Destroy it
            if (attackable != null) attackable.OnHit(hitColliders[i], ItemType.Bomb); //Attack it
        }
        {
            explosionEffect.transform.position = transform.position;//At center of bomb
        }
    }