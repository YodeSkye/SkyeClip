
Imports System.Text

Public Class FormatRuleEditor

    ' Declarations
    Private _rule As App.FormatRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)
    Private Shared ReadOnly formats As String() = New String() {
        "UnicodeText",
        "Text",
        "OEMText",
        "Rich Text Format",
        "HTML Format",
        "CSV",
        "Bitmap",
        "DIB",
        "DIBV5",
        "PNG",
        "JFIF",
        "GIF",
        "TIFF",
        "FileDrop",
        "FileGroupDescriptor",
        "FileGroupDescriptorW",
        "FileContents",
        "Shell IDList Array",
        "Preferred DropEffect",
        "UniformResourceLocator",
        "UniformResourceLocatorW"
    }

    ' Form Events
    Public Sub New()
        InitializeComponent()

        ' Populate Format Names
        CoBoxFormatName.Items.Clear()
        CoBoxFormatName.Items.AddRange(formats)

        ' Populate Actions from enum
        CoBoxAction.Items.Clear()
        Dim actions = [Enum].GetValues(Of ContentAction)().Cast(Of Object).ToArray()
        CoBoxAction.Items.AddRange(actions)

    End Sub
    Private Sub FormatRuleEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyToTooltip(Tip)
        Skye.UI.ThemeManager.ApplyToTooltip(TipError)
    End Sub

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Tip.HideTooltip()
        TipError.HideTooltip()
        If CoBoxFormatName.SelectedItem Is Nothing Then
            Dim pt = CoBoxFormatName.PointToScreen(New Point(0, CoBoxFormatName.Height))
            TipError.ShowTooltipAt(pt, "Please select a format name.", My.Resources.ImageRules16)
            Exit Sub
        End If
        If CoBoxAction.SelectedItem Is Nothing Then
            Dim pt = CoBoxAction.PointToScreen(New Point(0, CoBoxAction.Height))
            TipError.ShowTooltipAt(pt, "Please select an action.", My.Resources.ImageRules16)
            Exit Sub
        End If

        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)

    End Sub

    ' Methods
    Friend Sub LoadRule(rule As App.FormatRule)
        _rule = rule

        ' Select format name
        If Not String.IsNullOrEmpty(_rule.FormatName) Then
            CoBoxFormatName.SelectedItem = _rule.FormatName
        End If

        ' Select action
        CoBoxAction.SelectedItem = _rule.Action

    End Sub
    Friend Sub SaveRule()

        ' Format name
        _rule.FormatName = CStr(CoBoxFormatName.SelectedItem)

        ' Action
        _rule.Action = CType(CoBoxAction.SelectedItem, ContentAction)

    End Sub

End Class
