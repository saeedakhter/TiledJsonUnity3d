using System;

namespace TiledJsonUtility
{
    [Serializable]
    public class TiledTile
    {
        public int[] terrain;
        public TiledProperty[] properties;
    }
}