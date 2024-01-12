using System;
using System.Collections.Generic;

namespace Ddd.Infrastructure
{
    public class PoolObjects<T>
    {
        public List<T> poolAllObj { get; private set; } = new List<T>();
        private Queue<T> poolDisabledObj = new Queue<T>();

        private Func<T> generationObj;
        private Action<T> returnInActive;
        private Action<T, int> returnActive;

        public PoolObjects(Func<T> GenerationObj, Action<T> ReturnInActive, Action<T, int> ReturnActive, int countObj)
        {
            generationObj += GenerationObj;
            returnInActive += ReturnInActive;
            returnActive += ReturnActive;

            GenerationStartingObj(countObj);
        }

        private void GenerationStartingObj(int countObjs)
        {
            for (var i = 0; i < countObjs; i++)
            {
                var obj = generationObj();
                poolAllObj.Add(obj);
                ReturnInActive(obj);
            }
        }

        public void ReturnInActive(T obj)
        {
            returnInActive(obj);
            poolDisabledObj.Enqueue(obj);
        }

        public void ReturnActive(int countObjs)
        {
            for (var i = 0; i < countObjs; i++)
                returnActive(poolDisabledObj.Count > 0 ? poolDisabledObj.Dequeue() : generationObj(), i);
        }
    }
}