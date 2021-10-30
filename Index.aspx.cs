using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

using Task.Helpers;

using static System.Net.Mime.MediaTypeNames;

namespace Task
{
	public partial class Index : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Session["CurrentUser"]?.ToString()))
			{
				var currtenUser = Session["CurrentUser"].ToString();
				userActive.Text = currtenUser;
			}
			else
			{
				Response.Redirect("Login.aspx");
			}

		}

		protected void ShowProducts_Click(object sender, EventArgs e) => LoadProducts();

		protected void RunCommands_On(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName.Equals("AddNew"))
			{
				var connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
				using (var sqlConnection = new SqlConnection(connectionString))
				{
					var command = new SqlCommand($"[dbo].[{Enum.GetName(typeof(EnumStoredProcedure), EnumStoredProcedure.InsertarProducto)}]", sqlConnection)
					{
						CommandType = CommandType.StoredProcedure
					};
					try
					{
						command.Connection.Open();
						command.Parameters.Add("@producto", SqlDbType.VarChar, 50).Value = GetText("FootProductotextId");
						command.Parameters.Add("@descripcion", SqlDbType.VarChar, 500).Value = GetText("FoottextDescripcionId");
						command.Parameters.Add("@valor", SqlDbType.Int).Value = int.Parse(GetText("FoottextValorId"));
						if (e.CommandName.Equals("Update"))
						{
							command.Connection.Open();
							command.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(GetText("textProductoId"));
							command.Parameters.Add("@producto", SqlDbType.VarChar, 50).Value = GetText("ProductotextId");
							command.Parameters.Add("@descripcion", SqlDbType.VarChar, 500).Value = GetText("textDescripcionId");
							command.Parameters.Add("@valor", SqlDbType.Int).Value = int.Parse(GetText("textValorId"));
							productList.EditIndex = -1;
						}
						if (e.CommandName.Equals("Delete"))
						{
							command.Connection.Open();
							command.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(GetText("textProductoId"));
						}
						var result = command.ExecuteNonQuery();
						LoadProducts();
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


		protected void Run_OnEditing(object sender, GridViewEditEventArgs e)
		{
			productList.EditIndex = e.NewEditIndex;
			LoadProducts();
		}

		protected void Run_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			productList.EditIndex = -1;
			LoadProducts();
		}
		protected void Run_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			var connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var command = new SqlCommand($"[dbo].[{Enum.GetName(typeof(EnumStoredProcedure), EnumStoredProcedure.ActualizarProducto)}]", sqlConnection)
				{
					CommandType = CommandType.StoredProcedure
				};
				try
				{
					command.Connection.Open();
					command.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse((productList.Rows[e.RowIndex].FindControl("textProductoId") as TextBox).Text.Trim());
					command.Parameters.Add("@producto", SqlDbType.VarChar, 50).Value = (productList.Rows[e.RowIndex].FindControl("ProductotextId") as TextBox).Text.Trim();
					command.Parameters.Add("@descripcion", SqlDbType.VarChar, 500).Value = (productList.Rows[e.RowIndex].FindControl("textDescripcionId") as TextBox).Text.Trim();
					command.Parameters.Add("@valor", SqlDbType.Int).Value = int.Parse((productList.Rows[e.RowIndex].FindControl("textValorId") as TextBox).Text.Trim());
					productList.EditIndex = -1;

					var result = command.ExecuteNonQuery();
					LoadProducts();
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

		protected void Run_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			var connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				var command = new SqlCommand($"[dbo].[{Enum.GetName(typeof(EnumStoredProcedure), EnumStoredProcedure.BorrarProducto)}]", sqlConnection)
				{
					CommandType = CommandType.StoredProcedure
				};
				try
				{

					command.Connection.Open();
					command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(productList.DataKeys[e.RowIndex].Value.ToString());
					var result = command.ExecuteNonQuery();
					LoadProducts();
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


		private void LoadProducts()
		{
			searchPanel.Visible = true;
			var connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				var command = new SqlDataAdapter($"[dbo].[{Enum.GetName(typeof(EnumStoredProcedure), EnumStoredProcedure.ConsultarProductos)}]", sqlConnection);
				try
				{
					var table = new DataTable();
					command.Fill(table);
					productList.DataSource = table;
					productList.DataBind();
				}
				catch (Exception)
				{

					throw;
				}
				finally
				{
					sqlConnection.Close();
				}

			}
		}
		public void OrderDataProd_Click(object sender, EventArgs e)
		{
			searchPanel.Visible = true;
			var connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				var command = new SqlDataAdapter($"SELECT [idProducto],[Producto],[Descripcion],[Valor] " +
					$"FROM[Test].[dbo].[Productos] " +
					$"ORDER BY Producto", sqlConnection);
				try
				{
					var table = new DataTable();
					command.Fill(table);
					productList.DataSource = table;
					productList.DataBind();
				}
				catch (Exception)
				{

					throw;
				}
				finally
				{
					sqlConnection.Close();
				}

			}
		}
		public void OrderDataVal_Click(object sender, EventArgs e)
		{
			searchPanel.Visible = true;
			var connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				var command = new SqlDataAdapter($"SELECT [idProducto],[Producto],[Descripcion],[Valor] " +
					$"FROM[Test].[dbo].[Productos] " +
					$"ORDER BY Valor", sqlConnection);
				try
				{
					var table = new DataTable();
					command.Fill(table);
					productList.DataSource = table;
					productList.DataBind();
				}
				catch (Exception)
				{

					throw;
				}
				finally
				{
					sqlConnection.Close();
				}

			}
		}
		public void Filtering_Click(object sender, EventArgs e)
		{
			searchPanel.Visible = true;
			var connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
			using (var sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				var command = new SqlDataAdapter($"SELECT [idProducto],[Producto],[Descripcion],[Valor] " +
					$"FROM[Test].[dbo].[Productos]   WHERE Producto = '{searchText?.Text ?? string.Empty}'"
					, sqlConnection);
				try
				{
					var table = new DataTable();
					command.Fill(table);
					productList.DataSource = table;
					productList.DataBind();
				}
				catch (Exception)
				{

					throw;
				}
				finally
				{
					sqlConnection.Close();
				}

			}
		}
		private string GetText(string name) => (productList.FooterRow.FindControl(name) as TextBox)?.Text.Trim() ?? "<Empty>";


		protected void EndSession_Click(object sender, EventArgs e)
		{
			Session.Remove("CurrentUser");
			Response.Redirect("Login.aspx");
		}
	}
}