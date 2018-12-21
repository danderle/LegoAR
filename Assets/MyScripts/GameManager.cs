using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity;

public enum BlockColor
{
    White = 0,
    Red = 1,
    Green = 2,
    Blue = 3,
}

public class Block
{
    public Transform BlockTransform;
    public BlockColor BColor;
}

public class GameManager : MonoBehaviour
{
    #region Private Members

    private GameObject mFoundationObject;
    private Vector3 mBlockOffset;
    private Vector3 mFoundationCenter = new Vector3(1.25f, 0, 1.25f);
    private float mBlockSize = 0.25f;
    private bool mIsDeleting = false;
    #endregion

    #region Public Members

    public static GameManager InstanceGameManager { set;  get; }
    public GameObject BlockPrefab;
    public Block[,,] Blocks = new Block[20, 20, 20];
    public BlockColor SelectedColor;
    public Material[] BlockMaterials;
    #endregion

    #region Private Methods

    /// <summary>
    /// Starts the Game
    /// </summary>
    private void Start()
    {
        InstanceGameManager = this;
        mFoundationObject = GameObject.Find("Foundation");
        mBlockOffset = Vector3.one * (0.5f / 4);
        SelectedColor = BlockColor.White;
    }

    /// <summary>
    /// Updating Game
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 30.0f))
            {
                
                if(mIsDeleting )
                {
                    if(hit.transform.name != "Foundation")
                    {
                        Vector3 oldCubeIndex = BlockPosition(hit.point - (hit.normal * (mBlockSize - 0.01f)));
                        Destroy(Blocks[(int)oldCubeIndex.x, (int)oldCubeIndex.y, (int)oldCubeIndex.z].BlockTransform.gameObject);
                        Blocks[(int)oldCubeIndex.x, (int)oldCubeIndex.y, (int)oldCubeIndex.z] = null;
                    }
                    return;
                }

                Vector3 index = BlockPosition(hit.point);
                GameObject go = CreateBlock();
                

                if (Blocks[(int)index.x, (int)index.y, (int)index.z] == null)
                {
                    PositionBlock(go.transform, index);

                    Blocks[(int)index.x, (int)index.y, (int)index.z] = new Block
                    {
                        BlockTransform = go.transform,
                        BColor = SelectedColor,
                    };
                }
                else
                {
                    Vector3 newIndex = BlockPosition(hit.point + (hit.normal * mBlockSize));
                    PositionBlock(go.transform, newIndex);
                }

            }
        }
    }

    private GameObject CreateBlock()
    {
        GameObject go = Instantiate(BlockPrefab) as GameObject;
        go.GetComponent<Renderer>().material = BlockMaterials[(int)SelectedColor];
        go.transform.localScale = Vector3.one * mBlockSize;
        return go;
    }

    public GameObject CreateBlock(int x, int y, int z, Block b)
    {
        GameObject go = Instantiate(BlockPrefab) as GameObject;
        go.GetComponent<Renderer>().material = BlockMaterials[(int)b.BColor];
        go.transform.localScale = Vector3.one * mBlockSize;

        b.BlockTransform = go.transform;
        Blocks[x, y, z] = b;

        PositionBlock(b.BlockTransform, new Vector3(x, y, z));

        return go;
    }

    private Vector3 BlockPosition(Vector3 hit)
    {
        int x = (int)(hit.x / mBlockSize);
        int y = (int)(hit.y / mBlockSize);
        int z = (int)(hit.z / mBlockSize);

        return new Vector3(x, y, z);
    }


    public void PositionBlock(Transform t, Vector3 index)
    {
        t.position = ((index * mBlockSize) + mBlockOffset) + (mFoundationObject.transform.position - mFoundationCenter);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Changes the color of the block after clicking the slected color button
    /// </summary>
    /// <param name="color"></param>
    public void ChangeBlockColor(int color)
    {
        SelectedColor = (BlockColor)color;
    }

    public void ToggleDelete()
    {
        mIsDeleting = !mIsDeleting;
    }

    #endregion
}
















