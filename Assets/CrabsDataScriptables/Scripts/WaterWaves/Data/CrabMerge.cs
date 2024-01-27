using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrabWorld.Waters
{
    public class CrabMerge:MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public string MergeWaves(List<string> stringsToConcatenate)
        {
            StringBuilder resultBuilder = new StringBuilder();
            foreach (var str in stringsToConcatenate)
            {
                resultBuilder.Append(str);
            }

            return resultBuilder.ToString();
        }
    }
}