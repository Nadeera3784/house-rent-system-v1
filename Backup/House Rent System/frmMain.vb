Imports System.Text
Imports System.Security.Cryptography
Imports System.Data.OleDb

Public Class frmMain
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

#Region "When form load"
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        enablemenubtn()
        hidegb()

        dtpStartdate.Format = DateTimePickerFormat.Custom
        dtpStartdate.CustomFormat = "dd/MM/yyyy"

        dtpEnddate.Format = DateTimePickerFormat.Custom
        dtpEnddate.CustomFormat = "dd/MM/yyyy"
    End Sub
#End Region

#Region "Menu Buttons"

#Region "View all Customers"

#Region "Menu Button"
    Private Sub btnMenuCustomers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMenuCustomers.Click
        viewall()
        enablemenubtn()
        btnMenuCustomers.Enabled = False
        hidegb()
        gbCustomers.Show()
        gbCustomers.Dock = DockStyle.Fill
    End Sub
#End Region

#Region "Search Button"
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim search As String = txtSearch.Text.Trim
        If (search = "" Or IsNothing(search) = True) Then
            MsgBox("Please enter something to search", MessageBoxIcon.Warning, "Error")
            txtSearch.Focus()
            Exit Sub
        Else
            openconnection()
            Dim searchqry As String = "SELECT id,fname,mname,lname,mstatus,houseno,roomno,amountpaid,amountremain,startdate,enddate FROM customers WHERE id LIKE '%" + search + "%' OR fname LIKE '%" + search + "%' OR mname LIKE '%" + search + "%' OR lname LIKE '%" + search + "%' OR mstatus LIKE '%" + search + "%' OR houseno LIKE '%" + search + "%' OR roomno LIKE '%" + search + "%' OR amountpaid LIKE '%" + search + "%' OR amountremain LIKE '%" + search + "%' OR startdate LIKE '%" + search + "%' OR enddate LIKE '%" + search + "%'"
            Dim da As OleDbDataAdapter
            Dim dtset As New DataSet
            Dim dttable As New DataTable
            da = New OleDbDataAdapter(searchqry, connect)
            Try
                da.Fill(dtset, "customers")
            Catch ex As Exception
                MsgBox("Error occured. Please try again", MessageBoxIcon.Error, "Error")
                Exit Sub
            End Try
            dttable = dtset.Tables("customers")

            dgvCustomers.DataSource = dttable
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = New Font(dgvCustomers.Font, FontStyle.Bold)
            dgvCustomers.Font = New Font(dgvCustomers.Font, FontStyle.Regular)

            dgvCustomers.Columns("ID").HeaderText = "S/N"
            dgvCustomers.Columns("ID").Width = 50

            dgvCustomers.Columns("fname").HeaderText = "First Name"
            dgvCustomers.Columns("fname").Width = 120

            dgvCustomers.Columns("mname").HeaderText = "Middle Name"
            dgvCustomers.Columns("mname").Width = 120

            dgvCustomers.Columns("lname").HeaderText = "Last Name"
            dgvCustomers.Columns("lname").Width = 120

            dgvCustomers.Columns("mstatus").HeaderText = "Marital"
            dgvCustomers.Columns("mstatus").Width = 80

            dgvCustomers.Columns("houseno").HeaderText = "House No"
            dgvCustomers.Columns("houseno").Width = 80

            dgvCustomers.Columns("roomno").HeaderText = "Room"
            dgvCustomers.Columns("roomno").Width = 80

            dgvCustomers.Columns("amountpaid").HeaderText = "Amount Paid"
            dgvCustomers.Columns("amountpaid").Width = 100

            dgvCustomers.Columns("amountremain").HeaderText = "Amount Remain"
            dgvCustomers.Columns("amountremain").Width = 100

            dgvCustomers.Columns("startdate").HeaderText = "Start Date"
            dgvCustomers.Columns("startdate").Width = 100

            dgvCustomers.Columns("enddate").HeaderText = "End Date"
            dgvCustomers.Columns("enddate").Width = 100

            closeconnection()
            txtSearch.Clear()
        End If
    End Sub
#End Region

#Region "Edit Button"
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If lblDeletereference.Text = "delete reference" Then
            MsgBox("Please select a customer from the list", MessageBoxIcon.Warning, "Error")
            Exit Sub

        Else
            openconnection()
            Dim selectqry As String = "SELECT * FROM customers WHERE id=" + lblDeletereference.Text
            Dim da As OleDbDataAdapter
            da = New OleDbDataAdapter(selectqry, connect)
            Dim dtset As DataSet
            dtset = New DataSet
            Try
                da.Fill(dtset, "customers")
            Catch ex As Exception
                MsgBox("Error occured. Please try again", MessageBoxIcon.Error, "Error")
                Exit Sub
            End Try
            Dim dttable As DataTable
            dttable = New DataTable
            dttable = dtset.Tables("customers")

            Dim dbid, dbfname, dbmname, dblname, dbmstatus, dbjob, dbaddress, dbmobile, dbhouseno, dbroomno, dbamountpaid, dbamountremain, dbstartdate, dbenddate As String
            For Each temprow In dttable.Rows
                dbid = temprow("id").ToString
                dbfname = temprow("fname").ToString
                dbmname = temprow("mname").ToString
                dblname = temprow("lname").ToString
                dbmstatus = temprow("mstatus").ToString
                dbjob = temprow("job").ToString
                dbaddress = temprow("address").ToString
                dbmobile = temprow("mobile").ToString
                dbhouseno = temprow("houseno").ToString
                dbroomno = temprow("roomno").ToString
                dbamountpaid = temprow("amountpaid").ToString
                dbamountremain = temprow("amountremain").ToString
                dbstartdate = temprow("startdate").ToString
                dbenddate = temprow("enddate").ToString
            Next

            lblDeletereference.Text = "delete reference"
            hidegb()
            hideerrors()
            clearinputs()
            gbAddCustomer.Show()
            gbAddCustomer.Dock = DockStyle.Fill
            enablemenubtn()
            btnMenuAddCustomers.Enabled = False
            lblEditreference.Text = dbid
            txtFname.Text = dbfname
            txtMname.Text = dbmname
            txtLname.Text = dblname
            cbMstatus.Text = dbmstatus
            txtJob.Text = dbjob
            txtAddress.Text = dbaddress
            txtMobile.Text = dbmobile
            txtHouseno.Text = dbhouseno
            txtRoomno.Text = dbroomno
            txtAmountpaid.Text = dbamountpaid
            txtAmountremain.Text = dbamountremain
            dtpStartdate.Text = dbstartdate
            dtpEnddate.Text = dbenddate
        End If
    End Sub
#End Region

#Region "Delete Button"
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If lblDeletereference.Text = "delete reference" Then
            MsgBox("Please select a customer from the list", MessageBoxIcon.Warning, "Error")
            Exit Sub

        Else
            Dim deleteqry As String = "DELETE FROM customers WHERE id=" + lblDeletereference.Text
            Dim deletecmd As New OleDbCommand
            Dim deletemsg As String = MessageBox.Show("Are you sure you want to delete user with ID " + lblDeletereference.Text + "?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If deletemsg = MsgBoxResult.No Then
                Me.Refresh()
                viewall()
                lblDeletereference.Text = "delete reference"
            ElseIf deletemsg = MsgBoxResult.Yes Then
                openconnection()
                With deletecmd
                    .CommandText = deleteqry
                    .Connection = connect
                    Try
                        .ExecuteNonQuery()
                        MsgBox("Customer deleted successfully", MessageBoxIcon.Information, "Success")
                        viewall()
                        lblDeletereference.Text = "delete reference"
                        closeconnection()
                    Catch ex As Exception
                        MsgBox("Error occured. Please try again", MessageBoxIcon.Error, "Error")
                        closeconnection()
                        Exit Sub
                    End Try
                End With
            End If
        End If
    End Sub
#End Region

#Region "Refresh Button"
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        viewall()
        txtSearch.Clear()
        lblDeletereference.Text = "delete reference"
        lblEditreference.Text = "edit reference"
    End Sub
#End Region

#Region "All customers"
    Private Sub viewall()
        openconnection()

        Dim selectqry As String = "SELECT id,fname,mname,lname,mstatus,houseno,roomno,amountpaid,amountremain,startdate,enddate FROM customers"
        Dim da As OleDbDataAdapter
        da = New OleDbDataAdapter(selectqry, connect)
        Dim dtset As DataSet
        dtset = New DataSet
        Try
            da.Fill(dtset, "customers")
        Catch ex As Exception
            MsgBox("Error occured. Please try again", MessageBoxIcon.Error, "Error")
            Exit Sub
        End Try
        Dim dttable As DataTable
        dttable = New DataTable
        dttable = dtset.Tables("customers")

        dgvCustomers.DataSource = dttable
        dgvCustomers.ColumnHeadersDefaultCellStyle.Font = New Font(dgvCustomers.Font, FontStyle.Bold)
        dgvCustomers.Font = New Font(dgvCustomers.Font, FontStyle.Regular)

        dgvCustomers.Columns("ID").HeaderText = "S/N"
        dgvCustomers.Columns("ID").Width = 50

        dgvCustomers.Columns("fname").HeaderText = "First Name"
        dgvCustomers.Columns("fname").Width = 120

        dgvCustomers.Columns("mname").HeaderText = "Middle Name"
        dgvCustomers.Columns("mname").Width = 120

        dgvCustomers.Columns("lname").HeaderText = "Last Name"
        dgvCustomers.Columns("lname").Width = 120

        dgvCustomers.Columns("mstatus").HeaderText = "Marital"
        dgvCustomers.Columns("mstatus").Width = 80

        dgvCustomers.Columns("houseno").HeaderText = "House No"
        dgvCustomers.Columns("houseno").Width = 80

        dgvCustomers.Columns("roomno").HeaderText = "Room"
        dgvCustomers.Columns("roomno").Width = 80

        dgvCustomers.Columns("amountpaid").HeaderText = "Amount Paid"
        dgvCustomers.Columns("amountpaid").Width = 100

        dgvCustomers.Columns("amountremain").HeaderText = "Amount Remain"
        dgvCustomers.Columns("amountremain").Width = 100

        dgvCustomers.Columns("startdate").HeaderText = "Start Date"
        dgvCustomers.Columns("startdate").Width = 100

        dgvCustomers.Columns("enddate").HeaderText = "End Date"
        dgvCustomers.Columns("enddate").Width = 100

        closeconnection()
    End Sub
#End Region

#Region "When click customer"
    Private Sub dgvCustomers_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCustomers.CellContentClick
        Dim s As System.Drawing.Point
        s = dgvCustomers.CurrentCellAddress
        Dim id As String = dgvCustomers.Item("ID", s.Y).Value.ToString
        lblDeletereference.Text = id
    End Sub
#End Region

#End Region

#Region "Add new Customers"

#Region "Menu Button"
    Private Sub btnMenuAddCustomers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMenuAddCustomers.Click
        enablemenubtn()
        btnMenuAddCustomers.Enabled = False
        hidegb()
        gbAddCustomer.Show()
        gbAddCustomer.Dock = DockStyle.Fill
        hideerrors()
        clearinputs()
    End Sub
#End Region

#Region "Confirm Button"
    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim fname As String = txtFname.Text.Trim
        Dim mname As String = txtMname.Text.Trim
        Dim lname As String = txtLname.Text.Trim
        Dim mstatus As String = cbMstatus.Text
        Dim job As String = txtJob.Text.Trim
        Dim address As String = txtAddress.Text.Trim
        Dim mobile As String = txtMobile.Text.Trim
        Dim houseno As String = txtHouseno.Text.Trim
        Dim roomno As String = txtRoomno.Text.Trim
        Dim amountpaid As String = txtAmountpaid.Text.Trim
        Dim amountremain As String = txtAmountremain.Text.Trim
        Dim startdate As String = dtpStartdate.Text
        Dim enddate As String = dtpEnddate.Text

        If fname = "" Or IsNothing(fname) = True Then
            hideerrors()
            lblErrorfname.Show()
            lblErrorfname.Text = "Please enter first name"
            txtFname.Focus()
            Exit Sub
        ElseIf fname.Length < 3 Then
            hideerrors()
            lblErrorfname.Show()
            lblErrorfname.Text = "First name must be greater than 3 characters"
            txtFname.Focus()
            Exit Sub
        ElseIf txtFname.Text.Contains(" ") = True Then
            hideerrors()
            lblErrorfname.Show()
            lblErrorfname.Text = "Please enter first name correctly"
            txtFname.Focus()
            Exit Sub
        ElseIf txtFname.Text.Length > 24 Then
            hideerrors()
            lblErrorfname.Show()
            lblErrorfname.Text = "First name must be less than 25 characters"
            txtFname.Focus()
            Exit Sub

        ElseIf mname = "" Or IsNothing(mname) = True Then
            hideerrors()
            lblErrormname.Show()
            lblErrormname.Text = "Please enter middle name"
            txtMname.Focus()
            Exit Sub
        ElseIf mname.Length < 3 Then
            hideerrors()
            lblErrormname.Show()
            lblErrormname.Text = "Middle name must be greater than 3 characters"
            txtMname.Focus()
            Exit Sub
        ElseIf txtMname.Text.Contains(" ") = True Then
            hideerrors()
            lblErrormname.Show()
            lblErrormname.Text = "Please enter middle name correctly"
            txtMname.Focus()
            Exit Sub
        ElseIf txtMname.Text.Length > 24 Then
            hideerrors()
            lblErrormname.Show()
            lblErrormname.Text = "Middle name must be less than 25 characters"
            txtMname.Focus()
            Exit Sub

        ElseIf lname = "" Or IsNothing(lname) = True Then
            hideerrors()
            lblErrorlname.Show()
            lblErrorlname.Text = "Please enter last name"
            txtLname.Focus()
            Exit Sub
        ElseIf lname.Length < 3 Then
            hideerrors()
            lblErrorlname.Show()
            lblErrorlname.Text = "Last name must be greater than 3 characters"
            txtLname.Focus()
            Exit Sub
        ElseIf txtLname.Text.Contains(" ") = True Then
            hideerrors()
            lblErrorlname.Show()
            lblErrorlname.Text = "Please enter last name correctly"
            txtLname.Focus()
            Exit Sub
        ElseIf txtLname.Text.Length > 24 Then
            hideerrors()
            lblErrorlname.Show()
            lblErrorlname.Text = "Last name must be less than 25 characters"
            txtLname.Focus()
            Exit Sub

        ElseIf mstatus = "" Or IsNothing(mname) = True Then
            hideerrors()
            lblErrormstatus.Show()
            lblErrormstatus.Text = "Please select marital status"
            cbMstatus.Focus()
            Exit Sub

        ElseIf job = "" Or IsNothing(job) = True Then
            hideerrors()
            lblErrorjob.Show()
            lblErrorjob.Text = "Please enter job"
            txtJob.Focus()
            Exit Sub
        ElseIf job.Length < 5 Then
            hideerrors()
            lblErrorjob.Show()
            lblErrorjob.Text = "Job must be greater than 5 characters"
            txtJob.Focus()
            Exit Sub
        ElseIf txtJob.Text.Length > 55 Then
            hideerrors()
            lblErrorjob.Show()
            lblErrorjob.Text = "Job must be less than 55 characters"
            txtJob.Focus()
            Exit Sub

        ElseIf address = "" Or IsNothing(address) = True Then
            hideerrors()
            lblErroraddress.Show()
            lblErroraddress.Text = "Please enter address"
            txtAddress.Focus()
            Exit Sub
        ElseIf address.Length < 5 Then
            hideerrors()
            lblErroraddress.Show()
            lblErroraddress.Text = "Address must be greater than 5 characters"
            txtAddress.Focus()
            Exit Sub
        ElseIf txtAddress.Text.Length > 49 Then
            hideerrors()
            lblErroraddress.Show()
            lblErroraddress.Text = "Address must be less than 50 characters"
            txtAddress.Focus()
            Exit Sub

        ElseIf mobile = "" Or IsNothing(mobile) = True Then
            hideerrors()
            lblErrormobile.Show()
            lblErrormobile.Text = "Please enter mobile number"
            txtMobile.Focus()
            Exit Sub
        ElseIf txtMobile.Text.Contains(" ") = True Then
            hideerrors()
            lblErrormobile.Show()
            lblErrormobile.Text = "Please enter mobile number correctly"
            txtMobile.Focus()
            Exit Sub
        ElseIf IsNumeric(txtMobile.Text.Trim) = False Then
            hideerrors()
            lblErrormobile.Show()
            lblErrormobile.Text = "Please enter numbers only"
            txtMobile.Focus()
            Exit Sub
        ElseIf txtMobile.Text.Length > 14 Then
            hideerrors()
            lblErrormobile.Show()
            lblErrormobile.Text = "Mobile number must be less than 15 numbers"
            txtMobile.Focus()
            Exit Sub

        ElseIf houseno = "" Or IsNothing(houseno) = True Then
            hideerrors()
            lblErrorhouseno.Show()
            lblErrorhouseno.Text = "Please enter house number"
            txtHouseno.Focus()
            Exit Sub
        ElseIf houseno.Length > 10 Then
            hideerrors()
            lblErrorhouseno.Show()
            lblErrorhouseno.Text = "House number must me less than 10 characters"
            txtHouseno.Focus()
            Exit Sub
        ElseIf txtHouseno.Text.StartsWith("house ") = False Then
            hideerrors()
            lblErrorhouseno.Show()
            lblErrorhouseno.Text = "Error. Please refer to example above"
            txtHouseno.Focus()
            Exit Sub

        ElseIf roomno = "" Or IsNothing(roomno) = True Then
            hideerrors()
            lblErrorroomno.Show()
            lblErrorroomno.Text = "Please enter room number"
            txtRoomno.Focus()
            Exit Sub
        ElseIf roomno.Length > 5 Then
            hideerrors()
            lblErrorroomno.Show()
            lblErrorroomno.Text = "Room number must be less than 5 digits"
            txtRoomno.Focus()
            Exit Sub
        ElseIf txtRoomno.Text.Contains(" ") = True Then
            hideerrors()
            lblErrorroomno.Show()
            lblErrorroomno.Text = "Please enter room number correctly"
            txtRoomno.Focus()
            Exit Sub
        ElseIf IsNumeric(txtRoomno.Text.Trim) = False Then
            hideerrors()
            lblErrorroomno.Show()
            lblErrorroomno.Text = "Please enter numbers only"
            txtRoomno.Focus()
            Exit Sub

        ElseIf amountpaid = "" Or IsNothing(amountpaid) = True Then
            hideerrors()
            lblErroramountpaid.Show()
            lblErroramountpaid.Text = "Please enter amount paid"
            txtAmountpaid.Focus()
            Exit Sub
        ElseIf txtAmountpaid.Text.Contains(" ") = True Then
            hideerrors()
            lblErroramountpaid.Show()
            lblErroramountpaid.Text = "Please enter amount paid correctly"
            txtAmountpaid.Focus()
            Exit Sub
        ElseIf IsNumeric(txtAmountpaid.Text.Trim) = False Then
            hideerrors()
            lblErroramountpaid.Show()
            lblErroramountpaid.Text = "Please enter numbers only"
            txtAmountpaid.Focus()
            Exit Sub
        ElseIf txtAmountpaid.Text.Length > 19 Then
            hideerrors()
            lblErroramountpaid.Show()
            lblErroramountpaid.Text = "Amount paid must be less than 20 digits"
            txtAmountpaid.Focus()
            Exit Sub

        ElseIf amountremain = "" Or IsNothing(amountremain) = True Then
            hideerrors()
            lblErroramountremain.Show()
            lblErroramountremain.Text = "Please enter amount remain"
            txtAmountremain.Focus()
            Exit Sub
        ElseIf txtAmountremain.Text.Contains(" ") = True Then
            hideerrors()
            lblErroramountremain.Show()
            lblErroramountremain.Text = "Please enter amount remained correctly"
            txtAmountremain.Focus()
            Exit Sub
        ElseIf IsNumeric(txtAmountremain.Text.Trim) = False Then
            hideerrors()
            lblErroramountremain.Show()
            lblErroramountremain.Text = "Please enter numbers only"
            txtAmountremain.Focus()
            Exit Sub
        ElseIf txtAmountremain.Text.Length > 19 Then
            hideerrors()
            lblErroramountremain.Show()
            lblErroramountremain.Text = "Amount remain must be less than 20 digits"
            txtAmountremain.Focus()
            Exit Sub

        ElseIf lblEditreference.Text <> "edit reference" Then
            openconnection()
            Dim selectqry As String = "SELECT id FROM customers WHERE id=" + lblEditreference.Text
            Dim da As OleDbDataAdapter
            da = New OleDbDataAdapter(selectqry, connect)
            Dim dtset As DataSet
            dtset = New DataSet
            Try
                da.Fill(dtset, "customers")
            Catch ex As Exception
                MsgBox("Error occured. Please try again", MessageBoxIcon.Error, "Error")
                Exit Sub
            End Try

            Dim dttable As DataTable
            dttable = New DataTable
            dttable = dtset.Tables("customers")

            Dim dbid As String
            For Each temprow In dttable.Rows
                dbid = temprow("id").ToString

                Dim ask As String = "User exist. Do you want to update his/her informations?"
                Dim mymsgbox As String = MessageBox.Show(ask, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If mymsgbox = MsgBoxResult.No Then
                    Me.Refresh()
                    clearinputs()
                    lblEditreference.Text = "edit reference"
                    closeconnection()
                    btnConfirm.Enabled = True
                    Exit Sub
                Else
                    Dim updateqry As String = "UPDATE customers SET fname='" + fname + "',mname='" + mname + "',lname='" + lname + "',mstatus='" + mstatus + "',job='" + job + "',address='" + address + "',mobile='" + mobile + "',houseno='" + houseno + "',roomno='" + roomno + "',amountpaid='" + amountpaid + "',amountremain='" + amountremain + "',startdate='" + startdate + "',enddate='" + enddate + "' WHERE id=" + lblEditreference.Text
                    Dim updatecmd As New OleDbCommand
                    With updatecmd
                        .CommandText = updateqry
                        .Connection = connect

                        Try
                            .ExecuteNonQuery()
                            clearinputs()
                            MsgBox("Customer details updated successfuly", MsgBoxStyle.Information, "Success")
                            lblEditreference.Text = "edit reference"
                            closeconnection()
                            btnConfirm.Enabled = True
                            Exit Sub
                        Catch ex As Exception
                            MsgBox("Error occured. Please try again", MessageBoxIcon.Error, "Error")
                            hideerrors()
                            Exit Sub
                        End Try
                    End With
                End If
            Next

        ElseIf lblEditreference.Text = "edit reference" Then
            hideerrors()
            openconnection()
            Dim insertqry As String = "INSERT INTO customers(fname,mname,lname,mstatus,job,address,mobile,houseno,roomno,amountpaid,amountremain,startdate,enddate) VALUES('" + fname + "','" + mname + "','" + lname + "','" + mstatus + "','" + job + "','" + address + "','" + mobile + "','" + houseno + "','" + roomno + "','" + amountpaid + "','" + amountremain + "','" + startdate + "','" + enddate + "')"
            Dim insertcmd As New OleDbCommand
            With insertcmd
                .CommandText = insertqry
                .Connection = connect

                Try
                    .ExecuteNonQuery()
                    clearinputs()
                    MsgBox("Customer added successfuly", MsgBoxStyle.Information, "Success")
                    btnConfirm.Enabled = True
                    Exit Sub
                Catch ex As Exception
                    MsgBox("Error occured. Please try again", MessageBoxIcon.Error, "Error")
                    hideerrors()
                    Exit Sub
                End Try
            End With
        End If
    End Sub
#End Region

#Region "Clear Button"
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        clearinputs()
    End Sub
#End Region

#Region "Hide all error labels"
    Private Sub hideerrors()
        lblErrorfname.Hide()
        lblErrormname.Hide()
        lblErrorlname.Hide()
        lblErrormstatus.Hide()
        lblErrorjob.Hide()
        lblErroraddress.Hide()
        lblErrormobile.Hide()
        lblErrorhouseno.Hide()
        lblErrorroomno.Hide()
        lblErroramountpaid.Hide()
        lblErroramountremain.Hide()
        lblErrorstartdate.Hide()
        lblErrorenddate.Hide()
    End Sub
#End Region

#Region "Clear inputs"
    Private Sub clearinputs()
        txtFname.Clear()
        txtMname.Clear()
        txtLname.Clear()
        txtJob.Clear()
        txtAddress.Clear()
        txtMobile.Clear()
        txtHouseno.Clear()
        txtRoomno.Clear()
        txtAmountpaid.Clear()
        txtAmountremain.Clear()
        hideerrors()
    End Sub
#End Region

#Region "Textbox fname changed"
    Private Sub txtFname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFname.TextChanged
        If txtFname.Text.Length > 24 Then
            hideerrors()
            lblErrorfname.Show()
            lblErrorfname.Text = "First name must be less than 25 characters"
            txtFname.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
            Exit Sub
        End If
    End Sub
#End Region

#Region "Textbox mname changed"
    Private Sub txtMname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMname.TextChanged
        If txtMname.Text.Length > 24 Then
            hideerrors()
            lblErrormname.Show()
            lblErrormname.Text = "Middle name must be less than 25 characters"
            txtMname.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
            Exit Sub
        End If
    End Sub
#End Region

#Region "Textbox lname changed"
    Private Sub txtLname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLname.TextChanged
        If txtLname.Text.Length > 24 Then
            hideerrors()
            lblErrorlname.Show()
            lblErrorlname.Text = "Last name must be less than 25 characters"
            txtLname.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
            Exit Sub
        End If
    End Sub
#End Region

#Region "Textbox job changed"
    Private Sub txtJob_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJob.TextChanged
        If txtJob.Text.Length > 55 Then
            hideerrors()
            lblErrorjob.Show()
            lblErrorjob.Text = "Job must be less than 55 characters"
            txtJob.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
        End If
    End Sub
#End Region

#Region "Textbox address changed"
    Private Sub txtAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddress.TextChanged
        If txtAddress.Text.Length > 49 Then
            hideerrors()
            lblErroraddress.Show()
            lblErroraddress.Text = "Address must be less than 50 characters"
            txtAddress.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
        End If
    End Sub
#End Region

#Region "Textbox mobile changed"
    Private Sub txtMobile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMobile.TextChanged
        If IsNumeric(txtMobile.Text.Trim) = False Then
            hideerrors()
            lblErrormobile.Show()
            lblErrormobile.Text = "Please enter numbers only"
            txtMobile.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        ElseIf txtMobile.Text.Length > 14 Then
            hideerrors()
            lblErrormobile.Show()
            lblErrormobile.Text = "Mobile number must be less than 15 numbers"
            txtMobile.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
        End If
    End Sub
#End Region

#Region "Textbox houseno changed"
    Private Sub txtHouseno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHouseno.TextChanged

        If txtHouseno.Text.StartsWith("house ") = False Then
            hideerrors()
            lblErrorhouseno.Show()
            lblErrorhouseno.Text = "Error. Please refer to example above"
            txtHouseno.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        ElseIf txtHouseno.Text.Length > 9 Then
            hideerrors()
            lblErrorhouseno.Show()
            lblErrorhouseno.Text = "House number must be less than 10 characters"
            txtHouseno.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
        End If
    End Sub
#End Region

#Region "Textbox roomno changed"
    Private Sub txtRoomno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRoomno.TextChanged
        If IsNumeric(txtRoomno.Text.Trim) = False Then
            hideerrors()
            lblErrorroomno.Show()
            lblErrorroomno.Text = "Please enter numbers only"
            txtRoomno.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        ElseIf txtRoomno.Text.Length > 4 Then
            hideerrors()
            lblErrorroomno.Show()
            lblErrorroomno.Text = "Room number must be less than 5 digits"
            txtRoomno.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
        End If
    End Sub
#End Region

#Region "Textbox amountpaid changed"
    Private Sub txtAmountpaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmountpaid.TextChanged
        If IsNumeric(txtAmountpaid.Text.Trim) = False Then
            hideerrors()
            lblErroramountpaid.Show()
            lblErroramountpaid.Text = "Please enter numbers only"
            txtAmountpaid.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        ElseIf txtAmountpaid.Text.Length > 19 Then
            hideerrors()
            lblErroramountpaid.Show()
            lblErroramountpaid.Text = "Amount paid must be less than 20 digits"
            txtAmountpaid.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
        End If
    End Sub
#End Region

#Region "Textbox amountremain changed"
    Private Sub txtAmountremain_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmountremain.TextChanged
        If IsNumeric(txtAmountremain.Text.Trim) = False Then
            hideerrors()
            lblErroramountremain.Show()
            lblErroramountremain.Text = "Please enter numbers only"
            txtAmountremain.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        ElseIf txtAmountremain.Text.Length > 19 Then
            hideerrors()
            lblErroramountremain.Show()
            lblErroramountremain.Text = "Amount remain must be less than 20 digits"
            txtAmountremain.Focus()
            btnConfirm.Enabled = False
            Exit Sub
        Else
            hideerrors()
            btnConfirm.Enabled = True
        End If
    End Sub
#End Region
#End Region

#Region "Report"

#Region "Menu button"
    Private Sub btnMenuReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMenuReport.Click
        enablemenubtn()
        btnMenuReport.Enabled = False
        hidegb()
        gbReport.Show()
        gbReport.Dock = DockStyle.Fill
        hidereporterrors()
    End Sub
#End Region

#Region "Report by house number"
    Private Sub btnReporthouse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReporthouse.Click
        If txtReporthouse.Text = "" Or IsNothing(txtReporthouse.Text) = True Then
            hidereporterrors()
            lblErrorreporthouse.Show()
            lblErrorreporthouse.Text = "Please enter house number"
            txtReporthouse.Focus()
            Exit Sub
        ElseIf txtReporthouse.Text.StartsWith("house ") = False Then
            hidereporterrors()
            lblErrorreporthouse.Show()
            lblErrorreporthouse.Text = "Error. Please refer to example above"
            txtReporthouse.Focus()
            Exit Sub
        Else
            With frmReportbyhouse
                .myfilter = txtReporthouse.Text
            End With
            hidereporterrors()
            Me.Hide()
            txtReporthouse.Clear()
            frmReportbyhouse.ShowDialog()
        End If
    End Sub
#End Region

#Region "Report by marital"
    Private Sub btnReportMstatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReportMstatus.Click
        If cbReportMstatus.Text = "" Or IsNothing(cbReportMstatus.Text) = True Then
            hidereporterrors()
            lblErrorreportmstatus.Show()
            lblErrorreportmstatus.Text = "Please select marital"
            cbReportMstatus.Focus()
            Exit Sub
        Else
            With frmReportbymarital
                .myfilter = cbReportMstatus.Text
            End With
            hidereporterrors()
            Me.Hide()
            frmReportbymarital.ShowDialog()
        End If
    End Sub
#End Region

#Region "Report by standard"
    Private Sub btnReportstandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReportstandard.Click
        Me.Hide()
        hideerrors()
        frmReportbystandard.Show()
    End Sub
#End Region

#Region "Hide report errors"
    Private Sub hidereporterrors()
        lblErrorreporthouse.Hide()
        lblErrorreportmstatus.Hide()
    End Sub
#End Region

#End Region

#Region "Change Password"

#Region "Menu Button"
    Private Sub btnMenuPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMenuPassword.Click
        enablemenubtn()
        btnMenuPassword.Enabled = False
        hidegb()
        gbPassword.Show()
        gbPassword.Dock = DockStyle.Fill
        hideerrors()
        clearpassinputs()
    End Sub
#End Region

#Region "Confirm Button"
    Private Sub btnConfirmp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirmp.Click
        Dim oldp As String = txtOldp.Text.Trim
        Dim newp As String = txtNewp.Text.Trim
        Dim confirmp As String = txtConfirmp.Text.Trim

        If oldp = "" Or IsNothing(oldp) = True Then
            hidepasserrors()
            lblErrorOldp.Show()
            lblErrorOldp.Text = "Please enter your old password"
            txtOldp.Focus()
            Exit Sub
        ElseIf oldp.Length < 6 Then
            hidepasserrors()
            lblErrorOldp.Show()
            lblErrorOldp.Text = "Password cannot have less than 6 characters"
            txtOldp.Focus()
            Exit Sub

        ElseIf newp = "" Or IsNothing(newp) = True Then
            hidepasserrors()
            lblErrorNewp.Show()
            lblErrorNewp.Text = "Please enter your new password"
            txtNewp.Focus()
            Exit Sub
        ElseIf newp.Length < 6 Then
            hidepasserrors()
            lblErrorNewp.Show()
            lblErrorNewp.Text = "Password cannot have less than 6 characters"
            txtNewp.Focus()
            Exit Sub

        ElseIf confirmp = "" Or IsNothing(confirmp) = True Then
            hidepasserrors()
            lblErrorConfirmp.Show()
            lblErrorConfirmp.Text = "Please confirm your new password"
            txtConfirmp.Focus()
            Exit Sub
        ElseIf confirmp.Length < 6 Then
            hidepasserrors()
            lblErrorConfirmp.Show()
            lblErrorConfirmp.Text = "Password cannot have less than 6 characters"
            txtConfirmp.Focus()
            Exit Sub

        Else
            openconnection()
            Dim Ueold As New UnicodeEncoding()
            Dim ByteSourceTextold() As Byte = Ueold.GetBytes(oldp)
            Dim Md5old As New MD5CryptoServiceProvider()
            Dim ByteHashold() As Byte = Md5old.ComputeHash(ByteSourceTextold)
            Convert.ToBase64String(ByteHashold)
            Dim hasholdPwd As String
            hasholdPwd = Convert.ToBase64String(ByteHashold)

            Dim Uenew As New UnicodeEncoding()
            Dim ByteSourceTextnew() As Byte = Uenew.GetBytes(newp)
            Dim Md5new As New MD5CryptoServiceProvider()
            Dim ByteHashnew() As Byte = Md5new.ComputeHash(ByteSourceTextnew)
            Convert.ToBase64String(ByteHashnew)
            Dim hashnewPwd As String
            hashnewPwd = Convert.ToBase64String(ByteHashnew)

            Dim updateqry As String = "UPDATE users SET passwd='" + hashnewPwd + "' WHERE uname='cngirwa'"
            Dim cmd As OleDbCommand
            cmd = New OleDbCommand(updateqry, connect)

            Dim selectqry As String = "SELECT uname,passwd FROM users WHERE uname='cngirwa'"
            Dim da As OleDbDataAdapter
            da = New OleDbDataAdapter(selectqry, connect)
            Dim dtset As DataSet
            dtset = New DataSet()
            Try
                da.Fill(dtset, "users")
            Catch ex As Exception
                MsgBox("Server is busy. Please try again", MessageBoxIcon.Error, "Error")
                Exit Sub
            End Try

            Dim dttable As DataTable
            dttable = New DataTable()
            dttable = dtset.Tables("users")

            Dim dboldp As String
            For Each tempRow In dttable.Rows
                dboldp = tempRow("passwd").ToString

                If (oldp.Length > 5) And (newp.Length > 5) And (confirmp.Length > 5) Then
                    If (dboldp <> hasholdPwd) Then
                        MsgBox("Old password did not match", MessageBoxIcon.Warning, "Error")
                        Exit Sub

                    ElseIf (dboldp = hashnewPwd) Then
                        MsgBox("New password is the same as old password. Please enter new password", MessageBoxIcon.Warning, "Error")
                        Exit Sub

                    ElseIf (dboldp = hasholdPwd) Then
                        If (newp = confirmp) Then
                            Try
                                cmd.ExecuteNonQuery()
                                MsgBox("Password changed successful", MsgBoxStyle.Information, "Success")
                                clearpassinputs()
                                closeconnection()
                            Catch ex As Exception
                                MsgBox("Error occured. Please try again", MessageBoxIcon.Error, "Error")
                                Exit Sub
                            End Try
                        Else
                            MsgBox("New password did not match", MessageBoxIcon.Warning, "Error")
                            Exit Sub
                        End If
                    End If
                Else
                    MsgBox("Password cannot have less than 6 characters", MessageBoxIcon.Warning, "Error")
                    Exit Sub
                End If
            Next
            closeconnection()
        End If
    End Sub
#End Region

#Region "Clear Button"
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        clearpassinputs()
    End Sub
#End Region

#Region "Clear inputs"
    Private Sub clearpassinputs()
        txtOldp.Clear()
        txtNewp.Clear()
        txtConfirmp.Clear()
    End Sub
#End Region

#Region "Hide pass errors"
    Private Sub hidepasserrors()
        lblErrorOldp.Hide()
        lblErrorNewp.Hide()
        lblErrorConfirmp.Hide()
    End Sub
#End Region

#End Region

#Region "About"
    Private Sub btnAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbout.Click
        Dim apptitle As String = My.Application.Info.Title.ToString
        Dim compname As String = My.Application.Info.CompanyName.ToString
        Dim appversion As String = My.Application.Info.Version.ToString
        Dim appdescrption As String = My.Application.Info.Description.ToString
        Dim appcopyright As String = My.Application.Info.Copyright.ToString

        MsgBox("Name : " & apptitle & ControlChars.NewLine & _
        "Company Name : " & compname & ControlChars.NewLine & _
        "Version : v" & appversion & ControlChars.NewLine & _
        "Description : " & appdescrption & ControlChars.NewLine & _
        appcopyright & ControlChars.NewLine & _
        "Credits : Amani Allen Moova (System Analyst) ")
        enablemenubtn()
    End Sub
#End Region

#Region "Logout"
    Private Sub btnMenuLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMenuLogout.Click
        Dim close As String = MessageBox.Show("Are you sure you want to logout?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If close = MsgBoxResult.No Then
            Me.Refresh()
            enablemenubtn()
            hidegb()
            closeconnection()
        Else
            closeconnection()
            frmLogin.Show()
            Me.Dispose()
            Me.Close()
        End If
    End Sub
#End Region

#End Region

#Region "Hide all group boxes"
    Private Sub hidegb()
        gbCustomers.Hide()
        gbAddCustomer.Hide()
        gbPassword.Hide()
        gbReport.Hide()
    End Sub
#End Region

#Region "Enable menu buttons"
    Private Sub enablemenubtn()
        btnMenuCustomers.Enabled = True
        btnMenuAddCustomers.Enabled = True
        btnMenuReport.Enabled = True
        btnMenuPassword.Enabled = True
        btnMenuLogout.Enabled = True
    End Sub
#End Region

End Class
