using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameUIController UIController;
    private void Start()
    {
        WaveManager.Instance.WaveRunning += ShopOpening;
    }
    private void OnDisable()
    {
        WaveManager.Instance.WaveRunning -= ShopOpening;
    }
    public void ShopOpening(bool WaveRunning)
    {
        gameObject.SetActive(!WaveRunning);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIController.ShopOpen();
        }
    }
}
