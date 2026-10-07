using System;

[Serializable]
public class CharacterSelectionResponse
{
    public int count;
    public CharacterSelectionData[] characters;
}

[Serializable]
public class CharacterSelectionData
{
    public int id;

    public int userId;

    public string name;

    public string gender;

    public string face;

    public string hair;

    public string skin;

    public string faction;

    public string subClass;

    public int characterClassId;

    public string characterClassName;

    public int rankId;

    public string rankName;

    public int level;

    public int experience;

    public int money;

    public int health;

    public int energy;

    public string createdAt;
}