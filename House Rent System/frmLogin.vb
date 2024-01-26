Imports System.Data.OleDb
Imports System.Text
Imports System.Security.Cryptography

Public Class frmLogin
    Dim uname As String
    Dim passwd As String
    Dim attempts As Integer = 5
    Dim connstring As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|datadirectory|\hrs.mdb"
    Dim connect As New OleDbConnection

#Region "Connections"
    Public Sub openconnection()
        If connect.State = ConnectionState.Closed Then
            connect.ConnectionString = connstring
            connect.Open()
        ElseIf connect.State = ConnectionState.Open Then
            Me.Refresh()
        End If
    End Sub

    Public Sub closeconnection()
        If connect.State = ConnectionState.Open Then
            connect.Close()
        ElseIf connect.State = ConnectionState.Closed Then
            Me.Refresh()
        End If
    End Sub
#End Region

#Region "Login Button"
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        openconnection()
        uname = UsernameTextBox.Text.Trim
        passwd = PasswordTextBox.Text.Trim

        If attempts = 0 Then
            MsgBox("For security purpose, HRS will now close", MsgBoxStyle.Information, "Security level 2")
            closeconnection()
            Me.Dispose()
            Me.Close()
        ElseIf uname = "" Or IsNothing(uname) = True Then
            hideerrors()
            lblErroruname.Show()
            lblErroruname.Text = "Please enter username"
            UsernameTextBox.Focus()
            attempts = attempts - 1
            lblErrorattempts.Text = attempts & " Attempts left"
            Exit Sub
        ElseIf uname.Length < 4 Then
            hideerrors()
            lblErroruname.Show()
            lblErroruname.Text = "Please enter username correctly"
            UsernameTextBox.Focus()
            attempts = attempts - 1
            lblErrorattempts.Text = attempts & " Attempts left"
            Exit Sub

        ElseIf passwd = "" Or IsNothing(passwd) = True Then
            hideerrors()
            lblErrorpassword.Show()
            lblErrorpassword.Text = "Please enter password"
            PasswordTextBox.Focus()
            attempts = attempts - 1
            lblErrorattempts.Text = attempts & " Attempts left"
            Exit Sub
        ElseIf passwd.Length < 6 Then
            hideerrors()
            lblErrorpassword.Show()
            lblErrorpassword.Text = "Please enter password correctly"
            PasswordTextBox.Focus()
            attempts = attempts - 1
            lblErrorattempts.Text = attempts & " Attempts left"
            Exit Sub

        Else
            Dim Ue As New UnicodeEncoding()
            Dim ByteSourceText() As Byte = Ue.GetBytes(passwd)
            Dim Md5 As New MD5CryptoServiceProvider()
            Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
            Convert.ToBase64String(ByteHash)
            Dim hashPwd As String
            hashPwd = Convert.ToBase64String(ByteHash)

            Dim selectqry As String = "SELECT uname,passwd FROM users WHERE uname='" + uname + "'"
            Dim da As OleDbDataAdapter
            da = New OleDbDataAdapter(selectqry, connect)
            Dim dtset As DataSet
            dtset = New DataSet()
            Try
                da.Fill(dtset, "users")
            Catch ex As Exception
                MsgBox("Error occured. Please try again", MessageBoxIcon.Error, "Error")
                Exit Sub
            End Try
            Dim dttable As DataTable
            dttable = New DataTable
            dttable = dtset.Tables("users")

            Dim dbuname, dbpasswd As String
            For Each tempRow In dttable.Rows
                dbuname = tempRow("uname").ToString
                dbpasswd = tempRow("passwd").ToString

                If (dbuname = uname) And (dbpasswd = hashPwd) Then
                    clearinputs()
                    closeconnection()
                    MsgBox("Welcome " + uname, MsgBoxStyle.Information, "Welcome")
                    frmMain.Show()
                    attempts = 5
                    hideerrors()
                    Me.Hide()

                Else
                    attempts = attempts - 1
                    hideerrors()
                    MsgBox("Error occured. Please try again", MessageBoxIcon.Warning, "Error")
                    lblErrorattempts.Show()
                    lblErrorattempts.Text = attempts & " Attempts left"
                    Me.Refresh()
                    Me.WindowState = FormWindowState.Normal
                    Exit Sub
                End If
            Next

        End If
    End Sub
#End Region

#Region "Cancel Button"
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Dispose()
        Me.Close()
    End Sub
#End Region

#Region "Clear inputs"
    Private Sub clearinputs()
        UsernameTextBox.Clear()
        PasswordTextBox.Clear()
    End Sub
#End Region

#Region "Hide errors"
    Private Sub hideerrors()
        lblErrorattempts.Hide()
        lblErroruname.Hide()
        lblErrorpassword.Hide()
    End Sub
#End Region
End Class
