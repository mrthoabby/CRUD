using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Task.Helpers;

namespace Task
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Session["CurrentUser"]?.ToString()))Response.Redirect("Index.aspx");
		}

		protected void BtnLogin_Click(object sender,EventArgs e)
		{
			var connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var command = new SqlCommand($"[dbo].[{Enum.GetName(typeof(EnumStoredProcedure), EnumStoredProcedure.ValidarAutenticacion)}]", sqlConnection)
				{
					CommandType = CommandType.StoredProcedure
				};
				try
				{
					command.Connection.Open();
					command.Parameters.Add("@login", SqlDbType.VarChar, 50).Value = textUserName.Text;
					command.Parameters.Add("@pass", SqlDbType.VarChar, 50).Value = textPassword.Text;
					var readDataBase = command.ExecuteReader();
					if (readDataBase.Read())
					{
						//Agregando Sesión de usuario
						Session["CurrentUser"] = textUserName.Text; 
						Response.Redirect("Index.aspx");
					}
					else
					{
						resultError.Visible = true;
						resultError.Text = "Usuario o contraseña Incorrecto";
					}
				}
				catch (Exception)
				{

					throw;
				}
				finally
				{
					command.Connection.Close();
				}
				
			}
				
			
		}
	}
}