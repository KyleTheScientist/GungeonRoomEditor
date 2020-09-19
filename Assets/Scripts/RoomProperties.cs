using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using RoomCategory = Enums.RoomCategory;
using RoomNormalSubCategory = Enums.RoomNormalSubCategory;
using RoomBossSubCategory = Enums.RoomBossSubCategory;
using RoomSpecialSubCategory = Enums.RoomSpecialSubCategory;
using RoomData = ImportExport.RoomData;
public class RoomProperties
{
    public RoomCategory category;
    public RoomNormalSubCategory normalSubCategory;
    public RoomBossSubCategory bossSubCategory;
    public RoomSpecialSubCategory specialSubCategory;
    public bool 
        shuffleReinforcementPositions = false, 
        doFloorDecoration = true, 
        doWallDecoration = true, 
        doLighting = true;
    public float weight = 1f;

    public Dictionary<string, bool> validTilesets = new Dictionary<string, bool>();

    public RoomProperties()
    {
        foreach (var value in Enum.GetValues(typeof(Enums.ValidTilesets)))
            validTilesets.Add(value.ToString(), false);
    }


    public void CollectRoomProperties(ref ImportExport.RoomData data)
    {
        data.category = category.ToString();
        data.normalSubCategory = normalSubCategory.ToString();
        data.bossSubCategory = bossSubCategory.ToString();
        data.specialSubCategory = specialSubCategory.ToString();

        data.weight = this.weight;
        data.shuffleReinforcementPositions = shuffleReinforcementPositions;
        data.doFloorDecoration = doFloorDecoration;
        data.doWallDecoration = doWallDecoration;
        data.doLighting = doLighting;

        List<string> floors = new List<string>();
        foreach (var floor in validTilesets)
        {
            if (floor.Value)
                floors.Add(floor.Key);
        }
        data.floors = floors.ToArray();
    }

    public void ImportRoomProperties(RoomData data)
    {
        if (!string.IsNullOrEmpty(data.category))
            this.category = Enums.GetEnumValue<RoomCategory>(data.category);
        if (!string.IsNullOrEmpty(data.normalSubCategory))
            this.normalSubCategory = Enums.GetEnumValue<RoomNormalSubCategory>(data.normalSubCategory);
        if (!string.IsNullOrEmpty(data.specialSubCategory))
            this.specialSubCategory = Enums.GetEnumValue<RoomSpecialSubCategory>(data.specialSubCategory);
        if (!string.IsNullOrEmpty(data.bossSubCategory))
            this.bossSubCategory = Enums.GetEnumValue<RoomBossSubCategory>(data.bossSubCategory);

        if (data.floors != null)
        {
            foreach (var floor in data.floors)
            {
                if (validTilesets.ContainsKey(floor))
                    validTilesets[floor] = true;
            }
        }

        this.weight = data.weight;
        this.shuffleReinforcementPositions = data.shuffleReinforcementPositions;
        this.doFloorDecoration = data.doFloorDecoration;
        this.doWallDecoration = data.doWallDecoration;
        this.doLighting = data.doLighting;
    }
}
