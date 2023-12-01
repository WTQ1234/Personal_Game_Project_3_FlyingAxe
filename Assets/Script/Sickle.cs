using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    public SickleInfo sickleInfo;

    public float speed;
    public float rotateSpeed;
    public int damage;
    public float tuning;

    private Rigidbody2D rb2d;
    private Transform playerTransform;
    private Transform sickleTransform;
    private Vector2 startSpeed;

    private CameraShake camShake;
    public bool back = false;

    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        //rb2d.velocity = transform.right * speed;
        //startSpeed = rb2d.velocity;
        //playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //sickleTransform = GetComponent<Transform>();
        //camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    public void Init(Vector3 speedDir, SickleInfo _sickleInfo)
    {
        sickleInfo = _sickleInfo;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = speedDir * speed;
        startSpeed = rb2d.velocity;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sickleTransform = GetComponent<Transform>();
        camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);
        if (isCollected)
        {
            if (Player.Instance != null)
            {
                float distance = (transform.position - Player.Instance.transform.position).sqrMagnitude;
                transform.position = Vector2.MoveTowards(transform.position,
                                                        Player.Instance.transform.position,
                                                        backSpeed * Time.deltaTime);
            }
        }
        else
        {
            rb2d.velocity = rb2d.velocity - startSpeed * Time.deltaTime;
            if (rb2d.velocity.magnitude < 1f)
            {
                back = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage + LevelManager.Instance.level / 3);
        }
    }

    public float backSpeed = 3;
    public bool isCollected;
    public void Collect()
    {
        if (!back)
        {
            return;
        }
        isCollected = true;
        rb2d.velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}
