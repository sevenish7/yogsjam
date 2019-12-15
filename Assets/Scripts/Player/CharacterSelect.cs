using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterSelection
{
    public static CharacterType[] Players = new CharacterType[]
    {
        CharacterType.NONE,
        CharacterType.NONE,
        CharacterType.NONE,
        CharacterType.NONE,
    };

    public static void ResetCharacterData()
    {
        for(int i = 0; i < Players.Length; i++)
        {
            Players[i] = CharacterType.NONE;
        }
    }
}

public enum CharacterType
{
    NONE,
    Simon,
    Lewis,
    Duncan,
    Ben,
    Zylus,
    Rythian,
    Tom,
    Bouphe,
    Lydia,
    Barry,
    Spiff,
    Zoey,
    Pedguin,
}
