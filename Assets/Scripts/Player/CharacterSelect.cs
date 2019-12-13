using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterSelection
{
    public static SelectedCharacter Player1;
    public static SelectedCharacter Player2;
    public static SelectedCharacter Player3;
    public static SelectedCharacter Player4;

}

public enum SelectedCharacter
{
    NONE,
    SIMON,
    LEWIS,
    DUNCAN,
    ZYLUS,
    RYTHIAN,
}
