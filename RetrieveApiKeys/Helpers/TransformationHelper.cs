using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class TransformationHelper
    {
        public static string FindLevelOneGroupIdentifier(FreshServiceAgentGroupModel model)
        {
            var levelOneGroupId = string.Empty;

            var levelOneGroupRecord = model.Groups.FirstOrDefault(x => x.Name.Contains(ConfigHelper.FirstLevelHelpDesk));
            if (levelOneGroupRecord != null)
            {
                levelOneGroupId = levelOneGroupRecord.Id;
            }

            return levelOneGroupId;
        }
    }
}



