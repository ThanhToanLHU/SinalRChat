<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="SignalRChat.UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Management</title>

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/style.css" rel="stylesheet" />
    <link href="Content/font-awesome.css" rel="stylesheet" />

    <script src="Scripts/jQuery-1.9.1.min.js"></script>
    <script src="Scripts/jquery.signalR-2.2.2.min.js"></script>
    <script src="Scripts/date.format.js"></script>
    <style>
        .container {
            max-width: 400px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
        }

        h1 {
            text-align: center;
        }

        label {
            display: block;
            margin-bottom: 5px;
        }

        input[type="text"],
        select {
            padding: 5px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 3px;
        }

        input[type="file"] {
            margin-bottom: 10px;
        }

        .button-container {
            text-align: center;
        }

            .button-container .btn-save {
                padding: 10px 20px;
                background-color: #4CAF50;
                color: white;
                border: none;
                border-radius: 4px;
                cursor: pointer;
            }

                .button-container .btn-save:hover {
                    background-color: #45a049;
                }
    </style>

    <script>
        function handleClick() {
            $.ajax({
                type: "POST",
                url: "UserProfile.aspx/HandleButtonClick",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert('Button clicked!');
                },
                error: function (xhr, status, error) {
                    // Handle the error
                    alert("An error occurred: " + error);
                }
            });

            return false; // Prevent default form submission
        }
        $(document).ready(function () {
            var form = document.getElementById("form1");

            function submitForm(e) {
                e.preventDefault();
            }

            form.addEventListener('submit', submitForm);
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <h1>User Management</h1>

            <div>
                <label for="tbName">Name:<asp:TextBox ID="txtName" runat="server" Height="30px" Width="269px"></asp:TextBox></label>&nbsp;
                <label for="tbEmail">
                    Email:<asp:TextBox ID="txtEmail" runat="server" Height="29px" Width="267px"></asp:TextBox>
                </label>
            </div>

            <div>
                <label for="fileAvatar">
                    Avatar:<img src="<%= UserImage %>" class="user-image" alt="User Image" />
                    <a class="btn btn-default btn-flat" data-toggle="modal" href="#ChangePic">Change Picture</a>
                </label>
                &nbsp;
            </div>

            <div>
                <label for="ddlGender">Gender:</label>
                <asp:DropDownList ID="ddlGender" runat="server">
                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div>
                &nbsp;
            </div>
            <div class="button-container">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnRedirect" runat="server" Text="Redirect" OnClick="btnRedirect_Click" />
                        <asp:Button ID="btnSave" runat="server" BackColor="#CC0000" ForeColor="White" Height="43px" Text="Save" Width="81px" OnClick="OnBtnSaveClick"/>
                        <asp:Button ID="CloseBtn" runat="server" BackColor="#CC0000" ForeColor="White" Height="43px" Text="Close" Width="81px" OnClick="OnBtnCloseClick"/>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

        </div>

        <div class="modal fade" id="ChangePic" role="dialog">
            <div class="modal-dialog" style="width: 700px">
                <div class="modal-content">
                    <div class="modal-header bg-light-blue-gradient with-border">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Change Profile Picture</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnChangePicModel" />
                                </Triggers>
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <table class="table table-bordered table-striped table-hover table-responsive" style="width: 600px">

                                                <tr>

                                                    <div class="col-md-12">
                                                        <td class="text-primary col-md-4" style="font-weight: bold;">
                                                            <img id="ImgDisp" src="" class="user-image" style="height: 100px;" />
                                                        </td>
                                                        <td class="text-primary col-md-4" style="font-weight: bold;">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-default" />
                                                        </td>
                                                        <td class="col-md-4">

                                                            <asp:Button ID="btnChangePicModel" runat="server" Text="Update Picture" CssClass="btn btn-flat btn-success" OnClick="btnChangePicModel_Click" />


                                                        </td>
                                                    </div>

                                                </tr>


                                                <tr>
                                                    <div class="col-md-12">

                                                        <td class="col-md-12" colspan="3"></td>
                                                    </div>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>

                                    </div>
                </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="Scripts/bootstrap.min.js"></script>

    </form>

</body>
</html>
