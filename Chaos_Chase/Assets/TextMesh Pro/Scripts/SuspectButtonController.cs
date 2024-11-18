using UnityEngine;
using UnityEngine.UI;

public class SuspectButtonController : MonoBehaviour
{
    public Button[] suspectButtons;

    public void OnSuspectButtonClick(Button clickedButton)
    {
        clickedButton.interactable = false;

        ColorBlock colors = clickedButton.colors;
        colors.normalColor = Color.gray;
        colors.highlightedColor = Color.gray;
        colors.pressedColor = Color.gray;
        colors.selectedColor = Color.gray;
        clickedButton.colors = colors;

    }
}

