using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemyHealth;
    [SerializeField] private float _speed;
    [SerializeField] private int _killReward;
    private int _damage;
    private GameObject _targetTile;
    void Awake()
    {
        EnemyContainer.enemies.Add(gameObject);
    }
    void Start()
    {
        SpawnEnemy();
    }
    void Update()
    {
        CheckPosition();
        CalculateMovement();
    }
    private void SpawnEnemy()
    {
        _targetTile = MapGenerator.startTile;
    }
    private void CalculateMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetTile.transform.position, _speed * Time.deltaTime);
        if (transform.position == MapGenerator.endTile.transform.position)
        {
            die();
        }
    }
    private void CheckPosition()
    {
        if (_targetTile != null && _targetTile != MapGenerator.endTile)
        {
            float distance = (transform.position - _targetTile.transform.position).magnitude;
            if (distance < 0.001f)
            {
                int currentIndex = MapGenerator._pathTiles.IndexOf(_targetTile) + 1;
                _targetTile = MapGenerator._pathTiles[currentIndex + 1];
            }
        }
    }
    public void TakeDmg(float amount)
    {
        _enemyHealth -= amount;
        if (_enemyHealth <= 0)
        {
            die();
        }
    }
    private void die()
    {
        EnemyContainer.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }
}
