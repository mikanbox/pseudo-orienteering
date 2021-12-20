using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stage 
{
    public int _total_distance;
    public List<Section> _sections;

    public MappingStagetoTile _mapping_tile_mapcode;
    public Tile _baseGroundTile;

    public Geology _geology;
    public int _steeper_percentage;
    
    public Stage (int terrainId) 
    {
        //サンプルの geology を生成
        _geology = GameGlobalClasses._gameglobal._terrains.terrains[terrainId].geology;
        _baseGroundTile = Resources.Load<Tile>("Objects/TileMap/tilemap_0");
        // _steeper_percentage = 5;
        

        _mapping_tile_mapcode = new MappingStagetoTile();
        foreach(KeyValuePair<MapCode, List<string>> kvp in _geology.MapCode_Sprite_Mappings) {
            List<Tile> pp = new List<Tile>();
            foreach (var path in kvp.Value){
                pp.Add(Resources.Load<Tile>("Objects/TileMap/" + path));
            }
            _mapping_tile_mapcode.Mappings.Add(kvp.Key,pp);
        }


        List<Tile> p = new List<Tile>();        
        p.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p.Add(Resources.Load<Tile>("Objects/TileMap/Post"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.Post,p);

        p = new List<Tile>();
        p.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_5"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.PostWithRane,p);
        
        p = new List<Tile>();
        p.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_4"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.Rane,p);


        RegisterStartMapTips();
        RegisterGoalMapTips();

        _sections = new List<Section>();
        _sections.Add(new Section(GameGlobalClasses._gameglobal._cups.cups[GameMgr._gamemgr._selectedcupid].distance));
        Total_distance();

    }

    private void Total_distance()
    {
        int t = 0;
        for (int i = 0; i < _sections.Count; i++) 
            t += _sections[i]._distance;
        _total_distance = t;
        // _total_distance = GameGlobalClasses._gameglobal._cups.cups[GameMgr._gamemgr._selectedcupid].distance;
    }

    private void RegisterStartMapTips()
    {
        List<Tile> p = new List<Tile>();
        p.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_38"));
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_32"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.Start1,p);

        p = new List<Tile>();
        p.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_39"));
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_33"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.Start2,p);

        p = new List<Tile>();
        p.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_40"));
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_34"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.Start3,p);

        p = new List<Tile>();
        p.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_41"));
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_35"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.Start4,p);
    }

    private void RegisterGoalMapTips()
    {
        List<Tile> p3 = new List<Tile>();
        p3.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_15"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_7"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.Goal1,p3);
        p3 = new List<Tile>();
        p3.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_14"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.Goal2,p3);
        p3 = new List<Tile>();
        p3.Add(_mapping_tile_mapcode.Mappings[MapCode.Base][0]);
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_6"));
        _mapping_tile_mapcode.Mappings.Add(MapCode.Goal3,p3);
    }
}

