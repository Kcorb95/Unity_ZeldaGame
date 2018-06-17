using UnityEngine;

public class CharacterKeyboardControl : CharacterBaseControl
{
    private void Start()
    {
        SetDirection(new Vector2(0, -1)); //Make sure we start facing the right way
    }

    private void Update()
    {
        UpdateDirection();
        UpdateAction();
        UpdateAttack();
        UpdateBomb();
    }

    //When hitting space, do attack
    private void UpdateAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnAttackPressed();
    }

    //When hitting the F key, drop a bomb
    private void UpdateBomb()
    {
        if (Input.GetKeyDown(KeyCode.F)) OnBombUsed();
    }

    //When hitting the E key, do an interaction
    private void UpdateAction()
    {
        if (Input.GetKeyDown(KeyCode.E)) OnActionPressed();
    }

    //Handle the characters movement direction based on the input key
    private void UpdateDirection()
    {
        Vector2 newDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) newDirection.y = 1;
        if (Input.GetKey(KeyCode.S)) newDirection.y = -1;
        if (Input.GetKey(KeyCode.A)) newDirection.x = -1;
        if (Input.GetKey(KeyCode.D)) newDirection.x = 1;

        SetDirection(newDirection);
    }
}