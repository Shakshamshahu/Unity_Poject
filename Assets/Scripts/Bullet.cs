using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(Deactivate), 5f);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
