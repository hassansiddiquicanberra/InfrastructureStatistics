using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Utils
{
    public static class TransformationHelper
    {
        public static string LevelOneGroupIdentifier(FreshServiceAgentGroupModel model)
        {
            var levelOneGroupId = "";

            var levelOneGroupRecord = model.Groups.FirstOrDefault(x => x.Name.Contains(ConfigHelper.FirstLevelHelpDesk));
            if (levelOneGroupRecord != null)
            {
                levelOneGroupId = levelOneGroupRecord.Id;
            }

            return levelOneGroupId;
        }
    }
}



