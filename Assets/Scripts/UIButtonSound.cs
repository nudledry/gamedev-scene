using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("ButtonHover");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (AudioManager.instance != null && button != null && button.interactable)
        {
            AudioManager.instance.Play("ButtonClick");
        }
    }

    public void PlayClickSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("ButtonClick");
        }
    }
}