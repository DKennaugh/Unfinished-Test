<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="Netclip_Assessment.Browse" %>
    
    

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="sidebar">
        <h4>Narrow Search</h4>

        <div>
            <label class="sidebar-header">Body Types</label>
            <asp:CheckBoxList ID="RBLBody" runat="server" OnSelectedIndexChanged="RBLBody_SelectedIndexChanged" AutoPostBack="true" CssClass="sidebar-checkbox"></asp:CheckBoxList>
        </div>

        <div>
            <label class="sidebar-header">Engine Types</label>
            <asp:CheckBoxList ID="RBLEngine" runat="server" OnSelectedIndexChanged="RBLEngine_SelectedIndexChanged" AutoPostBack="true" CssClass="sidebar-checkbox"></asp:CheckBoxList>
        </div>

        
        <div>
            <label class="sidebar-header">Gear Types</label>
            <asp:CheckBoxList ID="RBLGear" runat="server" OnSelectedIndexChanged="RBLGear_SelectedIndexChanged" AutoPostBack="true" CssClass="sidebar-checkbox"></asp:CheckBoxList>
        </div>

        <div id="headyboi">
        <asp:CheckBox CssClass="sidebar-header" id="hasCar" runat="server" Text="In Stock" Enabled="True" Checked="True" OnCheckedChanged="HasCar_CheckedChanged" AutoPostBack="true"/>
            </div>
    </div>


    <div class="browse" >
        <div id="browserBox" runat="server">

        </div>
        <div class="browse-footer">
            <asp:Button ID="btnPrev" runat="server" Text="Prev" OnClick="BtnPrev_Click" />
            <asp:DropDownList ID="ddlList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLList_SelectedIndexChanged"></asp:DropDownList>
            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="BtnNext_Click" />
        </div>  
    </div>
</asp:Content>

