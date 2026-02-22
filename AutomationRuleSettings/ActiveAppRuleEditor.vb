
Imports System.Text

Public Class ActiveAppRuleEditor

    ' Declarations
    Private _rule As App.ActiveAppRule
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

        CoBoxMode.Items.Clear()
        CoBoxMode.Items.Add(App.ActiveAppRule.ActivationMode.ForegroundWindow)
        CoBoxMode.Items.Add(App.ActiveAppRule.ActivationMode.RunningProcess)

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
    Private Sub KeywordRuleEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)
    End Sub

    ' Methods
    Friend Sub LoadRule(rule As App.ActiveAppRule)
        _rule = rule

        ' Mode
        CoBoxMode.SelectedItem = _rule.Mode

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

        ' Descriptions
        TxtBoxEnterDescription.Text = _rule.EnterDescription
        TxtBoxExitDescription.Text = _rule.ExitDescription

    End Sub
    Friend Sub SaveRule()

        ' Mode + process
        _rule.Mode = CType(CoBoxMode.SelectedItem, App.ActiveAppRule.ActivationMode)
        _rule.TargetProcess = TxtBoxTargetProcess.Text

        ' Descriptions
        _rule.EnterDescription = TxtBoxEnterDescription.Text
        _rule.ExitDescription = TxtBoxExitDescription.Text

        ' Profiles
        Dim enterItem As ProfileItem = CType(CoBoxEnterProfile.SelectedItem, ProfileItem)
        _rule.EnterProfileID = enterItem.ID
        Dim exitItem As ProfileItem = CType(CoBoxExitProfile.SelectedItem, ProfileItem)
        _rule.ExitProfileID = exitItem.ID

        ' Actions
        _rule.OnEnter = Sub(ctx)
                            ctx.Profile.CurrentProfileID = enterItem.ID
                            If App.Settings.UseProfiles Then App.Settings.CurrentProfileID = enterItem.ID
                        End Sub

        _rule.OnExit = Sub(ctx)
                           ctx.Profile.CurrentProfileID = exitItem.ID
                           If App.Settings.UseProfiles Then App.Settings.CurrentProfileID = exitItem.ID
                       End Sub

    End Sub

End Class
