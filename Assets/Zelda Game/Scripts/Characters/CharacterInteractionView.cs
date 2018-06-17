using UnityEngine;

public class CharacterInteractionView : MonoBehaviour
{
    public Animator Animator;

    private CharacterInteractionModel m_Model;

    private void Awake()
    {
        m_Model = GetComponent<CharacterInteractionModel>();
    }

    private void Update()
    {
        UpdateIsCarryingObject(); //Are we still carrying thing?
    }

    //Are we still carrying something?
    private void UpdateIsCarryingObject()
    {
        Animator.SetBool("IsCarrying", m_Model.IsCarryingObject());
    }
}