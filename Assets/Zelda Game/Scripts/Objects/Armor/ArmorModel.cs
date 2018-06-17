using UnityEngine;

//Simpler version of HealthModel, basically all the same applies
public class ArmorModel : MonoBehaviour
{
    public float StartingArmor;

    private float m_MaximumArmor;
    private float m_Armor;

    private void Start()
    {
        m_Armor = StartingArmor;
        m_MaximumArmor = StartingArmor;
    }

    public float GetArmor()
    {
        return m_Armor;
    }

    public float GetMaximumArmor()
    {
        return m_MaximumArmor;
    }

    public void DealDamage(float damage)
    {
        if (m_Armor <= 0)
        {
            return;
        }

        m_Armor -= damage;

        if (m_Armor <= 0)
        {
            CharacterHealthModel healthModel = GetComponentInParent<CharacterHealthModel>();

            if (healthModel != null)
            {
                healthModel.UnregisterArmor(this);
            }

            Destroy(gameObject);
        }
    }
}