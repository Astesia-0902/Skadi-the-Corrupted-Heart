using UnityEngine;

namespace Tile
{
    public enum TileType
    {
        PlaceableLowland, PlaceableHighland, Unplaceable
    }
    public class Tile : MonoBehaviour
    {
        public TileType mapCubeTileType;
    }
}