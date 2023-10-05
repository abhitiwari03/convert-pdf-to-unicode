<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="convert_pdf_to_unicode.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="fileUpload" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload PDF" OnClick="btnUpload_Click" />
            <br />
            <asp:HyperLink ID="downloadLink" runat="server" Text="Download Unicode PDF" Visible="false" />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" />
        </div>
    </form>
</body>
</html>
