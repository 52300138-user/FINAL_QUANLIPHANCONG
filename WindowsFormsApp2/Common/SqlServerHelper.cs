using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.ServiceProcess;

public static class SqlServerHelper
{
    public static List<string> GetNetworkInstances()
    {
        var list = new List<string>();

        try
        {
            var dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            foreach (DataRow row in dt.Rows)
            {
                string server = row["ServerName"]?.ToString();
                string instance = row["InstanceName"]?.ToString();

                if (string.IsNullOrEmpty(server)) continue;

                list.Add(string.IsNullOrEmpty(instance) ? server : server + "\\" + instance);
            }
        }
        catch { }

        return list;
    }

    public static List<string> GetLocalInstances()
    {
        var list = new List<string>();

        try
        {
            foreach (var svc in ServiceController.GetServices())
            {
                if (svc.ServiceName.Equals("MSSQLSERVER", StringComparison.OrdinalIgnoreCase))
                    list.Add(".");
                else if (svc.ServiceName.StartsWith("MSSQL$", StringComparison.OrdinalIgnoreCase))
                    list.Add(@".\" + svc.ServiceName.Substring(6));
            }
        }
        catch { }

        return list;
    }

    public static List<string> GetAllInstances()
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var s in GetLocalInstances()) set.Add(s);
        foreach (var s in GetNetworkInstances()) set.Add(s);

        return set.OrderBy(x => x).ToList();
    }
}
