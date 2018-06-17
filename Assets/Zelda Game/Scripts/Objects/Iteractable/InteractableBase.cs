using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    virtual public void OnInteract(Character character)
    {
        Debug.LogWarning("OnInteract is not implemented");
    }
}