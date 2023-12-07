using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtikullServices.Data
{
    public static class Config
    {
        public static string GetConnectionString()
        {
            return "Data Source=localhost;Database=Lawyer_Office;Integrated Security=True;TrustServerCertificate=True";
        }
    }
}
