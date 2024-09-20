namespace MotoDev.Common.Dtos
{
    public class DashboardResponse
    {
        public int RepairsThisYear { get; set; }

        public int RepairsIncreaseThisYear { get; set; }

        public int RevenueThisMonth { get; set; }

        public int RevenueIncreaseThisMonth { get; set; }

        public int CustomersTotal { get; set; }

        public int CustomersIncreaseThisYear { get; set; }

        public IEnumerable<DashboardRecentActivity> DashboardRecentActivity { get; set; }

        public DashboardReports DashboardReports { get; set; }
    }

    public class DashboardRecentActivity
    {
        public string Time { get; set; }

        public string Title { get; set; }

        public int RepairStatusId { get; set; }
    }

    public class DashboardReports
    {
        public IList<int> Repairs { get; set; }

        public IList<decimal> TotalProfits { get; set; }

        public IList<DateTime> Dates { get; set; }
    }
}