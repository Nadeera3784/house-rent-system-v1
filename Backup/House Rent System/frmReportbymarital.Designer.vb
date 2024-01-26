<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportbymarital
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportbymarital))
        Me.crvMarital = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crvMarital
        '
        Me.crvMarital.ActiveViewIndex = -1
        Me.crvMarital.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvMarital.DisplayGroupTree = False
        Me.crvMarital.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvMarital.Location = New System.Drawing.Point(0, 0)
        Me.crvMarital.Name = "crvMarital"
        Me.crvMarital.Size = New System.Drawing.Size(1211, 672)
        Me.crvMarital.TabIndex = 0
        '
        'frmReportbymarital
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1211, 672)
        Me.Controls.Add(Me.crvMarital)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReportbymarital"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvMarital As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
