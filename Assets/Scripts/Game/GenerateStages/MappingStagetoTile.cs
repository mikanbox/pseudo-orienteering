using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MappingStagetoTile {
    public Dictionary<MapCode,List<Tile>> Mappings;
    public MappingStagetoTile ()
    {
        Mappings = new Dictionary<MapCode,List<Tile>>();
    }
}