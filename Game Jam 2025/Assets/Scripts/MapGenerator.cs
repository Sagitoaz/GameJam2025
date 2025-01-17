using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static List<GameObject> _mapTiles = new List<GameObject>();
    public static List<GameObject> _pathTiles = new List<GameObject>();
    public static GameObject startTile;
    public static GameObject endTile;
    public GameObject MapTile;
    public Color PathColor;
    public Color StartColor;
    public Color EndColor;
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;
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
        startTile = topEdgeTiles[rand1];
        endTile = bottomEdgeTiles[rand2];

        _currentTile = startTile;
        _pathTiles.Add(_currentTile);

        int loopCount = 0;

        while (_currentTile != endTile)
        {
            loopCount++;
            if (loopCount > 100)
            {
                Debug.Log("Break Loop!!!");
                break;
            }

            if (Random.value > 0.5f)
            {
                if (_currentTile.transform.position.x > endTile.transform.position.x)
                {
                    MoveLeft();
                }
                else if (_currentTile.transform.position.x < endTile.transform.position.x)
                {
                    MoveRight();
                }
            }
            else
            {
                if (_currentTile.transform.position.y > endTile.transform.position.y)
                {
                    MoveDown();
                }
            }

            if (!_pathTiles.Contains(_currentTile))
            {
                _pathTiles.Add(_currentTile);
            }
        }

        foreach (GameObject obj in _pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().color = PathColor;
        }
        startTile.GetComponent<SpriteRenderer>().color = StartColor;
        endTile.GetComponent<SpriteRenderer>().color = EndColor;
    }
}
