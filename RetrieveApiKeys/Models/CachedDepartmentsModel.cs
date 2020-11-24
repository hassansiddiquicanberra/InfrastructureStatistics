namespace F1Solutions.InfrastructureStatistics.ApiCalls.Models
{
    public class CachedDepartmentsModel
    {
        public CachedDepartmentsModel(Department department)
        {
            CachedDepartment = department;
        }
        public Department CachedDepartment { get; set; }
    }

    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}