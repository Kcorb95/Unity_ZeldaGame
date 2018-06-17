using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public CharacterHealthModel HealthModel;
    public Text HealthText;
    public RectTransform HealthBar;

    private void Update()
    {
        UpdateText();
        UpdateHealthBar();
    }

    private void UpdateText() //Sets the text value of the healthbar as a ratio
    {
        HealthText.text = Mathf.RoundToInt(HealthModel.GetHealth()) + "/" +
            Mathf.RoundToInt(HealthModel.GetMaximumHealth());
    }

    private void UpdateHealthBar() //Sets the health bar fill effect
    {
        HealthBar.localScale = new Vector3(HealthModel.GetHealthPercentage(), 1f, 1f);
    }
}