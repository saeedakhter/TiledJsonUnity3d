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
        public TiledProperty[] properties;
        public int spacing;
        public TiledTerrain[] terrains;
        public int tilecount;
        public int tileheight;
        public TiledTile[] tiles;
        public TiledLayer[] layers;
        public int tilewidth;
        public string type;

        public void PrintSummary()
        {
            if(TiledType.TILESET.Equals(type))
            {
                int terrainCount = terrains == null ? 0 : terrains.Length;
                Debug.LogFormat("Tileset: {0} (tiles are {1}px by {2}px) ({3} tiles) ({4} terrains)", name, tilewidth, tileheight, tilecount, terrainCount);
            }
            else
            {
                Debug.LogError("Not a tileset, json object is of type: " + type);
            }
        }
    }
}