using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _dmg;
    [SerializeField] private float _timeBetweenShot;
    private float _nextTimeToShoot;
    public GameObject _currentTarget;
    void Start()
    {
        _nextTimeToShoot = Time.time;
    }
    void Update()
    {
        UpdateNearestEnemy();
        if (Time.time > _nextTimeToShoot)
        {
            if (_currentTarget != null)
            {
                Shoot();
                _nextTimeToShoot = Time.time + _timeBetweenShot;
            }
        }
    }
    private void UpdateNearestEnemy()
    {
        GameObject currentNearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject enemy in EnemyContainer.enemies)
        {
            if (enemy != null)
            {
                float newDistance = (transform.position - enemy.transform.position).magnitude;
                if (newDistance < distance)
                {
                    distance = newDistance;
                    currentNearest = enemy;
                }
            }
        }

        if (distance <= _range)
        {
            _currentTarget = currentNearest;
        }
        else
        {
            _currentTarget = null;
        }
    }
    private void Shoot()
    {
        Enemy enemyScript = _currentTarget.GetComponent<Enemy>();
        enemyScript.TakeDmg(_dmg);
    }
}
