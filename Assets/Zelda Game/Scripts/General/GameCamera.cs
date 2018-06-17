using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public static GameCamera Instance;

    public RoomParent CurrentRoom;
    public float MovementSpeedOnRoomChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdatePosition();
    }

    //Update the camera every frame to the new room position
    private void UpdatePosition()
    {
        if (CurrentRoom == null)
        {
            return;
        }

        Vector3 targetPosition = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * MovementSpeedOnRoomChange);
    }

    //Target is rooms, used to position cam over room
    private Vector3 GetCameraTargetPosition()
    {
        if (CurrentRoom == null) return Vector3.zero;

        Vector3 targetPosition = CurrentRoom.GetRoomCenter();
        targetPosition.z = transform.position.z;

        return targetPosition;
    }

    //If we are currently switching rooms, used to reposition camera
    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}