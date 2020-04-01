using UnityEngine;

namespace Craft_TZ.Model.SquareTile
{
    internal class MiddleLevelSquareTilePositionGenerator : ALevelSquareTilePositionGenerator
    {
        private readonly Vector2Int[] horizontalFaceCellIndices = { new Vector2Int(0, 1), new Vector2Int(1, 1) };
        protected override Vector2Int[] TopFaceCellIndices => horizontalFaceCellIndices;

        private readonly Vector2Int[] verticalFaceCellIndices = { new Vector2Int(1, 0), new Vector2Int(1, 1) };
        protected override Vector2Int[] RightFaceCellIndices => verticalFaceCellIndices;

        private readonly Vector2Int generatedAreaSize = new Vector2Int(2, 2);
        protected override Vector2Int GeneratedAreaSize => generatedAreaSize;

        public MiddleLevelSquareTilePositionGenerator(int tileSize)
            : base(tileSize)
        {
        }
    }
}