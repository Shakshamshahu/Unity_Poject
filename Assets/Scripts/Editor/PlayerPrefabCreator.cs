using UnityEngine;
using UnityEditor;
using System.IO;

public class PlayerPrefabCreator
{
    [MenuItem("Tools/Create Player Prefab")]
    public static void CreatePlayerPrefab()
    {
        // Create Cube
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.name = "Player";

        // Add CharacterController
        if (player.GetComponent<CharacterController>() == null)
            player.AddComponent<CharacterController>();

        // Add PlayerController script
        var script = typeof(PlayerController);
        if (script != null)
            player.AddComponent(script);

        // Create Prefabs folder if it doesn't exist
        string prefabFolder = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(prefabFolder))
            AssetDatabase.CreateFolder("Assets", "Prefabs");

        // Save as prefab
        string prefabPath = Path.Combine(prefabFolder, "Player.prefab");
        PrefabUtility.SaveAsPrefabAsset(player, prefabPath);

        // Clean up
        GameObject.DestroyImmediate(player);

        Debug.Log("Player prefab created at " + prefabPath);
    }
} 