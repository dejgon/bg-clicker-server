namespace ClickerAPI.Models
{
    public interface IClickerDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string UpgradesCollectionName { get; set; }
        string StatisticsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}