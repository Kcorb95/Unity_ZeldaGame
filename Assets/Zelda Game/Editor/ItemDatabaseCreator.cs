using UnityEngine;
using UnityEditor;
using System.Collections;

public class ItemDatabaseCreator : MonoBehaviour 
{
    [MenuItem( "Zelda Game/Databases/Create Item Database" )]
    public static void CreateItemDatabase()
    {
        ItemDatabase newDatabase = ScriptableObject.CreateInstance<ItemDatabase>();
        AssetDatabase.CreateAsset( newDatabase, "Assets/ItemDatabase.asset" );
        AssetDatabase.Refresh();
    }
}
