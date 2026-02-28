
Imports System.Drawing.Imaging
Imports System.Text

Public Class TimeRuleEditor

    ' Declarations
    Private _rule As App.TimeRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)
    Private Class ProfileItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' Form Events
    Public Sub New()
        InitializeComponent()

        CoBoxProfile.Items.Clear()
        For Each p In App.Settings.Profiles
            CoBoxProfile.Items.Add(New ProfileItem With {.ID = p.ID, .Name = p.Name})
        Next
        CoBoxProfile.DisplayMember = "Name"
        CoBoxProfile.ValueMember = "ID"

    End Sub
    Private Sub TimeRuleEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyToTooltip(Tip)
        Skye.UI.ThemeManager.ApplyToTooltip(TipError)
    End Sub

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Tip.HideTooltip()
        TipError.HideTooltip()

        If DTPEndTime.Value <= DTPStartTime.Value Then
            Dim pt = DTPEndTime.PointToScreen(New Point(0, DTPEndTime.Height))
            TipError.ShowTooltipAt(pt, "End time must be greater than start time.", My.Resources.ImageRules16)
            Exit Sub
        End If
        If CoBoxProfile.SelectedItem Is Nothing Then
            Dim pt = CoBoxProfile.PointToScreen(New Point(0, CoBoxProfile.Height))
            TipError.ShowTooltipAt(pt, "Please select a profile.", My.Resources.ImageRules16)
            Exit Sub
        End If

        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)

    End Sub

    ' Methods
    Friend Sub LoadRule(rule As App.TimeRule)
        _rule = rule

        ' Load Start/End times
        DTPStartTime.Value = Date.Today + _rule.StartTime
        DTPEndTime.Value = Date.Today + _rule.EndTime

        ' Load profile
        For Each item As ProfileItem In CoBoxProfile.Items
            If item.ID = _rule.TargetProfileID Then
                CoBoxProfile.SelectedItem = item
                Exit For
            End If
        Next

    End Sub
    Friend Sub SaveRule()

        ' Save times
        _rule.StartTime = DTPStartTime.Value.TimeOfDay
        _rule.EndTime = DTPEndTime.Value.TimeOfDay

        ' Save profile
        Dim item As ProfileItem = CType(CoBoxProfile.SelectedItem, ProfileItem)
        _rule.TargetProfileID = item.ID

    End Sub

End Class
