using UnityEngine;
using System;

namespace TiledJsonUtility
{
    [Serializable]
    public class TiledMap
    {
        public static TiledMap ReadMap(string fileName)
        {
            string assetName = fileName.ToLower();
            if (assetName.EndsWith(".tmx"))
            {
                throw new Exception("This class does not read XML - try using TiledSharp instead");
            }
            else if (assetName.EndsWith(".json"))
            {
                assetName = fileName.Substring(0, fileName.Length - 4);
            }
            TextAsset textasset = Resources.Load(assetName) as TextAsset;
            return JsonUtility.FromJson<TiledMap>(textasset.text);
        }

        public string tiledversion;
        public int version;
        public string backgroundcolor;
        public string orientation;
        public string renderorder;
        public int nextobjectid;
        public bool infinite;
        public string type;
        public int height;
        public int width;
        public int tileheight;
        public int tilewidth;
        public TiledLayer[] layers;
        public TiledTileset[] tilesets;
        public MapProperties properties;
    }
}