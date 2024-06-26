using System.Collections;
using UnityEngine;

/// <summary>
/// Projectile class sets the velocity of the projectile and its lifespan.
/// Tells the GameManager to remove it if colliding with the trigger (bottom).
/// </summary>
public class Projectile : MonoBehaviour
{
    public Rigidbody2D _Rigidbody2D;

    private Turret mTurret;

    public void Init(Turret turret, Vector2 velocity, float lifespan)
    {
        mTurret = turret;
        _Rigidbody2D.velocity = velocity;
        StartCoroutine(Lifespan(lifespan));
    }

    IEnumerator Lifespan(float lifespan)
    { 
        yield return new WaitForSeconds(lifespan);

        mTurret.RemoveProjectile(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAllCoroutines();
        mTurret.RemoveProjectile(this);
    }
}
