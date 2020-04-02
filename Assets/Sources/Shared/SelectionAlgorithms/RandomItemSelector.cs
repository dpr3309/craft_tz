using System.Collections.Generic;
using UnityEngine;

namespace Craft_TZ.Shared.SelectionAlgorithms
{
    /// <summary>
    /// Инкапсулирает алгорим выборки одного случайного элеменат из контрольной коллекции, каждые n итераций
    /// </summary>
    public sealed class RandomItemSelector : ISelector
    {
        private int iterationNumberSeletedForGeneration;
        private int counter;

        private readonly int maxIterationCount;

        public RandomItemSelector(int maxIterationCount)
        {
            this.maxIterationCount = maxIterationCount;
        }

        /// <summary>
        /// каждые maxIterationCount, из controllCollection выбирается 1 рандомный элемент
        /// </summary>
        /// <returns>Выбранный элемент/ коллекция выбранных элементов.</returns>
        /// <param name="controllCollection">Controll collection - коллекция, из которой производится выборка</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public IEnumerable<T> SelectItems<T>(IEnumerable<T> controllCollection)
        {
            foreach (var item in controllCollection)
            {
                if (counter == 0)
                    iterationNumberSeletedForGeneration = Random.Range(0, maxIterationCount);

                if (counter == iterationNumberSeletedForGeneration)
                    yield return item;

                counter = (++counter < maxIterationCount) ? counter : 0;
            }
        }
    }
}