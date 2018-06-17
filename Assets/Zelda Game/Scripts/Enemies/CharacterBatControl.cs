using UnityEngine;

public class CharacterBatControl : CharacterBaseControl
{
    public float PushStrength;
    public float PushTime;
    public AttackableEnemy AttackableEnemy;
    public float AttackDamage;

    private GameObject m_CharacterInRange;

    private void Update()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        Vector2 direction = Vector2.zero;

        if (m_CharacterInRange != null)
        {
            direction = m_CharacterInRange.transform.position - transform.position;
            direction.Normalize();
        }

        if (AttackableEnemy != null && AttackableEnemy.GetHealth() <= 0)
        {
            direction = Vector2.zero;
        }

        SetDirection(direction);
    }

    public void SetCharacterInRange(GameObject characterInRange) //set the character to being in range of the bat
    {
        m_CharacterInRange = characterInRange;
    }

    //Player was hit, give player chance to escape as well as moving them and damaging them
    public void OnHitCharacter(Character character)
    {
        Vector2 direction = character.transform.position - transform.position;
        direction.Normalize();

        m_CharacterInRange = null; //stop chasing
        character.Movement.PushCharacter(direction * PushStrength, PushTime); //push them

        character.Health.DealDamage(AttackDamage); //deal damage
    }
}