using UnityEngine;

/// <summary>
/// The Block class will set the color of the block and the number of hits needed
/// to destroy.  Handle collision with the ball and once all hits are gone tell
/// the GameManager to remove it.
/// </summary>
public class Block : MonoBehaviour
{
    public SpriteRenderer _Sprite;

    private LayoutManager mLayoutManager;
    private int mHitsLeft;

    public void Init(LayoutManager layoutManager, int numHits, Color color)
    {
        mLayoutManager = layoutManager;
        mHitsLeft = numHits;
        _Sprite.color = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.pInstance.BlockHit();

        mHitsLeft--;
        if (mHitsLeft <= 0)
            mLayoutManager.RemoveBlock(this);
    }
}
