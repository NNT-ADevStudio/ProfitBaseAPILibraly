namespace ProfitBaseAPILibraly.Classes
{
    public class CastomStatus
    {
        public int Id { get; }

        public string Name { get; }

        public string BaseStatus { get; set; }

        public bool IsProtected { get; set; }

        public string Alias { get; set; }

        public CastomStatus(int id, string name)
        {
            Name = name;
            Id = id;
        }

        public CastomStatus(int id, string name, string baseStatus) : this(id, name) => BaseStatus = baseStatus;

        public CastomStatus(int id, string name, string baseStatus, bool isProtected) : this(id, name, baseStatus) => IsProtected = isProtected;

        public CastomStatus(int id, string name, string baseStatus, bool isProtected, string alias) : this(id, name, baseStatus, isProtected) => Alias = alias;
    }
}
