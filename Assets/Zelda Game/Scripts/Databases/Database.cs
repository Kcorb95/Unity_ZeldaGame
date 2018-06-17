using UnityEngine;

public class Database
{
    private static ItemDatabase m_ItemDatabase;

    public static ItemDatabase Item {
        get {
            if (m_ItemDatabase == null)
                m_ItemDatabase = Resources.Load<ItemDatabase>("Databases/ItemDatabase"); //Our item database used to store our items for use in game

            return m_ItemDatabase;
        }
    }
}