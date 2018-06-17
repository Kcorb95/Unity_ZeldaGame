using UnityEngine;

public class DropItemOnDestroy : MonoBehaviour
{
    public ItemType DropItemType;
    public int Amount;
    public Transform DropPosition;

    [Range(0f, 1f)]
    public float Probability; //From 0 for no drop to 1 for guaranteed drop

    private void OnLootDrop()
    {
        float randomValue = Random.Range(0f, 1f); //Our chance

        if (randomValue > Probability) return;

        ItemData data = Database.Item.FindItem(DropItemType);

        if (data == null || data.Prefab == null)
        {
            return;
        }

        Transform dropPosition = DropPosition;

        if (dropPosition == null)
        {
            dropPosition = transform;
        }

        Instantiate(data.Prefab, dropPosition.position, Quaternion.identity);
    }
}