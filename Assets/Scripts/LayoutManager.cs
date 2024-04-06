using System;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class LayoutManager : MonoBehaviour
{
    [Tooltip("the block prefab")]
    public GameObject _Block;
    [Tooltip("the percentage of screen space to leave blank at the top")]
    public int _BlankPercentage;
    [Tooltip("the percentage of screen space to use for the layout")]
    public int _Percentage;
    [Tooltip("the percentage chance a block is not created")]
    public int _BlankChance;
    [Tooltip("the color will show the number of hits needed")]
    public List<Color> _HitColors;

    private int mNumBlocks = 0;
    private Queue<Block> mBlocks = new Queue<Block>();

    private void Start()
    {
        GameManager.pInstance.OnGameStart += CreateLayout;
    }

    private void OnDestroy()
    {
        GameManager.pInstance.OnGameStart -= CreateLayout;
    }

    /// <summary>
    /// Based on the size of the screen and the parameters create the layout.
    /// </summary>
    private void CreateLayout()
    {
        float aspect = (float)Screen.width / Screen.height;
        float height = Camera.main.orthographicSize * 2;
        float width = height * aspect;

        // get the amount of screen space to use for the layout
        float space = height * (_Percentage / 100f);
        float blockHeight = _Block.transform.localScale.y;
        int rows = (int)(space / blockHeight);

        // find where to start based on blank percentage
        float startY = (height * 0.5f) - (height * (_Percentage / 100f));

        // find how many fit
        float blockWidth = _Block.transform.localScale.x;
        int count = (int)(width * blockWidth);

        mNumBlocks = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < count; j++)
            {
                int chance = UnityEngine.Random.Range(0, 100);
                if (chance < _BlankChance)
                    continue;

                Block block = GetBlock();
                block.transform.position = new Vector2(j * blockWidth - ((int)width * 0.5f) + blockWidth * 0.5f, startY - i * blockHeight + blockHeight * 0.5f);
                int hits = UnityEngine.Random.Range(0, _HitColors.Count) + 1;
                block.Init(this, hits, _HitColors[hits - 1]);
                mNumBlocks++;
            }
        }
    }
    /// <summary>
    /// Gets a block from the pool
    /// </summary>
    /// <returns>Block</returns>
    private Block GetBlock()
    {
        Block block;
        if (mBlocks.Count > 0)
        {
            block = mBlocks.Dequeue();
            block.gameObject.SetActive(true);
        }
        else
            block = Instantiate(_Block, transform).GetComponent<Block>();

        return block;
    }

    /// <summary>
    /// Adds a block to the pool and decreases mNumBlocks.
    /// If mNumBlocks reaches 0 then GameOver.
    /// </summary>
    /// <param name="block"></param>
    public void RemoveBlock(Block block)
    {
        GameManager.pInstance.BlockDestroyed();
        block.gameObject.SetActive(false);
        mBlocks.Enqueue(block);

        mNumBlocks--;
        if (mNumBlocks == 0)
            GameManager.pInstance.GameOver();
    }
}
