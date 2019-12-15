using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName="CharacterLookup", menuName="Yogs/Character Lookup")]
public class CharacterSelectionDictionary : SerializedScriptableObject
{
    [OdinSerialize] Dictionary<CharacterType, GameObject> characterPrefabLookup = new Dictionary<CharacterType, GameObject>();
    public Dictionary<CharacterType, GameObject> CharacterPrefabLookup { get { return characterPrefabLookup; } }

    #if UNITY_EDITOR

    [Button(30)]
    private void AddKeysForAllEnumVals()
    {
        foreach(var val in System.Enum.GetValues(typeof(CharacterType)))
        {
            CharacterType type = (CharacterType)val;
            if(!characterPrefabLookup.ContainsKey(type))
            {
                characterPrefabLookup.Add(type, null);
            }
        }
    }

    #endif
}
