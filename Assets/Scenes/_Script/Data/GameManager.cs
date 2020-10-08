using System.Collections.Generic;
public class GameManager
{
    private List<DataChar> characterList;
    private List<Equip> equipmentList; // Main1, Sub1, Armor3
    private int[,] formation;
    private int key;

    private GameManager()
    {
        Load();
    }

    private static class LazyHolder
    {
        public static readonly GameManager INSTANCE = new GameManager();
    }


    public static GameManager GetInstatnce()
    {
        return LazyHolder.INSTANCE;
    }

    private void Load()
    {
        characterList = DataManager.BinaryDeserialize<List<DataChar>>(DataFilePath.DataCharacter);
        if (characterList == default)
        {
            characterList = new List<DataChar>();
        }
        equipmentList = DataManager.BinaryDeserialize<List<Equip>>(DataFilePath.Equipment);
        if (equipmentList == default)
        {
            equipmentList = new List<Equip>();
        }
        formation = DataManager.BinaryDeserialize<int[,]>(DataFilePath.Formation);
        if (formation == default)
        {
            formation = new int[3, 3] { { 0, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        }
        key = DataManager.BinaryDeserialize<int>(DataFilePath.Key);
    }

    public void Save()
    {
        DataManager.BinarySerialize<List<DataChar>>(characterList, DataFilePath.DataCharacter);
        DataManager.BinarySerialize<List<Equip>>(equipmentList, DataFilePath.Equipment);
        DataManager.BinarySerialize<int[,]>(formation, DataFilePath.Formation);
        DataManager.BinarySerialize<int>(key, DataFilePath.Key);
    }

    public void DataReset()
    {
        characterList = new List<DataChar>();
        equipmentList = new List<Equip>();
        formation = new int[3, 3] { { 0, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        key = default;
        Save();
    }

    public List<DataChar> GetCharacterList()
    {
        return characterList;
    }


    public void UpdateCharacterList(List<DataChar> newDataChar)
    {
        characterList = newDataChar;
        Save();
    }

    public void AddCharacter(DataChar character)
    {
        characterList.Add(character);
        Save();
    }

    public int NumOfCharacter()
    {
        return characterList.Count;
    }

    public int[,] GetFormation()
    {
        return formation;
    }

    public void SetFormation(int[,] newFormation)
    {
        formation = newFormation;
    }

    public void SetCharacterOnFormation(int x, int y, DataChar character)
    {
        formation[x, y] = characterList.IndexOf(character);
        Save();
    }

    public void RemoveCharacterOnFormation(int x, int y)
    {
        formation[x, y] = -1;
    }

    public List<Equip> GetEquipment()
    {
        return equipmentList;
    }

    public void AddEquipment(Equip equipment)
    {
        equipmentList.Add(equipment);
        Save();
    }

    public void SetEquipment(List<Equip> newEquipment)
    {
        equipmentList = newEquipment;
    }

    public void AddKey(int key)
    {
        this.key += key;
        Save();
    }

    public bool UseKey(int key)
    {
        if(this.key >= key)
        {
            this.key -= key;
            Save();
            return true;
        }
        else
        {
            Save();
            return false;
        }
    }

    private int GetKey()
    {
        return key;
    }

    private void SetKey(int newKey)
    {
        key = newKey;
    }
}
