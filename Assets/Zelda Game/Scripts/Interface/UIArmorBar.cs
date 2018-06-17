using UnityEngine;
using UnityEngine.UI;

public class UIArmorBar : MonoBehaviour
{
    public CharacterHealthModel HealthModel;
    public Text ArmorText;
    public RectTransform ArmorBar;
    public Image[] Images;

    private void Update()
    {
        if (HealthModel.GetTotalMaximumArmor() == 0) //If no armor at all on player, never picked up, then hide this part of the UI
        {
            SetImagesVisible(false);
            ArmorText.text = "";
            //Hide
        }
        else
        {
            SetImagesVisible(true);
            UpdateText();
            UpdateBar();
        }
    }

    private void UpdateText() //update the armor value with percentage when picking up new armor
    {
        ArmorText.text = Mathf.RoundToInt(HealthModel.GetTotalArmor()) + "/" +
            Mathf.RoundToInt(HealthModel.GetTotalMaximumArmor());
    }

    private void UpdateBar() //Update the visual fill effect of the armor bar
    {
        ArmorBar.localScale = new Vector3(HealthModel.GetTotalArmorPercentage(), 1f, 1f);
    }

    private void SetImagesVisible(bool visible)
    {
        foreach (Image image in Images)
        {
            image.enabled = visible;
        }
    }
}