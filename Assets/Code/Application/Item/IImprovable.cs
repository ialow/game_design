namespace Ddd.Application
{
    public interface IImprovable
    {
        public int CurrentLeavel { get; }
        public int MaxLeavel { get; }

        public void UpLevels();
    }
}