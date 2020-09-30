<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MoteurDeRecherche.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Style.css" rel="stylesheet" type="text/css" />
    <title>Search</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class =" container">
                        <div class ="container1"><h1>Search</h1></div>
                        <div class ="container2">
                            <asp:TextBox ID="txtBox" runat="server" class="auto-style1"></asp:TextBox>
                            <asp:Button ID="search" runat="server" Text="Go" Width="41px" height = "34px" color ="dimgray" OnClick="search_Click" />
                        </div>
                    </div>
                    <hr />
                    <div class ="results">
                        <%= nbrResultat %>
                        <br/><br/>
                        <div>
                            <a href = " #" runat="server" onServerClick="link0Clicked" class="linkResult"><%= results[0,1] %></a>
                            <br/><%= results[0,0] %>
                        </div><br/><br/>
                        <div>
                            <a href = " #" runat="server" onServerClick="link1Clicked" class="linkResult"><%= results[1,1] %></a>
                            <br/><%= results[1,0] %>
                        </div><br/><br/>
                        <div>
                            <a href = " #" runat="server" onServerClick="link2Clicked" class="linkResult"><%= results[2,1] %></a>
                            <br/><%= results[2,0] %>
                        </div><br/><br/>
                        <div>
                            <a href = " #" runat="server" onServerClick="link3Clicked" class="linkResult"><%= results[3,1] %></a>
                            <br/><%= results[3,0] %>
                        </div><br/><br/>
                        <div>
                            <a href = " #" runat="server" onServerClick="link4Clicked" class="linkResult"><%= results[4,1] %></a>
                            <br/><%= results[4,0] %>
                        </div><br/><br/>
                        <div>
                            <a href = " #" runat="server" onServerClick="link5Clicked" class="linkResult"><%= results[5,1] %></a>
                            <br/><%= results[5,0] %>
                        </div><br/><br/>
                        <a runat="server" onServerClick="Prev" ><%= prev %></a>
                        <a runat="server" onServerClick="Next" ><%= next %></a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
