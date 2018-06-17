using UnityEngine;
public class WeaponBomb : MonoBehaviour{    public GameObject ExplosionEffect;    private void Start()    {        Invoke("DoExplode", 3); //After 3 seconds, run method
    }    private void DoExplode() //Explodes the bomb
    {        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1); //Gets a list of all colliders within a certain range of our game object
        for (int i = 0; i < hitColliders.Length; i++) //loop through this list
        {
            //We want colliders which are attackable or destroyable
            AttackableBase attackable = hitColliders[i].gameObject.GetComponent<AttackableBase>();            Destroyable destroyable = hitColliders[i].gameObject.GetComponent<Destroyable>();
            //If any, do thing
            if (destroyable != null) Destroy(destroyable.gameObject); //Destroy it
            if (attackable != null) attackable.OnHit(hitColliders[i], ItemType.Bomb); //Attack it
        }        if (ExplosionEffect != null) //If no explosion effect
        {            GameObject explosionEffect = (GameObject)Instantiate(ExplosionEffect);//Do explosion
            explosionEffect.transform.position = transform.position;//At center of bomb
        }        Destroy(gameObject);//Destroy bomb
    }}