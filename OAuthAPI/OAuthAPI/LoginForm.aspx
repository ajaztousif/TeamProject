<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="OAuthAPI.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Login Form</h1>
    <form id="form1" runat="server">
        <div>
            Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
            Password: <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:Label ID="Label1" runat="server"></asp:Label><br />
            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
        </div>
        <div>
            Username:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="Nametxt" runat="server"></asp:TextBox><br />
            Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="UserTxt" runat="server"></asp:TextBox><br />
            <asp:Label ID="Label2" runat="server"></asp:Label><br />
            Password:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="Pwdtxt" runat="server" TextMode="Password"></asp:TextBox><br />
            CPasword:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="CPwdtxt" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords doesn't matches" Operator="Equal" ControlToValidate="CPwdtxt" ControlToCompare="Pwdtxt" Type="String"></asp:CompareValidator>
            <asp:Button ID="Button2" runat="server" Text="Register" OnClick="Button2_Click"/>
        </div>
        <div>
            Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    <asp:TextBox ID="Uname" runat="server"></asp:TextBox><br />
            Password: <asp:TextBox ID="Pwd" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:Label ID="Label3" runat="server"></asp:Label><br />
            New Password: <asp:TextBox ID="NPwd" runat="server" TextMode="Password"></asp:TextBox><br />
            C Password:   <asp:TextBox ID="NCPwd" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Passwords doesn't matches" Operator="Equal" ControlToValidate="NCPwd" ControlToCompare="NPwd" Type="String"></asp:CompareValidator><br />
            <asp:Button ID="Button4" runat="server" Text="Save" OnClick="Button3_Click"/>
        </div>
        <div>
            FName: <asp:TextBox runat="server" ID="FName"/>
            LName: <asp:TextBox runat="server" ID="LName"/>
            Email: <asp:TextBox runat="server" ID="Email"/>
            UName: <asp:TextBox runat="server" ID="UsName"/>
            Pwd: <asp:TextBox runat="server" ID="UPwd" TextMode="Password" />
            Phno: <asp:TextBox runat="server" ID="Phno" TextMode="Number"/>
            DeptId: <asp:TextBox runat="server" ID="DId" TextMode="Number"/>
            <asp:Button Text="Submit" ID="BtnSmbt" runat="server" OnClick="BtnSmbt_Click" />
        </div>
    </form>
</body>
</html>
