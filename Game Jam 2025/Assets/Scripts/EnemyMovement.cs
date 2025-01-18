using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float HeathPoint;
    [SerializeField] private float MaxHealth;
    [SerializeField] private int killReward;
    [SerializeField] HealthBar healthBar;
    private Transform target;
    private int pathIndex = 0;
    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }
    void Start()
    {
        target = LevelManager.main.path[0];
        healthBar.UpdateHealthBar(HeathPoint, MaxHealth);
    }
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }
    public void Damage(float dmg)
    {
        HeathPoint -= dmg;
        healthBar.UpdateHealthBar(HeathPoint, MaxHealth);
        if (HeathPoint <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
}
