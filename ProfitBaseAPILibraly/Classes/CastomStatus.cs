namespace ProfitBaseAPILibraly.Classes
{
    public class CastomStatus
    {
        public int Id { get; }

        public string Name { get; }

        public string BaseStatus { get; set; }

        public CastomStatus(int id, string name)
        {
            Name = name;
            Id = id;
        }

        public CastomStatus(int id, string name, string baseStatus) : this(id, name) => BaseStatus = baseStatus;
    }
}
