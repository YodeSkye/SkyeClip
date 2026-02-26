
Imports System.Text

Public Class ActiveAppRuleProfilesEditor

    ' Declarations
    Private _rule As App.ActiveAppRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)
    Private Class ModeItem
        Public Property Value As App.ActiveAppRule.ActivationMode
        Public Property Text As String
        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class
    Private Class ProfileItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' Form Events
    Friend Sub New()
        InitializeComponent()

        CoBoxMode.Items.Clear()
        For Each v As App.ActiveAppRule.ActivationMode In [Enum].GetValues(Of ActiveAppRule.ActivationMode)()
            CoBoxMode.Items.Add(New ModeItem With {
                .Value = v,
                .Text = App.GetEnumDescription(v)
            })
        Next

        CoBoxEnterProfile.Items.Clear()
        CoBoxExitProfile.Items.Clear()
        For Each p In App.Settings.Profiles
            CoBoxEnterProfile.Items.Add(New ProfileItem With {.ID = p.ID, .Name = p.Name})
            CoBoxExitProfile.Items.Add(New ProfileItem With {.ID = p.ID, .Name = p.Name})
        Next
        CoBoxEnterProfile.DisplayMember = "Name"
        CoBoxEnterProfile.ValueMember = "ID"
        CoBoxExitProfile.DisplayMember = "Name"
        CoBoxExitProfile.ValueMember = "ID"

    End Sub
    Private Sub ActiveAppRuleProfilesEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyToTooltip(Tip)
    End Sub

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Tip.HideTooltip()
        TipError.HideTooltip()
        If CoBoxMode.SelectedItem Is Nothing Then
            TipError.ShowTooltipAt(CoBoxMode.Location, "Please select a mode.", My.Resources.ImageRules16)
            Exit Sub
        End If
        If TxtBoxTargetProcess.Text.Trim() = String.Empty Then
            TipError.ShowTooltipAt(TxtBoxTargetProcess.PointToScreen(TxtBoxTargetProcess.Location), "Please enter a process name.", My.Resources.ImageRules16)
            Exit Sub
        End If

        'OrElse CoBoxEnterProfile.SelectedItem Is Nothing OrElse CoBoxExitProfile.SelectedItem Is Nothing Then Return False


        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)
    End Sub

    ' Methods
    Friend Sub LoadRule(rule As App.ActiveAppRule)
        _rule = rule

        ' Mode
        For Each item As ModeItem In CoBoxMode.Items
            If item.Value = _rule.Mode Then
                CoBoxMode.SelectedItem = item
                Exit For
            End If
        Next

        ' Target process
        TxtBoxTargetProcess.Text = _rule.TargetProcess

        ' Profiles
        For Each item As ProfileItem In CoBoxEnterProfile.Items
            If item.ID = _rule.EnterProfileID Then
                CoBoxEnterProfile.SelectedItem = item
                Exit For
            End If
        Next
        For Each item As ProfileItem In CoBoxExitProfile.Items
            If item.ID = _rule.ExitProfileID Then
                CoBoxExitProfile.SelectedItem = item
                Exit For
            End If
        Next

    End Sub
    Friend Sub SaveRule()

        ' Mode
        Dim modeItem As ModeItem = CType(CoBoxMode.SelectedItem, ModeItem)
        _rule.Mode = modeItem.Value

        ' Process
        _rule.TargetProcess = TxtBoxTargetProcess.Text

        ' Profiles
        Dim enterItem As ProfileItem = CType(CoBoxEnterProfile.SelectedItem, ProfileItem)
        _rule.EnterProfileID = enterItem.ID
        Dim exitItem As ProfileItem = CType(CoBoxExitProfile.SelectedItem, ProfileItem)
        _rule.ExitProfileID = exitItem.ID

    End Sub

End Class
