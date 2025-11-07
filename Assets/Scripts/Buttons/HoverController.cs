using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class HoverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cursorImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cursorImage != null)
        {
            cursorImage.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cursorImage != null)
        {
            cursorImage.SetActive(false);
        }
    }
}