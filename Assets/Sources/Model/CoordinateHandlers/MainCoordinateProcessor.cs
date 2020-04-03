﻿using System.Collections.Generic;
using UnityEngine;

namespace Craft_TZ.Model.CoordinateHandlers
{
    public interface IMainCoordinateProcessor
    {
        bool CoordinatesAreWithinTiles(Vector2 coordinate, IReadOnlyCollection<Vector2> tilesCoordinates);
        bool PlayerChipCollisionWithOtherObject(Vector2 playerChipCoordinate, Vector2 otherObjectCoordinate);
    }

    internal sealed class MainCoordinateProcessor : IMainCoordinateProcessor
    {
        private readonly IFigureCoordinateProcessor tileCoordinateProcessor;
        private readonly IFigureCoordinateProcessor playerChipCoordinateProcessor;

        public MainCoordinateProcessor(IFigureCoordinateProcessor tileCoordinateProcessor, IFigureCoordinateProcessor playerChipCoordinateProcessor)
        {
            this.tileCoordinateProcessor = tileCoordinateProcessor;
            this.playerChipCoordinateProcessor = playerChipCoordinateProcessor;
        }

        public bool CoordinatesAreWithinTiles(Vector2 otherCoordinate, IReadOnlyCollection<Vector2> tilesCoordinates)
        {
            foreach (var tileCenterCoordinate in tilesCoordinates)
            {
                if (tileCoordinateProcessor.ContainsCoordinates(tileCenterCoordinate, otherCoordinate))
                    return true;
            }
            return false;
        }

        public bool PlayerChipCollisionWithOtherObject(Vector2 playerChipCoordinate, Vector2 otherObjectCoordinate)
        {
            return playerChipCoordinateProcessor.ContainsCoordinates(playerChipCoordinate, otherObjectCoordinate);
        }
    }
}