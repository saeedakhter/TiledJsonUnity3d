using System;

namespace TiledJsonUtility
{
    [Serializable]
    public class TiledObject
    {
        public int id = -1;
        public int gid = -1;
        public string name;
        public bool ellipse;
        public int height;
        public int width;
        public bool point;
        public TiledCoordinate[] polygon;
        public TiledCoordinate[] polyline;
        public TiledProperty[] properties;
        public float rotation;
        public string text;
        public string type;
        public bool visible;
        public int x;
        public int y;
    }
}