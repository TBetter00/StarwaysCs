using UnityEngine;

public class enemyC : MonoBehaviour
{
    public EnemyManagerC manager;

    private void OnDestroy()
    {
        if (manager != null)
        {
            manager.EnemyDied();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
