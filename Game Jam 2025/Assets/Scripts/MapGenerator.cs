using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject MapTile;
    public Color PathColor;
    public Color StartColor;
    public Color EndColor;
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;
    private List<GameObject> _mapTiles = new List<GameObject>();
    private List<GameObject> _pathTiles = new List<GameObject>();
    private bool _reachedX = false;
    private bool _reachedY = false;
    private GameObject _currentTile;
    private int _currentIndex;
    private int _nextIndex;
    void Start()
    {
        GenerateMap();
    }
    private void MoveDown()
    {
        _pathTiles.Add(_currentTile);
        _currentIndex = _mapTiles.IndexOf(_currentTile);
        _nextIndex = _currentIndex - _mapWidth;
        _currentTile = _mapTiles[_nextIndex];
    }
    private void MoveLeft()
    {
        _pathTiles.Add(_currentTile);
        _currentIndex = _mapTiles.IndexOf(_currentTile);
        _nextIndex = _currentIndex - 1;
        _currentTile = _mapTiles[_nextIndex];
    }
    private void MoveRight()
    {
        _pathTiles.Add(_currentTile);
        _currentIndex = _mapTiles.IndexOf(_currentTile);
        _nextIndex = _currentIndex + 1;
        _currentTile = _mapTiles[_nextIndex];
    }
    private List<GameObject> GetTopEdgeTile()
    {
        List<GameObject> edgeTiles = new List<GameObject>();
        for (int i = _mapWidth * (_mapHeight - 1); i < _mapHeight * _mapWidth; i++)
        {
            edgeTiles.Add(_mapTiles[i]);
        }
        return edgeTiles;
    }
    private List<GameObject> GetBottomEdgeTile()
    {
        List<GameObject> edgeTiles = new List<GameObject>();
        for (int i = 0; i < _mapWidth; i++)
        {
            edgeTiles.Add(_mapTiles[i]);
        }
        return edgeTiles;
    }
    public void GenerateMap()
    {
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                GameObject newTile = Instantiate(MapTile);
                _mapTiles.Add(newTile);
                newTile.transform.position = new Vector2(x, y);
            }
        }

        List<GameObject> topEdgeTiles = GetTopEdgeTile();
        List<GameObject> bottomEdgeTiles = GetBottomEdgeTile();
        int rand1 = Random.Range(0, _mapWidth);
        int rand2 = Random.Range(0, _mapWidth);
        GameObject startTile = topEdgeTiles[rand1];
        GameObject endTile = bottomEdgeTiles[rand2];

        _currentTile = startTile;
        MoveDown();

        int loopCount = 0;
        while (!_reachedX)
        {
            loopCount++;
            if (loopCount > 100)
            {
                Debug.Log("Break Loop!!!");
                break;
            }

            if (_currentTile.transform.position.x > endTile.transform.position.x)
            {
                MoveLeft();
            }
            else if (_currentTile.transform.position.x < endTile.transform.position.x)
            {
                MoveRight();
            }
            else
            {
                _reachedX = true;
            }
        }

        while (!_reachedY)
        {
            if (_currentTile.transform.position.y > endTile.transform.position.y)
            {
                MoveDown();
            }
            else
            {
                _reachedY = true;
            }
        }

        _pathTiles.Add(endTile);

        foreach (GameObject obj in _pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().color = PathColor;
        }
        startTile.GetComponent<SpriteRenderer>().color = StartColor;
        endTile.GetComponent<SpriteRenderer>().color = EndColor;
    }
}
