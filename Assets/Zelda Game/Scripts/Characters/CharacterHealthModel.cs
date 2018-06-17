using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealthModel : MonoBehaviour
{
    public float StartingHealth; //Our starting health value (100)

    private float m_MaximumHealth; //Our Maximum health value (100)
    private float m_Health; //Our current health value

    private List<ArmorModel> m_ArmorModels = new List<ArmorModel>(); //Initiate our ArmorModel

    private void Start()
    {
        m_Health = StartingHealth; //Set out values
        m_MaximumHealth = StartingHealth; //Set out values
    }

    //Returns the current health of the user
    public float GetHealth()
    {
        return m_Health;
    }

    //Add more health to our current health
    public void AddHealth(float health)
    {
        if (m_Health == m_MaximumHealth) return; //If at max, do nothing
        if (m_Health + health > m_MaximumHealth) //If new health would be greater than 100, then just set to 100
        {
            m_Health = m_MaximumHealth;
            return;
        }
        m_Health += health; //Add health to current health
    }

    //Returns the max health a user can have
    public float GetMaximumHealth()
    {
        return m_MaximumHealth;
    }

    //Gets our current health as a percentage (90/100)
    public float GetHealthPercentage()
    {
        return m_Health / m_MaximumHealth;
    }

    //Gets our current total armor
    public float GetTotalArmor()
    {
        float totalArmor = 0;

        for (int i = 0; i < m_ArmorModels.Count; ++i)
        {
            totalArmor = m_ArmorModels[i].GetArmor();
        }

        return totalArmor;
    }

    //Gets the total max armor we can have
    public float GetTotalMaximumArmor()
    {
        float totalArmor = 0;

        for (int i = 0; i < m_ArmorModels.Count; ++i)
        {
            totalArmor = m_ArmorModels[i].GetMaximumArmor();
        }

        return totalArmor;
    }

    //Gets our armor as a percentage (40/50)
    public float GetTotalArmorPercentage()
    {
        return GetTotalArmor() / GetTotalMaximumArmor();
    }

    //Deals damage to our health
    public void DealDamage(float damage)
    {
        if (m_Health <= 0 || m_Health - damage <= 0) SceneManager.LoadScene("Game"); //If our health equals zero, reload game

        UIDamageNumbers.Instance.ShowDamageNumber(damage, transform.position); //Show the damage effect

        //Calculate the damage done
        float healthDamage = damage;
        float damageAbsorbedByArmor = 0;
        float totalDamageToAbsorb = damage * 0.5f;

        m_ArmorModels.Sort(delegate (ArmorModel armor1, ArmorModel armor2)
       {
           return armor1.GetArmor().CompareTo(armor2.GetArmor());
       });

        for (int i = 0; i < m_ArmorModels.Count; ++i)
        {
            float damageToAbsorb = totalDamageToAbsorb / m_ArmorModels.Count;

            if (damageToAbsorb > m_ArmorModels[i].GetArmor())
            {
                damageToAbsorb = m_ArmorModels[i].GetArmor();
            }

            m_ArmorModels[i].DealDamage(damageToAbsorb);

            damageAbsorbedByArmor += damageToAbsorb;
        }

        healthDamage -= damageAbsorbedByArmor;
        m_Health -= healthDamage;

        if (m_Health <= 0) //If our health equals zero, reload game
        {
            m_Health = 0;
            SceneManager.LoadScene("Game");
        }
    }

    //Add armor to player
    public void RegisterArmor(ArmorModel armor)
    {
        m_ArmorModels.Add(armor);
    }

    //Remove armor from player
    public void UnregisterArmor(ArmorModel armor)
    {
        m_ArmorModels.Remove(armor);
    }
}