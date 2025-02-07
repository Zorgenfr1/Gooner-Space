using UnityEngine;

public class sellMineral : MonoBehaviour
{
    public void SellIron()
    {
        GameManager.instance.SellMineral("Iron", 1); 
    }

    public void SellGold()
    {
        GameManager.instance.SellMineral("Gold", 1); 
    }
}
