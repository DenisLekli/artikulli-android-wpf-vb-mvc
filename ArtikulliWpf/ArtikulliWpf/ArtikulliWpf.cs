using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtikulliWpf
{
    public static class ArtikulliWpfConfig
    {
        public static string GetConnectionString()
        {
            return "Data Source=DESKTOP-2179KCJ\\SQLEXPRESS;Database=Artikulli_DB;Integrated Security=True;TrustServerCertificate=True";
        }
    }
}
