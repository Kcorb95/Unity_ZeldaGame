using UnityEngine;
using UnityEngine.UI;

//The dialog box used throughout the game
public class DialogBox : MonoBehaviour
{
    private static DialogBox Instance;

    private Image m_DialogFrame;
    private Text m_Text;

    private void Awake()
    {
        Instance = this;

        m_DialogFrame = GetComponent<Image>();
        m_Text = GetComponentInChildren<Text>();
    }

    //Show dialog with included text
    public static void Show(string displayText)
    {
        Instance.DoShow(displayText);
    }

    //Toggle visibility of the dialog
    public static void Hide()
    {
        Instance.DoHide();
    }

    //if it is visible, show the frame
    public static bool IsVisible()
    {
        return Instance.m_DialogFrame.enabled;
    }

    //Actually hide the UI
    private void DoHide()
    {
        m_DialogFrame.enabled = false; //disable frame
        m_Text.enabled = false; //disable text
    }

    //Actually show the UI
    private void DoShow(string displayText)
    {
        m_DialogFrame.enabled = true; //Frame is to be enabled

        m_Text.enabled = true; //As is the text
        m_Text.text = displayText; //Display the desired text
    }
}