using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private bool mIsSaving;

    #region Public Members

    public GameObject SaveMenu;
    public GameObject ConfirmMenu;
    public Transform SaveList;
    public GameObject SavePrefab;

    #endregion


    #region Private Methods

    private void Save()
    {
        string saveData = string.Empty;

        Block[,,] b = GameManager.InstanceGameManager.Blocks;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Block currentBlock = b[i, j, k];
                    if (currentBlock == null)
                        continue;

                    saveData += i.ToString() + "|" + j.ToString() + "|" + k.ToString() + "|" + ((int)currentBlock.BColor).ToString() + "%";
                }
            }
        }

        PlayerPrefs.SetString("TEST", saveData);
    }

    private void Load()
    {
        string save = PlayerPrefs.GetString("TEST");

        string[] blockData = save.Split('%');

        for(int i = 0; i < blockData.Length; i++)
        {
            string[] currentBlock = blockData[i].Split('|');
            int x = int.Parse(currentBlock[0]);
            int y = int.Parse(currentBlock[1]);
            int z = int.Parse(currentBlock[2]);

            int c = int.Parse(currentBlock[3]);

            Block b = new Block() { BColor = (BlockColor)c };

            GameManager.InstanceGameManager.CreateBlock(x, y, z, b);
        }
    }

    #endregion
    #region Public Methods

    public void OnSaveMenuClick()
    {
        SaveMenu.SetActive(true);
    }

    public void OnSaveClick()
    {
        SaveMenu.SetActive(false);
        ConfirmMenu.SetActive(true);
        mIsSaving = true;
    }


    public void OnLoadClick()
    {
        SaveMenu.SetActive(false);
        ConfirmMenu.SetActive(true);
        mIsSaving = false;
    }

    public void OnCancelClick()
    {
        SaveMenu.SetActive(false);
    }

    public void OnConfirmOK()
    {
        if (mIsSaving)
            Save();
        else
            Load();

        ConfirmMenu.SetActive(false);
    }

    public void OnConfirmCancel()
    {
        ConfirmMenu.SetActive(false);
    }

    

    #endregion
}
