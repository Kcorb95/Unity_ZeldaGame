using UnityEngine;

public class EnemyCharacterCollision : MonoBehaviour
{
    private CharacterBatControl m_Control;

    private void Awake()
    {
        m_Control = GetComponentInParent<CharacterBatControl>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_Control.OnHitCharacter(collider.gameObject.GetComponent<Character>());
        }
    }
}