using UnityEngine;
using System;

namespace TiledJsonUtility
{
    [Serializable]
    public class TiledTileset
    {
        public static TiledTileset ReadTileset(string fileName)
        {
            string assetName = fileName.ToLower();
            if (assetName.EndsWith(".tsx"))
            {
                throw new Exception("This class does not read XML - try using TiledSharp instead");
            }
            else if (assetName.EndsWith(".json"))
            {
                assetName = fileName.Substring(0, fileName.Length - 4);
            }
            TextAsset textasset = Resources.Load(assetName) as TextAsset;
            return JsonUtility.FromJson<TiledTileset>(textasset.text);
        }

        public string source;
        public int columns;
        public int firstgid;
        public string grid;
        public string image;
        public int imagewidth;
        public int imageheight;
        public int margin;
        public string name;
        TilesetProperties properties;
        public int spacing;
        public TiledTerrain[] terrains;
        int tilecount;
        int tileheight;
        object tileoffset;
        TilesetProperties tileproperties;
        public TiledTile[] tiles;
        int tilewidth;
        string type;
    }
}