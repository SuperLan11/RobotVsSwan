using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Swan : MonoBehaviour, IHealth
{
    public float minWanderY;
    public float maxWanderY;
    public float minWanderX;
    public float maxWanderX;

    public Vector2 wanderTarget;
    public GameObject projectilePrefab;
    
    private bool isWithinWanderRange(Vector2 pos)
    {
        return pos.x >= minWanderX && pos.x <= maxWanderX && pos.y >= minWanderY && pos.y <= maxWanderY;
    }
    
    private void randomizeWanderTarget()
    {
        Vector2 pos;
        do
        {
            float angle = Random.Range(0, 2 * Mathf.PI);
            Vector2 offset = 2 * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            pos = (Vector2) transform.position + offset;
        } while (!isWithinWanderRange(pos));
        wanderTarget = pos;
    }

    private void ClampPosition()
    {
        float x = Mathf.Clamp(transform.position.x, minWanderX, maxWanderX);
        float y = Mathf.Clamp(transform.position.y, minWanderY, maxWanderY);
        transform.position = new Vector2(x, y);
    }

    protected void Wander()
    {
        ClampPosition();
        transform.position = Vector2.MoveTowards(transform.position, wanderTarget, Time.deltaTime);
        if (Vector2.Distance(transform.position, wanderTarget) < 0.1f)
        {
            randomizeWanderTarget();
        }
    }

    protected void Face(Vector2 target)
    {
        bool flip = target.x < transform.position.x;
        GetComponent<SpriteRenderer>().flipX = flip;
    }

    public float shootPoseTime;
    public Sprite shootPose;
    protected IEnumerator Shoot()
    {
        originalPose = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = shootPose;
        Face(Robot.instance.transform.position);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponentInChildren<BaseProjectile>().Fire(Robot.instance.transform.position - transform.position, 0);
        yield return new WaitForSeconds(shootPoseTime);
        GetComponent<SpriteRenderer>().sprite = originalPose;
    }

    public float dashSpeed;
    public int dashDamage;
    private bool collisionFlag;
    public Sprite dashPose;
    private Sprite originalPose;
    protected IEnumerator DashAt(Vector2 target)
    {
        collisionFlag = false;
        Vector2 startPos = transform.position;
        Face(target);
        GetComponent<SpriteRenderer>().sprite = dashPose;
        while (!collisionFlag && Vector2.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * dashSpeed);
            yield return null;
        }
        GetComponent<SpriteRenderer>().sprite = originalPose;
        while (Vector2.Distance(transform.position, startPos) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, Time.deltaTime * dashSpeed);
            yield return null;
        }

    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        Robot robot = col.collider.gameObject.GetComponent<Robot>();
        if (robot != null)
        {
            collisionFlag = true;
            robot.TakeDamage(dashDamage);
        }
    }

    protected virtual void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        originalPose = GetComponent<SpriteRenderer>().sprite;
        randomizeWanderTarget();
        StartCoroutine(swanAI());
        maxHealth = health;
    }

    public float shootChance;
    public float dashChance;
    protected IEnumerator swanAI()
    {
        while (true)
        {
            if (RoundManager.instance.roundState != RoundState.GAME)
            {
                yield return null;
                continue;
            }
            float criticalHealthMultiplier = isCriticalHealth() ? 10f : 1;
            if (Random.Range(0f, 1f) < Time.deltaTime * dashChance * criticalHealthMultiplier)
            {
                yield return DashAt(Robot.instance.transform.position);
            }
            if (Random.Range(0f, 1f) < Time.deltaTime * shootChance * criticalHealthMultiplier)
            {
                yield return Shoot();
            }

            Wander();
            yield return null;
        }
    }

    public int health;
    public int GetHealth()
    {
        return health;
    }
    
    
    private int maxHealth;
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    
    public float hurtTimer = 0;

    public void TakeDamage(int amount)
    {
        hurtTimer += 0.1f;
        health -= amount;
        if (true)
        {
            AudioManager.instance.Play("swan_death");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BaseProjectile baseProjectile = other.gameObject.GetComponentInChildren<BaseProjectile>();
        if (baseProjectile != null)
        {
            TakeDamage(baseProjectile.GetDamage());
            Destroy(baseProjectile.gameObject);
        }
    }

    bool isCriticalHealth()
    {
        return ((float)health / maxHealth) < 0.33f;
    }

    void Update()
    {
        hurtTimer = Mathf.Max(0, hurtTimer - Time.deltaTime);
        if (hurtTimer > 0 || isCriticalHealth())
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f, 1f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
