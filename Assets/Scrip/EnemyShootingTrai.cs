using UnityEngine;

public class EnemyShootingTrai : MonoBehaviour
{
    public Transform gunTransform;  
    public GameObject bulletPrefab;
    public Transform firePoint; 
    public float bulletSpeed = 5f;
    public float fireRate = 2f;

    private float nextFireTime;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        RotateGun();
        Shoot();
    }

    void RotateGun()
    {
        if (spriteRenderer.flipX) 
        {
            gunTransform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else 
        {
            gunTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Shoot()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            // Đảo ngược hướng bắn
            float direction = spriteRenderer.flipX ? 1f : -1f; 
            float rotationAngle = spriteRenderer.flipX ? 0f : 180f; 
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, rotationAngle));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Destroy(bullet, 3f);

            // Cập nhật vận tốc đạn để bắn ngược lại
            rb.linearVelocity = new Vector2(bulletSpeed * direction, 0);
        }
    }


}