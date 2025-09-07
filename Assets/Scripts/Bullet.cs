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

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision != null && collision.gameObject.CompareTag("Player")) 
        { 
            collision.gameObject.GetComponent<PlayerController>().
        }*/
    }
}
