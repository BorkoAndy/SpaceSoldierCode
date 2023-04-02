using UnityEngine;

public class SpawningFactory : MonoBehaviour
{
    public Transform[] _spawningPoints;

    [SerializeField] private GameObject _enemyBoss;
    [SerializeField] private GameObject[] _enemyWaves;
    [SerializeField] private Transform _npcParent;
    [SerializeField] private int _enemiesInWave;
    private ObjectPool _pool;
    private int _currentWave;    
    private bool _spawning;
    private bool _bossCreated;

    private void Start() => CreateWave();
    private void Update()
    {
        if(_npcParent.transform.childCount <=0 && !_bossCreated) CreateWave();
    }
    private void CreateWave()
    {
        if (_currentWave <= _enemyWaves.Length - 1)
        {
            _spawning = true;
            GameObject newObject = _enemyWaves[_currentWave];
            _pool = new ObjectPool(_enemyWaves[_currentWave], _enemiesInWave);
            _currentWave++;
            for (int i = 0; i < _enemiesInWave; i++)           
                CreateEnemy();            
            _spawning = false;
        }
        else CreateBoss();
    }
    public void CreateEnemy()
    {
        GameObject newEnemy = _pool.GetFromPool();
        newEnemy.transform.parent = _npcParent;
        newEnemy.transform.position = _spawningPoints[Random.Range(0, _spawningPoints.Length)].position;
        newEnemy.SetActive(true);   
    }
    private void CreateBoss()
    {
        _bossCreated = true;
        Instantiate(_enemyBoss, _spawningPoints[_spawningPoints.Length- 1]);
    }
}
