<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Task.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <title>Plataforma</title>
</head>
<body>

    
    
<div class="container-fluid">
        <div class="row justify-content-center mt-5 p-3">

            <form id="FormshowProducts" class="col-7 p-4 border border-1" runat="server" method="post">
                <section style="display: flex;justify-content:space-around">
                <h2 class="sr-only mb-4">Gestor de productos</h2>
                 <asp:Button ID="endSession" runat="server" Text="Cerrar Sesión" CssClass="btn btn-outline-danger btn-block col-4"  OnClick="EndSession_Click"/>
                </section>
                <strong>USUARIO LOGUEADO: <span class="badge bg-light text-dark"><asp:Label ID="userActive" runat="server"></asp:Label></span></strong>
                <div class="form-group mt-4">
                    <asp:Button class="btn btn-outline-success btn-block col-12"  ID="buttonShowProducts" runat="server" Text="Mostrar productos"  OnClick="ShowProducts_Click"/>
                </div>
                <asp:Panel ID="searchPanel"  Visible="false" CssClass="row p-3" runat="server">
                       

                    <div style="display:flex;justify-content:space-around; padding:0 80px">
                        <asp:TextBox CssClass="form-control col-1" TextMode="Search" ID="searchText" runat="server" />
                        <asp:Button CssClass="btn btn-outline-danger col-1" ID="searchButton" Text="Filtrar" runat="server" OnClick="Filtering_Click" />
    
                     </div>
                        
                    </asp:Panel>
                <section style="display:flex;justify-content:center;align-content:center">
                    
                    <aside>

                    <asp:GridView ID="productList" runat="server" BackColor="White" BorderStyle="None" CellPadding="3" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="idProducto" OnRowCommand="RunCommands_On"
                      OnRowEditing="Run_OnEditing" 
                      OnRowCancelingEdit="Run_OnRowCancelingEdit" 
                      OnRowUpdating="Run_OnRowUpdating" 
                      OnRowDeleting="Run_OnRowDeleting">

                        

                        <HeaderStyle BackColor="LimeGreen" Font-Bold="true" ForeColor="White" />
                        <PagerStyle BackColor ="White" ForeColor="Black" HorizontalAlign="Left" />
                        <RowStyle ForeColor="Black" />
                        
                        <Columns>

                            <%--Template para id Producto--%>
                            <asp:TemplateField HeaderText="Id Producto">
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("[idProducto]") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox TextMode="Number" ID="textProductoId" Text='<%# Eval("[idProducto]") %>' runat="server"/>
                                </EditItemTemplate>
                            </asp:TemplateField>
                             <%--Template para Producto--%>
                            <asp:TemplateField HeaderText="Producto">
                                <HeaderTemplate>
                                    <asp:Label Text="Producto" runat="server" />
                                     <asp:Button ID="orderButtonProdut" CssClass="btn btn-outline-warning btn-block" Text="Ordenar" runat="server" OnClick="OrderDataProd_Click" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("[Producto]") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="ProductotextId" Text='<%# Eval("[Producto]") %>' runat="server"/>
                                </EditItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:TextBox ID="FootProductotextId" runat="server"/>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <%--Template para Descripcion--%>
                            <asp:TemplateField HeaderText="Descripcion">
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("[Descripcion]") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="textDescripcionId" Text='<%# Eval("[Descripcion]") %>' runat="server"/>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="FoottextDescripcionId" runat="server"/>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <%--Template para Valor--%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label Text="Valor" runat="server" />
                                     <asp:Button ID="orderButtonVal" CssClass="btn btn-outline-warning btn-block" Text="Ordenar" runat="server" OnClick="OrderDataVal_Click" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("[Valor]") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox TextMode="Number" ID="textValorId" Text='<%# Eval("[Valor]") %>' runat="server"/>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox TextMode="Number" ID="FoottextValorId" runat="server"/>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" CommandName="Edit" CssClass="btn btn-outline-success btn-block col-12" runat="server" Text="Modificar"></asp:Button>
                                    <asp:Button ID="btnDelete" CommandName="Delete"  CssClass="btn btn-outline-success btn-block col-12" runat="server" Text="Borrar"></asp:Button>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnUpdate" CommandName="Update" CssClass="btn btn-outline-success btn-block col-12" runat="server" Text="Guardar"></asp:Button>
                                    <asp:Button ID="btnCancel" CommandName="Cancel"  CssClass="btn btn-outline-success btn-block col-12" runat="server" Text="Cancelar"></asp:Button>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnAdd" CommandName="AddNew"  CssClass="btn btn-outline-success btn-block col-12" runat="server" Text="Insertar"></asp:Button>
                                </FooterTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    </aside>
                </section>
            </form>
        </div>
    </div>

    
</body>
</html>
