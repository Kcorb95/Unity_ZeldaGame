using UnityEngine;

public class AttackableBush : AttackableBase
{
    public GameObject DestroyedPrefab; //What destroyed bush
    public GameObject DestroyEffect; //What effect for the destroy action

    public void CreateDestroyedPrefab()
    {
        GameObject newDestroyedBushObject = (GameObject)Instantiate(DestroyedPrefab, transform.position, transform.rotation); //Instantiate the prefab for the destroyed bush
        newDestroyedBushObject.transform.parent = transform.parent; //Update the parent etc
    }

    public void DestroyBush() //Destroy the bush
    {
        Destroy(gameObject); //Get rid of the game object

        if (DestroyEffect != null) //If no destroy effect
        {
            GameObject destroyEffect = (GameObject)Instantiate(DestroyEffect); //Instantiate it
            destroyEffect.transform.position = transform.position; //At the position of the bush
        }
    }

    public void DropLoot() //If loot dropped, drop some
    {
        BroadcastMessage("OnLootDrop", SendMessageOptions.DontRequireReceiver);
    }

    //When hit, destroy it, create the new prefab and drop loot
    public override void OnHit(Collider2D hitCollider, ItemType item)
    {
        DestroyBush();
        CreateDestroyedPrefab();
        DropLoot();
    }

    //When it is picked up, destroy the bush and drop loot
    private void OnPickupObject(Character character)
    {
        CreateDestroyedPrefab();
        DropLoot();
    }

    //When it is thrown, destroy the bush
    private void OnObjectThrown()
    {
        DestroyBush();
    }
}