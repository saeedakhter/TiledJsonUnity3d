using UnityEngine;
using TiledJsonUtility;

public class SampleUsage : MonoBehaviour
{
    public string TextAssetName;

    public void Start()
    {
        TiledMap map = TiledMap.ReadMap(TextAssetName);
        foreach (TiledLayer layer in map.layers)
        {
            TiledLayerTile[] tiles = layer.GetTiles();
            if (tiles != null)
            {
                foreach (TiledLayerTile tile in tiles)
                {
                    Debug.LogFormat("Tile({0},{1}) = {2} H={3} V={4} D={5}", tile.X, tile.Y, tile.Gid, tile.IsFlippedHorz, tile.IsFlippedVert, tile.IsFlippedDiag);
                }
            }
        }
    }
}
