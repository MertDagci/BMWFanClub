namespace BMWFanClub.Data.MySQL
{
    public class Settings
    {
        public MysqlInfo MysqlInfo { get; set; } = new MysqlInfo();
    }
    public class MysqlInfo
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
