<%@ Page Title="Database" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatabaseView.aspx.cs" Inherits="Lab1.DatabseView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    
    <asp:ListView ID="listView1ID"
        DataSourceID =""
        runat="server">

    </asp:ListView>

</asp:Content>
