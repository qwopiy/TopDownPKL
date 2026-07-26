using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameUIController UIController;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIController.ShopOpen();
        }
    }
}
