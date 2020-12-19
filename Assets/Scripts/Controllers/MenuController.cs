using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuCanvas = null;
    [SerializeField]
    private GameObject instructionsCanvas = null;
    [SerializeField]
    private GameObject creditCanvas = null;
    [SerializeField]
    private GameObject optionsCanvas = null;

    public void ShowMenu(string name)
    {
        switch(name)
        {
            case "Menu":
                SetMenusActive(true, false, false, false);
                break;
            case "Instructions":
                SetMenusActive(false, true, false, false);
                break;
            case "Credits":
                SetMenusActive(false, false, true, false);
                break;
            case "Options":
                SetMenusActive(false, false, false, true);
                break;
            default:
                break;
        }        
    }

    private void SetMenusActive(bool menu, bool instructions, bool credits, bool options)
    {
        menuCanvas.SetActive(menu);
        instructionsCanvas.SetActive(instructions);
        creditCanvas.SetActive(credits);
        optionsCanvas.SetActive(options);
    }
}
