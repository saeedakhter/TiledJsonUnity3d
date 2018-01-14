namespace TiledJsonUtility
{
    public struct TiledLayerTile
    {
        private uint GidWithFlags;
        public int X { private set; get; }
        public int Y { private set; get; }

        private const uint DIAG_FLIP = 0x20000000;
        private const uint VERT_FLIP = 0x40000000;
        private const uint HORZ_FLIP = 0x80000000;
        private const uint ALL_THREE = 0xE0000000;

        public int Gid
        {
            get
            {
                // remove all three flags
                return (int)((GidWithFlags ^ ALL_THREE) - ALL_THREE);
            }
        }

        public bool IsFlippedDiag
        {
            get
            {
                return (GidWithFlags & DIAG_FLIP) != 0;
            }
        }

        public bool IsFlippedVert
        {
            get
            {
                return (GidWithFlags & VERT_FLIP) != 0;
            }
        }
        public bool IsFlippedHorz
        {
            get
            {
                return (GidWithFlags & HORZ_FLIP) != 0;
            }
        }

        public TiledLayerTile(int X, int Y, uint GidWithFlags)
        {
            this.X = X;
            this.Y = Y;
            this.GidWithFlags = GidWithFlags;
        }
    }
}