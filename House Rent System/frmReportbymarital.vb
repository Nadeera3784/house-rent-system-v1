Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data

Public Class frmReportbymarital
    Public myfilter As String
    Private mReport As ReportDocument

#Region "Report load"
    Private Sub crvMarital_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crvMarital.Load
        mReport = New ReportDocument()

        Dim tbCurrent As CrystalDecisions.CrystalReports.Engine.Table
        Dim tliCurrent As CrystalDecisions.Shared.TableLogOnInfo

        mReport.Load(Application.StartupPath & "\crReportbymarital.rpt")

        mReport.RecordSelectionFormula = "{customers.mstatus} = '" & myfilter & "'"

        For Each tbCurrent In mReport.Database.Tables
            tliCurrent = tbCurrent.LogOnInfo
            With tliCurrent.ConnectionInfo
                .ServerName = Application.StartupPath & "\hrs.mdb"
                .UserID = ""
                .Password = ""
                .DatabaseName = Application.StartupPath & "\hrs.mdb"
            End With
            tbCurrent.ApplyLogOnInfo(tliCurrent)
        Next tbCurrent

        crvMarital.ReportSource = mReport
    End Sub
#End Region

#Region "Form is closed"
    Private Sub frmReportbymarital_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmMain.Show()
        Me.Dispose()
        Me.Close()
    End Sub
#End Region

End Class