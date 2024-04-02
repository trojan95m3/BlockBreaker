using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public SpriteRenderer _Sprite;

    private int hitsLeft;

    public void Init(int numHits, Color color)
    {
        hitsLeft = numHits;
        _Sprite.color = color;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitsLeft--;
        if (hitsLeft <= 0)
            GameManager.pInstance.RemoveBlock(this);
    }
}
