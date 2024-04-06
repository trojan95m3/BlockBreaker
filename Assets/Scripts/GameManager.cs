using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager pInstance;

    public Action OnGameStart;
    public Action OnGameOver;
    public Action OnProjectileShot;
    public Action OnBlockHit;
    public Action OnBlockDestroyed;

    /// <summary>
    /// Set the static instance
    /// </summary>
    private void Awake()
    {
        pInstance = this;
    }

    /// <summary>
    /// Call OnGameStart at the end of frame so other objects can register 
    /// to OnGameStart in their Start method
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        OnGameStart?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    /// <summary>
    /// Called from UI to restart the game
    /// </summary>
    public void Restart()
    {
        OnGameStart?.Invoke();
    }

    public void ProjectileShot()
    { 
        OnProjectileShot?.Invoke();
    }

    public void BlockHit()
    {
        OnBlockHit?.Invoke();
    }

    public void BlockDestroyed()
    {
        OnBlockDestroyed?.Invoke();
    }
}
