using UnityEngine;

public class CharacterMovementAdvancedView : MonoBehaviour
{
    public Animator Animator;

    private CharacterMovementAdvancedModel m_Model;

    private void Awake()
    {
        m_Model = GetComponent<CharacterMovementAdvancedModel>();
    }

    private void Update()
    {
        UpdatePushing();
    }

    private void UpdatePushing()
    {
        Animator.SetBool("IsPushing", m_Model.IsPushing()); //Set bool to true for anims
        Animator.SetBool("IsPushingAndWalking", m_Model.IsPushingAndWalking()); //Set bool to true for anims
    }
}