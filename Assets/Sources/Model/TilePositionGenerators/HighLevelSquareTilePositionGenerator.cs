using UnityEngine;

namespace Craft_TZ.Model.SquareTile
{
    internal class HighLevelSquareTilePositionGenerator : ALevelSquareTilePositionGenerator
    {
        private readonly Vector2Int[] faceCellIndeces = { new Vector2Int(0, 0) };

        protected override Vector2Int[] TopFaceCellIndices => faceCellIndeces;

        protected override Vector2Int[] RightFaceCellIndices => faceCellIndeces;

        private readonly Vector2Int generatedAreaSize = new Vector2Int(1, 1);
        protected override Vector2Int GeneratedAreaSize => generatedAreaSize;

        public HighLevelSquareTilePositionGenerator(int tileSize)
            : base(tileSize)
        {
        }
    }
}