
namespace ProfitBaseAPILibraly.Classes
{
    public class CastomStatus
    {
        public int Id { get; }
        public string Name { get; }

        public CastomStatus(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}
