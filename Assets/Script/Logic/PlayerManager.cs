using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerManager
{
    public Player mPlayerPrefab;
    public int mMaxPlayerCount = 2;

    private bool mIsGameStarted = false;
    private List<Player> mPlayersList;
    private int mPlayerIdGenerator = 1;

    public void StartGame()
    {
        mPlayersList = new List<Player>(mMaxPlayerCount);
        mIsGameStarted = true;
    }

    public void EndGame()
    {
        if (!mIsGameStarted)
        {
            return;
        }

        foreach (Player player in mPlayersList)
        {
            if(player != null)
            {
                GameObject.Destroy(player.gameObject);
            }
        }
    }

    public int GetCurrentPlayerCount()
    {
        return mPlayersList.Count;
    }

    public bool RequestNewPlayer(InputDevice inputDevice, Vector3 spawnPosition)
    {
        if (mPlayersList.Count >= mMaxPlayerCount)
        {
            return false;
        }

        foreach (Player player in mPlayersList)
        {
            if (player.IsInputDevicePaired(inputDevice))
            {
                return false;
            }
        }

        // no device is connected to any player, create a new player
        if (mPlayerPrefab != null)
        {
            Player newPlayer = GameObject.Instantiate<Player>(mPlayerPrefab);
            newPlayer.Init(mPlayerIdGenerator++, spawnPosition);
            newPlayer.SetInputDevice(inputDevice, true);
            mPlayersList.Add(newPlayer);
            return true;
        }

        return false;
    }
}
