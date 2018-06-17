using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponCollider : MonoBehaviour
{
    public ItemType Type;

    private Collider2D m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        AttackableBase attackable = collider.gameObject.GetComponent<AttackableBase>();

        if (attackable != null) attackable.OnHit(m_Collider, Type);
    }
}