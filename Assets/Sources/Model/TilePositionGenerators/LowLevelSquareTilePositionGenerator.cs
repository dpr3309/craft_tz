using UnityEngine;

namespace Craft_TZ.Model.SquareTile
{
    internal class LowLevelSquareTilePositionGenerator : ALevelSquareTilePositionGenerator
    {
        private readonly Vector2Int[] horizontalFaceCellIndices = { new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2) };
        protected override Vector2Int[] TopFaceCellIndices => horizontalFaceCellIndices;

        private readonly Vector2Int[] verticalFaceCellIndices = { new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(2, 2) };
        protected override Vector2Int[] RightFaceCellIndices => verticalFaceCellIndices;

        private readonly Vector2Int generatedAreaSize = new Vector2Int(3, 3);
        protected override Vector2Int GeneratedAreaSize => generatedAreaSize;

        public LowLevelSquareTilePositionGenerator(float tileSize)
            : base(tileSize)
        {
        }
    }
}