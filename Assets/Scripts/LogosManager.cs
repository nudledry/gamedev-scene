using UnityEngine;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour 
{
    [SerializeField] private Image logoImage;
    [SerializeField] private Sprite customLogo;
    
    private void Start()
    {
        SetupLogo();
    }
    
    private void SetupLogo()
    {
        if (customLogo != null)
        {
            logoImage.sprite = customLogo;
            logoImage.preserveAspect = true;
        }
    }
}
