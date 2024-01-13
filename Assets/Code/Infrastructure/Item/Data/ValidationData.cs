using System.Collections.Generic;

namespace Ddd.Infrastructure
{
    public static class ValidationData
    {
        public static int OnValidateMaxLeavel(int maxLeavel)
        {
            return 1 > maxLeavel ? 1 : maxLeavel;
        }

        public static List<T> OnValidateListImprovementSpecification<T>(List<T> ImprovementSpecificationsTTX, int maxLeavel)
            where T : struct
        {
            var newListIS = ImprovementSpecificationsTTX;
            var lengthLastListIS = ImprovementSpecificationsTTX.Count;

            if (maxLeavel == 1)
                return null;
            else if (maxLeavel > 1 && lengthLastListIS < maxLeavel - 1)
                for (; lengthLastListIS + 1 < maxLeavel; lengthLastListIS++)
                    newListIS.Add(new T());
            else
                newListIS.RemoveRange(maxLeavel - 1, lengthLastListIS - maxLeavel + 1);

            return newListIS;
        }
    }
}