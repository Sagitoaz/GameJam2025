using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemyHealth;
    private float _speed;
    private int _killReward;
    private int _damage;
    private GameObject _targetTile;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void SpawnEnemy()
    {
        _targetTile = MapGenerator.startTile;
    }
    private void CalculateMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetTile.transform.position, _speed * Time.deltaTime);
    }
    private void CheckPosition()
    {
        if (_targetTile != null && _targetTile != MapGenerator.endTile)
        {
            float distance = (transform.position - _targetTile.transform.position).magnitude;
        }
    }
}
