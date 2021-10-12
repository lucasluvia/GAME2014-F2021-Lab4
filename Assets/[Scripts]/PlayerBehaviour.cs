using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")] 
    [Range(0.0f, 200.0f)]
    public float horizontalForce;
    [Range(0.0f, 1.0f)]
    public float decay;
    public Bounds bounds;
    public int frameDelay;

    [Header("Player Attack")]
    public Transform bulletSpawn;
    
    private Rigidbody2D rigidbody; 
    private BulletManager bulletManager;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.FindObjectOfType<BulletManager>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckBounds();
        CheckFire();
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");

        rigidbody.AddForce(new Vector2(x * horizontalForce, 0.0f));

        rigidbody.velocity *= (1.0f - decay);
    }

    private void CheckBounds()
    {
        // Left Boundary
        if (transform.position.x < bounds.min)
        {
            transform.position = new Vector2(bounds.min, transform.position.y);
        }

        // Right Boundary
        if (transform.position.x > bounds.max)
        {
            transform.position = new Vector2(bounds.max, transform.position.y);
        }
    }

    private void CheckFire()
    {
        if (Input.GetAxisRaw("Jump") > 0 && Time.frameCount % frameDelay == 0)
        {
            bulletManager.GetBullet(bulletSpawn.position, BulletType.PLAYER);
        }
    }
}
