using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float HeathPoint;
    [SerializeField] private int killReward;
    private Transform target;
    private int pathIndex = 0;
    void Start()
    {
        target = LevelManager.main.path[0];
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
    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
}
