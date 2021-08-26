using System.Collections;
using System.Collections.Generic;
public class Maptip 
{
    // public int _mapcode;
    public int _height;
    public bool _isMovable;
    public MapCode _mapcode;

        public Maptip(MapCode mapcode, int height, bool isMovable) {
        _mapcode = mapcode;
        _height=height;
        _isMovable = isMovable;
    }
    

}


