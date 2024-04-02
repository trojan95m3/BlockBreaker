using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager pInstance;

    [Header("Turret Settings")]
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

    [Header("Layout Settings")]
    [Tooltip("Parent object to place all the Blocks under")]
    public Transform _Parent;
    [Tooltip("the block prefab")]
    public GameObject _Block;
    [Tooltip("the percentage of screen space to leave blank at the top")]
    public int _BlankPercentage;
    [Tooltip("the percentage of screen space to use for the layout")]
    public int _Percentage;
    [Tooltip("the color will show the number of hits needed")]
    public List<Color> _HitColors;

    private float lastFireTime = 0;

    private List<Projectile> projectiles = new List<Projectile>();
    private List<Block> blocks =  new List<Block>();

    void Start()
    {
        pInstance = this;

        CreateLayout();
    }

    void CreateLayout()
    {
        float aspect = (float)Screen.width / Screen.height;
        float height = Camera.main.orthographicSize * 2;
        float width = height * aspect;

        // get the amount of screen space to use for the layout
        float space = height * (_Percentage / 100f);
        float blockHeight = _Block.transform.localScale.y;
        int rows = (int)(space / blockHeight);

        // find where to start based on blank percentage
        float startY = (height * 0.5f)- (height * (_Percentage / 100f));

        // find how many fit
        float blockWidth = _Block.transform.localScale.x;
        int count = (int)(width * blockWidth);

        for(int i =  0; i < rows; i++) 
        { 
            for(int j = 0; j < count; j++) 
            {
                Block block = GetBlock();
                block.transform.position = new Vector2(j * blockWidth - ((int)width * 0.5f) + blockWidth * 0.5f, startY - i * blockHeight + blockHeight * 0.5f);
                int hits = Random.Range(0, _HitColors.Count) + 1;
                block.Init(hits, _HitColors[hits - 1]);
            }
        }
    }

    void Update()
    {
        // move the turret
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 offset = mousePosition - _Turret.position;
        offset.z = 0;
        float angle = Vector3.Angle(offset, Vector3.up);
        if (angle <= _MaxAngle)
            _Turret.up = offset.normalized;

        if(Input.GetKey(KeyCode.Space))
        {
            float rateOfFire = 1 / _RateOfFire;

            if ((Time.realtimeSinceStartup - lastFireTime) > rateOfFire)
            {
                lastFireTime = Time.realtimeSinceStartup;

                Projectile projectile = GetProjectile();
                Vector2 direction = _Turret.transform.up;
                projectile.Init(direction * _ProjectileSpeed, _ProjectileLifeSpan);
            }
        }
    }

    Projectile GetProjectile()
    {
        Projectile projectile;
        if (projectiles.Count > 0)
        {
            projectile = projectiles[0];
            projectiles.RemoveAt(0);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _Turret.position + _Turret.up;
        }
        else
            projectile = Instantiate(_Projectile, _Turret.position + _Turret.up, Quaternion.identity).GetComponent<Projectile>();

        return projectile;
    }

    public void RemoveProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectiles.Add(projectile);
    }

    Block GetBlock()
    {
        Block block;
        if (blocks.Count > 0)
        {
            block = blocks[0];
            blocks.RemoveAt(0);
            block.gameObject.SetActive(true);
        }
        else
            block = Instantiate(_Block, _Parent).GetComponent<Block>();

        return block;
    }

    public void RemoveBlock(Block block)
    {
        block.gameObject.SetActive(false);
        blocks.Add(block);
    }
}
