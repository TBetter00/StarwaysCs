using UnityEngine;

public class ShootC : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float bulletSpeed = 30f;
    public float fireRate = 0.1f; // Smaller number = faster firing

    [Header("References")]
    public GameObject bullet;
    public GameObject shootPos;
    public PoolingManagerC poolingManager;

    private float nextFireTime = 0f;
    void Awake()
    {
        poolingManager = FindAnyObjectByType<PoolingManagerC>();
    }

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
        GameObject bulletObj = poolingManager.GetPoolObject(); //ดึง

        if (bulletObj != null)
        {
            bulletObj.transform.position = shootPos.transform.position;
            bulletObj.transform.rotation = shootPos.transform.rotation;

            bulletObj.SetActive(true); //ใช้งานจริง

            Rigidbody rb = bulletObj.GetComponent<Rigidbody>();
            if (rb == null) rb = bulletObj.AddComponent<Rigidbody>();

            rb.linearVelocity = shootPos.transform.forward * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("SOS หมดแล้วนายจ๋า");
        }


        // Clean up the bullet after 2 seconds to save memory
        // Destroy(bulletObj, 2f);
    }
}