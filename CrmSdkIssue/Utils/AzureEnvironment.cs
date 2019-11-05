using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmSdkIssue
{
    public static class AzureEnvironment
    {
        public static string GetSetting(string variable)
        {
            string setting = Environment.GetEnvironmentVariable(variable);

            if (String.IsNullOrEmpty(setting))
            {
                throw new ArgumentException($"Setting '{variable}' is not configured.");
            }

            return setting;
        }

        public static bool GetSettingAsBoolean(string variable)
        {
            return Boolean.Parse(GetSetting(variable));
        }

        public static List<string> GetSettingAsList(string variable)
        {
            string list = GetSetting(variable);

            return list.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
        }

        public static string GetSetting(string variable, string defaultValue)
        {
            string value = Environment.GetEnvironmentVariable(variable);

            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            return value;
        }
    }
}
