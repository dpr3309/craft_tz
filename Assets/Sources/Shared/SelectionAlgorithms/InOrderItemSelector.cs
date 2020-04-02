using System.Collections.Generic;

namespace Craft_TZ.Shared.SelectionAlgorithms
{
    /// <summary>
    /// Инкапсулирает алгорим выборки 1 элеменат из контрольной коллекции, каждые n итераций, по порядку, с инкременом счетчика
    /// 
    /// </summary>
    public sealed class InOrderItemSelector : ISelector
    {
        private int iterationNumberSeletedForGeneration;
        private int counter;

        private readonly int maxIterationCount;

        public InOrderItemSelector(int maxIterationCount)
        {
            this.maxIterationCount = maxIterationCount;
            iterationNumberSeletedForGeneration = -1;
        }

        /// <summary>
        /// каждые maxIterationCount, из controllCollection выбирается 1 элемент, индекс которого в группе из maxIterationCount элементов, равен iterationNumberSeletedForGeneration
        /// т.е. из первой группы размером в maxIterationCount, выберется первый элемент, из второй группы - второй элемент, и т.д.
        /// </summary>
        /// <returns>Выбранный элемент/ коллекция выбранных элементов.</returns>
        /// <param name="controllCollection">Controll collection - коллекция, из которой производится выборка</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public IEnumerable<T> SelectItems<T>(IEnumerable<T> controllCollection)
        {
            foreach (var item in controllCollection)
            {
                if (counter == 0)
                    iterationNumberSeletedForGeneration = (++iterationNumberSeletedForGeneration < maxIterationCount) ? iterationNumberSeletedForGeneration : 0;


                if (counter == iterationNumberSeletedForGeneration)
                    yield return item;

                counter = (++counter < maxIterationCount) ? counter : 0;
            }
        }
    }
}