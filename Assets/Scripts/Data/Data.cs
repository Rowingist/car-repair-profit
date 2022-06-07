using System;
using UnityEngine;

public class Data : MonoBehaviour
{
    protected const string _dataKeyName = "CarRepairProfit";
    protected SaveOptions _options = new SaveOptions();

    public void Save()
    {
        string json = JsonUtility.ToJson(_options);
        PlayerPrefs.SetString(_dataKeyName, json);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_dataKeyName) == false)
        {
            _options = new SaveOptions();
            Save();
        }
        else
            _options = JsonUtility.FromJson<SaveOptions>(PlayerPrefs.GetString(_dataKeyName));
    }

    [ContextMenu("RemoveSevedData")]
    public void RemoveData()
    {
        PlayerPrefs.DeleteKey(_dataKeyName);
        _options = new SaveOptions();
    }

    public void SetLevelIndex(int index)
    {
        _options.LevelNumber = index;
    }

    public void SetDateRegistration(DateTime date)
    {
        _options.RegistrationDate = date.ToString();
    }

    public void SetLastLoginDate(DateTime date)
    {
        _options.LastLoginDate = date.ToString();
    }

    public void SetCurrentSoft(int value)
    {
        _options.CurrentSoft = value;
    }

    public void AddSession()
    {
        _options.SessionCount++;
    }

    public void SetOppenedServiceZones(int count)
    {
        _options.OpenedServiceZones = count;
    }

    public void SetClosedMoneyDropZones(int count)
    {
        _options.OpenedDropZones = count;
    }

    public string GetDataKeyName()
    {
        return _dataKeyName;
    }

    public int GetLevelIndex()
    {
        return _options.LevelNumber;
    }

    public int GetSessionCount()
    {
        return _options.SessionCount;
    }

    public void AddDisplayedLevelNumber()
    {
        _options.DisplayedLevelNumber++;
    }

    public int GetNumberDaysAfterRegistration()
    {
        return (DateTime.Parse(_options.LastLoginDate) - DateTime.Parse(_options.RegistrationDate)).Days;
    }

    public int GetDisplayedLevelNumber()
    {
        return _options.DisplayedLevelNumber;
    }

    public string GetRegistrationDate()
    {
        return _options.RegistrationDate;
    }

    public int GetCurrentSoft()
    {
        return _options.CurrentSoft;
    }

    public int GetOpennedServiceZones()
    {
        return _options.OpenedServiceZones;
    }

    public int GetClosedMoneyDropZones()
    {
        return _options.OpenedDropZones;
    }
}

[Serializable]
public class SaveOptions
{
    public int LevelNumber;
    public int SessionCount;
    public string LastLoginDate;
    public string RegistrationDate;
    public int DisplayedLevelNumber = 1;
    public int CurrentSoft;
    public int OpenedServiceZones;
    public int OpenedDropZones;
    public int OpenRacks;
    public int WheelsOnRack;
    public int EnginesOnRack;
    public int PaintOnRack;
}

