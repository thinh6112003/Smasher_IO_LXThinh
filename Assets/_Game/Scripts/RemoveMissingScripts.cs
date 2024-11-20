using UnityEngine;
using UnityEditor;

public class RemoveMissingScripts : MonoBehaviour
{
    [ContextMenu("Remove Missing Scripts in Children (Editor Only)")]
    private void RemoveAllMissingScriptsInEditor()
    {
        int totalRemovedCount = 0;


        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            // Use UnityEditor's method to remove MonoBehaviours with missing scripts
            int removedCount = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(child.gameObject);
            totalRemovedCount += removedCount;
        }

        Debug.Log($"A total of {totalRemovedCount} missing scripts were removed from {gameObject.name} and its child objects.");
    }
}