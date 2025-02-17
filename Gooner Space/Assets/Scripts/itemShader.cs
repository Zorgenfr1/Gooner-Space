using UnityEngine;

public class itemShader : MonoBehaviour
{
    public Material itemThing;
    private static readonly int DeexID = Shader.PropertyToID("_deex");
    private bool isMouseOver = false;

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (!isMouseOver)
            {
                isMouseOver = true;
                itemThing.SetInt(DeexID, 1);
            }
        }
        else
        {
            if (isMouseOver)
            {
                isMouseOver = false;
                itemThing.SetInt(DeexID, 0);
            }
        }
    }
}
