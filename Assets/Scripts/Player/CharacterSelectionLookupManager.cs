using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionLookupManager : MonoBehaviour
{
    [SerializeField, Sirenix.OdinInspector.InlineEditor] 
    private CharacterSelectionDictionary lookup;

    public static CharacterSelectionLookupManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null || Instance != this)
        {
            Instance = this;
        }
    }

    public GameObject GetPrefabForCharacterType(CharacterType character)
    {
        return lookup.CharacterPrefabLookup[character];
    }
}
