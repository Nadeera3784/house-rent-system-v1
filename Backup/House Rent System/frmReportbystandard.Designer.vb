﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportbystandard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportbystandard))
        Me.crvStandard = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crvStandard
        '
        Me.crvStandard.ActiveViewIndex = -1
        Me.crvStandard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvStandard.DisplayGroupTree = False
        Me.crvStandard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvStandard.Location = New System.Drawing.Point(0, 0)
        Me.crvStandard.Name = "crvStandard"
        Me.crvStandard.Size = New System.Drawing.Size(1211, 672)
        Me.crvStandard.TabIndex = 0
        '
        'frmReportbystandard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1211, 672)
        Me.Controls.Add(Me.crvStandard)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReportbystandard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvStandard As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class