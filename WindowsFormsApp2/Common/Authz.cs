using System.Windows.Forms;

namespace WindowsFormsApp2.Common
{
    static class Authz
    {
        public static bool Has(string code) =>
            Program.CurrentPerms != null && Program.CurrentPerms.Contains(code);

        public static bool Demand(string code, bool showMessage = true)
        {
            if (Has(code)) return true;
            if (showMessage) MessageBox.Show("Bạn không có quyền: " + code);
            return false;
        }
    }
}
