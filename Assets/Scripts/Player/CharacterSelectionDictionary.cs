using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

public class CharacterSelectionDictionary : MonoBehaviour
{
    [OdinSerialize] Dictionary<SelectedCharacter, GameObject> CharacterPrefabLookup = new Dictionary<SelectedCharacter, GameObject>();
}
