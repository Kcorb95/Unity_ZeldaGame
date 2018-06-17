using UnityEngine;

public class RoomDoorBlockingObject : MonoBehaviour
{
    public float DestroyProbability; //Will this door actually be there on instantiation

    private void Start()
    {
        RoomParent roomParent = GetComponentInParent<RoomParent>();

        float randomValue = Random.Range(0f, 1f);

        if (randomValue < DestroyProbability ||
            (roomParent.X == 0 && roomParent.Y == 0) ||
            (roomParent.X == 0 && roomParent.Y == 1) ||
            (roomParent.X == 0 && roomParent.Y == -1) ||
            (roomParent.X == 1 && roomParent.Y == -1) ||
            (roomParent.X == 1 && roomParent.Y == -2))
        {
            Destroy(gameObject);
        }
    }
}