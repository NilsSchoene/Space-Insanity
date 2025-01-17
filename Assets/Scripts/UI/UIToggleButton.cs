using UnityEngine.UI;
using UnityEngine;

public class UIToggleButton : MonoBehaviour
{
    [SerializeField]
    private Sprite untoggledState;
    [SerializeField]
    private Sprite toggledState;
    [SerializeField]
    private Image buttonImage;
    private bool toggled = false;

    public void OnButtonClick()
    {
        toggled = !toggled;
        SwitchState(toggled);
    }

    private void SwitchState(bool toggle)
    {
        if(toggle)
        {
            buttonImage.sprite = toggledState;
        }
        else
        {
            buttonImage.sprite = untoggledState;
        }
    }
}
