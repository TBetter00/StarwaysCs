using UnityEngine;

public class bulletC : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
