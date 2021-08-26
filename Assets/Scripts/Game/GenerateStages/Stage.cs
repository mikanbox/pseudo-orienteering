using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stage 
{
    public int _total_distance;
    public List<Section> _sections;

    public MappingStagetoTile _mappings;

    public Geology _geology;
    public int _steeper_percentage;
    
    public Stage () 
    {
        _geology = new Geology();
        _steeper_percentage = 5;
        _geology.MaptipRatio.Add(MapCode.Open,40);
        _geology.MaptipRatio.Add(MapCode.Forest,10);
        _geology.MaptipRatio.Add(MapCode.WaterCource,5);


        _mappings = new MappingStagetoTile();
        List<Tile> p4 = new List<Tile>();
        p4.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        _mappings.Mappings.Add(MapCode.Open,p4);


        List<Tile> p3 = new List<Tile>();
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_29"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_21"));
        _mappings.Mappings.Add(MapCode.Forest,p3);

        List<Tile> p2 = new List<Tile>();
        p2.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_11"));
        _mappings.Mappings.Add(MapCode.WaterCource,p2);

        List<Tile> p = new List<Tile>();
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p.Add(Resources.Load<Tile>("Objects/TileMap/Post"));
        _mappings.Mappings.Add(MapCode.Post,p);

        p = new List<Tile>();
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_5"));
        _mappings.Mappings.Add(MapCode.PostWithRane,p);
        
        p = new List<Tile>();
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_4"));
        _mappings.Mappings.Add(MapCode.Rane,p);


        RegisterStartMapTips();
        RegisterGoalMapTips();



        _sections = new List<Section>();
        _sections.Add(new Section(1));
        _sections.Add(new Section(2));
        _sections.Add(new Section(1));
        Total_distance();

    }

    private void Total_distance()
    {
        int t = 0;
        for (int i = 0; i < _sections.Count; i++) 
            t += _sections[i]._distance;
        _total_distance = t;
    }

    private void RegisterStartMapTips()
    {
        List<Tile> p3 = new List<Tile>();
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_38"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_32"));
        _mappings.Mappings.Add(MapCode.Start1,p3);

        p3 = new List<Tile>();
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_39"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_33"));
        _mappings.Mappings.Add(MapCode.Start2,p3);

        p3 = new List<Tile>();
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_40"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_34"));
        _mappings.Mappings.Add(MapCode.Start3,p3);

        p3 = new List<Tile>();
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_41"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_35"));
        _mappings.Mappings.Add(MapCode.Start4,p3);
    }

    private void RegisterGoalMapTips()
    {
        List<Tile> p3 = new List<Tile>();
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_15"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_7"));
        _mappings.Mappings.Add(MapCode.Goal1,p3);
        p3 = new List<Tile>();
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_14"));
        _mappings.Mappings.Add(MapCode.Goal2,p3);
        p3 = new List<Tile>();
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_8"));
        p3.Add(Resources.Load<Tile>("Objects/TileMap/tilemap_6"));
        _mappings.Mappings.Add(MapCode.Goal3,p3);
    }
}

