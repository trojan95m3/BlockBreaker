using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Tooltip("The transform that will be rotated")]
    public Transform _Turret;
    [Tooltip("The max allowed angle for the turret to move relative to the Y axis")]
    public float _MaxAngle;
    [Tooltip("the projectile")]
    public GameObject _Projectile;
    [Tooltip("How many projecticles can be fired per second")]
    public float _RateOfFire;
    [Tooltip("How fast is the projectile")]
    public float _ProjectileSpeed;
    [Tooltip("How long does the projectile last (seconds)")]
    public float _ProjectileLifeSpan;

    private bool mGameOver = false;
    private float mLastFireTime = 0;
    private Queue<Projectile> mProjectiles = new Queue<Projectile>();

    void Start()
    {
        GameManager.pInstance.OnGameStart += GameStart;
        GameManager.pInstance.OnGameOver += GameOver;
    }

    private void OnDestroy()
    {
        GameManager.pInstance.OnGameStart -= GameStart;
        GameManager.pInstance.OnGameOver -= GameOver;
    }

    void Update()
    {
        if (mGameOver)
            return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 offset = mousePosition - _Turret.position;
        offset.z = 0;
        float angle = Vector3.Angle(offset, Vector3.up);
        if (angle <= _MaxAngle)
            _Turret.up = offset.normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            float rateOfFire = 1 / _RateOfFire;

            if ((Time.realtimeSinceStartup - mLastFireTime) > rateOfFire)
            {
                mLastFireTime = Time.realtimeSinceStartup;

                Projectile projectile = GetProjectile();
                Vector2 direction = _Turret.transform.up;
                projectile.Init(this, direction * _ProjectileSpeed, _ProjectileLifeSpan);

                GameManager.pInstance.ProjectileShot();
            }
        }
    }

    private void GameStart()
    {
        mGameOver = false;
    }

    private void GameOver()
    {
        mGameOver = true;

        Projectile[] projectiles = FindObjectsOfType<Projectile>();
        foreach (Projectile projectile in projectiles)
            RemoveProjectile(projectile);
    }

    /// <summary>
    /// Get a projectile from the pool
    /// </summary>
    /// <returns>Projectile</returns>
    private Projectile GetProjectile()
    {
        Projectile projectile;
        if (mProjectiles.Count > 0)
        {
            projectile = mProjectiles.Dequeue();
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _Turret.position + _Turret.up;
        }
        else
            projectile = Instantiate(_Projectile, _Turret.position + _Turret.up, Quaternion.identity).GetComponent<Projectile>();

        return projectile;
    }

    /// <summary>
    /// Add a projectile to the pool
    /// </summary>
    /// <param name="projectile"></param>
    public void RemoveProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        mProjectiles.Enqueue(projectile);
    }
}
