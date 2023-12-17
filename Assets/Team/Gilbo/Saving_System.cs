using System;
using UnityEngine;

public class Saving_System : MonoBehaviour
{
    [SerializeField]
    private bool _isencrypted;
    
    public string _path;

    private IDataService dataService = new JsonDataService();

    private Player_Save playerSave = new Player_Save();
    
    



    public void SerializeJson()
    {
        
        if (dataService.SaveData(_path, playerSave, _isencrypted))
        {
            try
            {
                Player_Save data = dataService.LoadData<Player_Save>(_path, _isencrypted);

            }
            catch (Exception e)
            {
                Debug.LogError($"Could not read file!" + e);
            }
        }
        else
        {
            Debug.LogError("Could not save file!");
        }
    }


    public void SavePlayer_LevelSystem(int lvl, int rank, int xp, int xpneed)
    {
        playerSave.playerLevel = lvl;
        playerSave.rankingLevel = rank;
        playerSave.playerXP = xp;
        playerSave.XPneeded = xpneed;
        
        SerializeJson();

    }


    public void LoadPlayer_LevelSystem()
    {
        Player_Save data = dataService.LoadData<Player_Save>(_path, _isencrypted);
        // load data here 
       // levelSystem.LoadPlayer(data.playerLevel, data.rankingLevel, data.playerXP, data.XPneeded);
    }




}

