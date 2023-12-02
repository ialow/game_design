using Domain;

namespace Application
{
    public class PatternGenerator
    {
        private IPatternGenerator patternGenerator;

        public PatternGenerator(IPatternGenerator patternGenerator)
        {
            this.patternGenerator = patternGenerator;
        }

        public void SpawnPlatform(int directionZ = 1, int directionX = 0)
        {
            patternGenerator.SpawnPlatform(directionZ, directionX);
        }
    }
}