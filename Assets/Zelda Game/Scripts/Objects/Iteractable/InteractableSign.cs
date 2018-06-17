public class InteractableSign : InteractableBase
{
    public string Text;

    public override void OnInteract(Character character)
    {
        if (DialogBox.IsVisible() == true) //If it is visible
        {
            character.Movement.SetFrozen(false, false, true); //allow player to move again
            DialogBox.Hide(); //hide the ui
        }
        else
        {
            character.Movement.SetFrozen(true, true, true); //do not allow players to move
            DialogBox.Show(Text); //show the ui
        }
    }
}