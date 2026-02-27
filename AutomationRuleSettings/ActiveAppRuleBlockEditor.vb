
Imports System.Drawing.Imaging
Imports System.Text
Imports SkyeClip.ClipRepository

Public Class ActiveAppRuleBlockEditor

    ' Declarations
    Private _rule As App.ActiveAppRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)

    ' Form Events
    Friend Sub New()
        InitializeComponent()

        CoBoxTargetProcess.Items.Clear()
        For Each a In App.Tray.repo.GetKnownSourceApps()
            CoBoxTargetProcess.Items.Add(a)
        Next

    End Sub
    Private Sub ActiveAppRuleBlockEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyToTooltip(Tip)
        Skye.UI.ThemeManager.ApplyToTooltip(TipError)
    End Sub

    ' Control Events
    Private Sub CoBoxTargetProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxTargetProcess.SelectedIndexChanged
        If TypeOf CoBoxTargetProcess.SelectedItem Is SourceAppInfo Then
            Dim item = CType(CoBoxTargetProcess.SelectedItem, SourceAppInfo)
            TxtBoxTargetProcess.Text = item.ProcessName
        End If
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Tip.HideTooltip()
        TipError.HideTooltip()
        If TxtBoxTargetProcess.Text.Trim() = String.Empty Then
            Dim pt = CoBoxTargetProcess.PointToScreen(New Point(0, CoBoxTargetProcess.Height))
            TipError.ShowTooltipAt(pt, "Please enter or select a process name.", My.Resources.ImageRules16)
            Exit Sub
        End If

        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)

    End Sub

    ' Methods
    Friend Sub LoadRule(rule As App.ActiveAppRule)
        _rule = rule

        TxtBoxTargetProcess.Text = _rule.TargetProcess

    End Sub
    Friend Sub SaveRule()

        _rule.TargetProcess = TxtBoxTargetProcess.Text

    End Sub

End Class
