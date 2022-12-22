using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProgressConroller : MonoBehaviour
{
    public class SaveFile
    {
        public List<string> EnemyList;
        public List<string> ZoneList;
        public List<string> MaskList;
        public int WinedRuns=1;
        public bool SecondHalf=false;
        
        public void AddEnemy(string code)
        {
            EnemyList.Add(code);
        }

        public void AddZone(string code)
        {
            ZoneList.Add(code);
        }

        public void AddMask(string code)
        {
            MaskList.Add(code);
        }

        public void IncreaseWins()
        {
            WinedRuns ++;
        }

        public void UnlockSecondHalf()
        {
            SecondHalf= true;
        }
        public bool CheckForEnemy(string code)
        {
            foreach (string s in EnemyList)
            {
                if (s == code)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckForZone(string code)
        {
            foreach (string s in ZoneList)
            {
                if (s == code)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckForMask(string code)
        {
            foreach (string s in MaskList)
            {
                if (s == code)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetWinedRuns()
        {
            return WinedRuns;
        }

        public bool CheckSecondHalf()
        {
            return SecondHalf;
        }
    }

    public SaveFile saveFile = new SaveFile();

    private void Start()
    {
        string json= File.ReadAllText(Application.dataPath + "/save.txt");
        saveFile = JsonUtility.FromJson<SaveFile>(json);
        Save();  
    }

    public void Save()
    {
        string save= JsonUtility.ToJson(saveFile,true);

        File.WriteAllText(Application.dataPath+"/save.txt",save);
    }
}
