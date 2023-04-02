using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{   
    private void Start()
    {
        GetComponent<Collider>().isTrigger= true;
        PlayAttackSound?.Invoke(_weaponSound);
    }
    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        Destroy(this.gameObject);
    } 
}
