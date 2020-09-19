using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public static class Enums
{
    public enum RoomCategory
    {
        NORMAL,
        SPECIAL,
        REWARD,
        BOSS,
        CONNECTOR,
        HUB,
        SECRET,
        ENTRANCE,
        EXIT
    }

    public enum RoomNormalSubCategory
    {
        COMBAT,
        TRAP
    }

    public enum RoomBossSubCategory
    {
        FLOOR_BOSS,
        MINI_BOSS
    }

    public enum RoomSpecialSubCategory
    {
        UNSPECIFIED_SPECIAL,
        STANDARD_SHOP,
        WEIRD_SHOP,
        MAIN_STORY,
        NPC_STORY,
        CATACOMBS_BRIDGE_ROOM
    }

    public enum ValidTilesets
    {
        GUNGEON,
        CASTLEGEON,
        SEWERGEON,
        CATHEDRALGEON,
        MINEGEON,
        CATACOMBGEON,
        FORGEGEON,
        HELLGEON,
        SPACEGEON,
        PHOBOSGEON512,
        WESTGEON,
        OFFICEGEON,
        BELLYGEON,
        JUNGLEGEON,
        FINALGEON,
        RATGEON
    }

    public enum CategoryType
    {
        Normal, 
        Special,
        Boss,
        Secret
    }
    public static T GetEnumValue<T>(string val) where T : Enum
    {
        //Debug.Log($"Get enum value: \"{val}\"");
        return (T)Enum.Parse(typeof(T), val.ToUpper());
    }
}
