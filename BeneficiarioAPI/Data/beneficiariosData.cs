//using System.Data.SqlClient;


using Microsoft.Data.SqlClient;

namespace BeneficiarioAPI.Data
{
    public class beneficiariosData
    {
        SqlConnection cnstr = new SqlConnection(@"server=DESKTOP1\SQLEXPRESS; database=dbBeneficiarios; user id=sa; password=U1tr0m0n; Trusted_Connection= True; MultipleActiveResultSets= True;TrustServerCertificate= true;Encrypt= true");
        public SqlConnection getConnection
        {
            get { return cnstr; }
        }
    }
}
