using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float bulletSpeed = 30f;
    public float fireRate = 0.1f; // Smaller number = faster firing

    [Header("References")]
    public GameObject bullet;
    public GameObject shootPos;

    private float nextFireTime = 0f;

    void Update()
    {
        // Use GetKey to detect if the button is being held down
        if (Input.GetKey(KeyCode.F))
        {
            if (Time.time >= nextFireTime)
            {
                FireBullet();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void FireBullet()
    {
        GameObject bulletObj = Instantiate(bullet, shootPos.transform.position, shootPos.transform.rotation);

        Rigidbody rb = bulletObj.GetComponent<Rigidbody>();
        if (rb == null) rb = bulletObj.AddComponent<Rigidbody>();

        rb.linearVelocity = shootPos.transform.forward * bulletSpeed;

        // Clean up the bullet after 2 seconds to save memory
        Destroy(bulletObj, 2f);
    }
}