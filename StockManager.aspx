<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockManager.aspx.cs" Inherits="Netclip_Assessment.StockManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

    

    <div class="manage">
        <div class="manage-inner">
            <h3 style="text-align:center">Stock Management</h3>

            Model Name:
            <br />
            <asp:TextBox ID="txtName" runat="server" MaxLength="128" ValidateRequestMode="Enabled" Width="20em"></asp:TextBox>
            <br />
            Body Type:
            <br />
            <asp:DropDownList ID="dropdownBody" runat="server" Height="20px" Width="20em"></asp:DropDownList>
            <br />
            Engine Type:
            <br />
            <asp:DropDownList ID="dropdownEngine" runat="server" Height="20px" Width="20em"></asp:DropDownList>
            <br />
            Gear Type:
            <br />
            <asp:DropDownList ID="dropdownGear" runat="server" Height="20px" Width="20em"></asp:DropDownList>
            <br />
            Price:
            <br />
            <asp:TextBox ID="textPrice" runat="server" TextMode="Number" ValidateRequestMode="Enabled" Min="0" Width="20em">0</asp:TextBox>
            <br />
            Amount in Stock:
            <br />
            <asp:TextBox ID="textQty" runat="server" TextMode="Number" ValidateRequestMode="Enabled" Min="0" Width="20em">0</asp:TextBox>
            <br />
            Description:
            <br />
            <asp:TextBox ID="textDesc" runat="server" MaxLength="8000" Rows="10" TextMode="MultiLine" Width="20em"></asp:TextBox>
            <br />
            Image Source:
            <br />
            <asp:TextBox ID="textImg" runat="server" MaxLength="8000" ValidateRequestMode="Enabled" Width="20em"></asp:TextBox>
            <br />
            <br />
            <div class="browse-footer">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" visible="false" OnClick="BtnDelete_Click" OnClientClick="return confirm('Are you sure? This process is irreversible!')" />
            </div>
        </div>
    </div>

</asp:Content>
