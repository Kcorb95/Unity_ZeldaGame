using UnityEngine;

public class BatProximityTrigger : MonoBehaviour
{
    private CharacterBatControl m_Control;

    private void Awake()
    {
        m_Control = GetComponentInParent<CharacterBatControl>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player") m_Control.SetCharacterInRange(collider.gameObject); //If the collider belongs to the player, get ready to chase
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player") m_Control.SetCharacterInRange(null); //Player left range, dont chase
    }
}