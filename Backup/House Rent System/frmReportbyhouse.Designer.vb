<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportbyhouse
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportbyhouse))
        Me.crvReportbyhouse = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crvReportbyhouse
        '
        Me.crvReportbyhouse.ActiveViewIndex = -1
        Me.crvReportbyhouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvReportbyhouse.DisplayGroupTree = False
        Me.crvReportbyhouse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvReportbyhouse.Location = New System.Drawing.Point(0, 0)
        Me.crvReportbyhouse.Name = "crvReportbyhouse"
        Me.crvReportbyhouse.Size = New System.Drawing.Size(1217, 678)
        Me.crvReportbyhouse.TabIndex = 0
        '
        'frmReportbyhouse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1217, 678)
        Me.Controls.Add(Me.crvReportbyhouse)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReportbyhouse"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvReportbyhouse As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
