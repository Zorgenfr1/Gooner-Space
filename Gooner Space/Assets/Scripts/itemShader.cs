using UnityEngine;
using UnityEngine.EventSystems;  

public class ItemShader : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Material itemThing;
    private static readonly int DeexID = Shader.PropertyToID("_deex");

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemThing.SetInt(DeexID, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemThing.SetInt(DeexID, 0);
    }
}

