using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShootAndHit : MonoBehaviour
{
    [SerializeField] private GameObject _bulletSource;
    [SerializeField] private GameObject _flash;    
    [SerializeField] private ParticleSystem _hitEffectOnObjectShoot;
    [SerializeField] private ParticleSystem _hitEffectOnEnemyShoot;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _flashLifeTime;
    [SerializeField] private float _damage; // Depends on used weapon
    [SerializeField] private float _shootingRange = 500.0f;
    [SerializeField] private Transform _weapon;
    [SerializeField] private GameObject _bulletHolePrefab;
    [SerializeField] private int _bulletHolesAmount = 50;    
    
    private float _flashTimer;
    private LineRenderer _lineRenderer;
    private ObjectPool _bulletHoles;
    private LayerMask _enemyHeadLayer, _enemyBodyLayer, _shootableStuffLayer, _locationLayer ;

    public static Action OnShotMade;
    private void OnEnable() => PlayerStats.OnPlayerDeath += Inactivate;
    private void OnDisable() => PlayerStats.OnPlayerDeath -= Inactivate;
    private void Start()
    {
        _enemyHeadLayer = LayerMask.NameToLayer("EnemyHead");
        _enemyBodyLayer = LayerMask.NameToLayer("EnemyBody");
        _shootableStuffLayer = LayerMask.NameToLayer("Shootable");
        _locationLayer = LayerMask.NameToLayer("Location");
        _lineRenderer = GetComponent<LineRenderer>();
        _bulletHoles = new ObjectPool(_bulletHolePrefab, _bulletHolesAmount);
        
    }

    private void Update()
    {
        Aim();
        _flashTimer -= Time.deltaTime;
        if (_flashTimer <= 0 && _flash.activeSelf == true) _flash.SetActive(false);        
       
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) Shoot();

    }
    public void Shoot()
    {                
        _flash.SetActive(true);
        _flashTimer = _flashLifeTime;
        OnShotMade?.Invoke();

        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit))
        {
            var layer = hit.collider.gameObject.layer;
           
            if(layer == _enemyHeadLayer || layer == _enemyBodyLayer)
            {
                _hitEffectOnEnemyShoot.transform.position = hit.point;
                _hitEffectOnEnemyShoot.Play(); 
            }
            else
            {
                _hitEffectOnObjectShoot.transform.position = hit.point;
                _hitEffectOnObjectShoot.Play();
            }

            if (layer == _enemyHeadLayer)
            {                
                hit.collider.gameObject.GetComponentInParent<IKillable>().HeadShot();
            }
            else if (layer == _enemyBodyLayer)
            {
                hit.collider.gameObject.GetComponentInParent<IKillable>().BodyShot(_damage);
            }
            else if (layer  == _shootableStuffLayer)
            {
                hit.rigidbody?.AddForce((hit.collider.transform.position - transform.position).normalized * 10, ForceMode.Impulse);
                
            }
            else if(layer == _locationLayer)
            {                
                var newHole = _bulletHoles.GetFromPool();
                newHole.gameObject.SetActive(true);
                newHole.transform.position = hit.point;
                newHole.transform.rotation = Quaternion.LookRotation(hit.normal);
                StartCoroutine(DestroyHole(newHole));
            }
            
        }       
    }
    private void Aim()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _shootingRange))
        {
            _lineRenderer.SetPosition(0, _bulletSource.transform.position);
            _lineRenderer.SetPosition(1, hit.point);
        }
        
    }
    private void Inactivate() => _lineRenderer.enabled= false;

    private IEnumerator DestroyHole(GameObject newHole)
    {
        yield return new WaitForSeconds(2);
        _bulletHoles.ReturnToPool(newHole);
    }   
}
