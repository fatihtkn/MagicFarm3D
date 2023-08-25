

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonManager :MonoSingleton<ButtonManager>    
{
    public GameObject wandButton;
    public GameObject battleButton;
    public GameObject flameButton;
    
    public void AddListener(ButtonTypes buttonType,UnityAction action)
    {
        Button button = GetButton(buttonType);
        button.onClick.AddListener(action);
    }
    public void RemoveListener(ButtonTypes buttonType, UnityAction action)
    {
        Button button =GetButton(buttonType);
        button.onClick.RemoveListener(action);
    }

   
    private Button GetButton(ButtonTypes buttonType)
    {
        Button button = null;
        switch (buttonType)
        {
            case ButtonTypes.Battle:
                button = battleButton.GetComponent<Button>();
                break;
            case ButtonTypes.Wand:
                button=wandButton.GetComponent<Button>();
                break;
            default:
                break;
        }
        return button;

    }
        

}
