Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Public Class frmregreport
    Public myfilter As String
    Public strReport As String
    Private mReport As ReportDocument

    Private Sub frmregreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pvCollection As New CrystalDecisions.Shared.ParameterValues
        Dim pdvBusName As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim pdvBusAddress As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim pdvContactInfo As New CrystalDecisions.Shared.ParameterDiscreteValue

        Dim BusName As String = "ABC Company"
        Dim BusAddress As String = "Bacolod City"
        Dim ContactInfo As String = "1234567"

        mReport = New ReportDocument()

        pdvBusName.Value = BusName
        pdvBusAddress.Value = BusAddress
        pdvContactInfo.Value = ContactInfo

        Dim tbCurrent As CrystalDecisions.CrystalReports.Engine.Table
        Dim tliCurrent As CrystalDecisions.Shared.TableLogOnInfo



        mReport.Load(Application.StartupPath & "\crReportbyhouse.rpt")

        mReport.RecordSelectionFormula = "{customers.houseno} = '" & myfilter & "'"

        'pvCollection.Add(pdvBusName)
        'mReport.DataDefinition.ParameterFields("prmBusName").ApplyCurrentValues(pvCollection)

        'pvCollection.Add(pdvBusAddress)
        'mReport.DataDefinition.ParameterFields("prmBusAddress").ApplyCurrentValues(pvCollection)

        'pvCollection.Clear()
        'pvCollection.Add(pdvContactInfo)

        'mReport.DataDefinition.ParameterFields("prmContactInfo").ApplyCurrentValues(pvCollection)
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

        CrystalReportViewer1.ReportSource = mReport
    End Sub
End Class