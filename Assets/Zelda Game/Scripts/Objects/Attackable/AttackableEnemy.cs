using System.Collections;
using UnityEngine;

public class AttackableEnemy : AttackableBase
{
    public float MaxHealth;
    public GameObject DestroyObjectOnDeath;
    public float DestroyDelayOnDeath;
    public CharacterMovementModel MovementModel;
    public float HitPushStrength;
    public float HitPushDuration;
    public GameObject DeathFX;
    public float DelayDeathFX;

    private float m_Health;

    private void Awake()
    {
        m_Health = MaxHealth; //Set the *Health of this enemy
    }

    public float GetHealth()
    {
        return m_Health; //Get the health of the enemy
    }

    public override void OnHit(Collider2D hitCollider, ItemType item) //When enemy is hit
    {
        float damage = 10; //Deal 10 base damage, define on weapon later

        m_Health -= damage; //remove health based on our damage
        UIDamageNumbers.Instance.ShowDamageNumber(damage, transform.position); //Show damage dealt

        if (MovementModel != null) //If movement is implemented
        {
            //push player and all that stuff
            Vector3 pushDirection = transform.position - hitCollider.gameObject.transform.position;
            pushDirection = pushDirection.normalized * HitPushStrength;

            MovementModel.PushCharacter(pushDirection, HitPushDuration);
        }

        if (m_Health <= 0) //If health is less than or equal to zero
        {
            StartCoroutine(DestroyRoutine(DestroyDelayOnDeath)); //Destroy the dude

            if (DeathFX != null) //If no dfx
            {
                StartCoroutine(CreateDeathFXRoutine(DelayDeathFX)); //make it
            }
        }
    }

    private IEnumerator DestroyRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        BroadcastMessage("OnLootDrop", SendMessageOptions.DontRequireReceiver);

        Destroy(DestroyObjectOnDeath);
    }

    private IEnumerator CreateDeathFXRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(DeathFX, transform.position, Quaternion.identity);
    }
}