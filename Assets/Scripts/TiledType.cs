using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TiledJsonUtility
{
    public enum TiledFiletype {
        Tileset,
        Map,
        Invalid
    }

    [Serializable]
    public class TiledType
    {
        public const string TILESET = "tileset";
        public const string MAP = "map";

        public static TiledFiletype GetAssetType(string fileName)
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
            string type = JsonUtility.FromJson<TiledType>(textasset.text).type;
            if(TILESET.Equals(type))
            {
                return TiledFiletype.Tileset;
            }
            else if(MAP.Equals(type))
            {
                return TiledFiletype.Map;
            }
            return TiledFiletype.Invalid;
        }

        public string type;
    }
}
