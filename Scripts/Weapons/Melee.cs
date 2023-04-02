using UnityEngine;

public class Melee : Weapon
{   
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().isTrigger = true;       
    }
    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
