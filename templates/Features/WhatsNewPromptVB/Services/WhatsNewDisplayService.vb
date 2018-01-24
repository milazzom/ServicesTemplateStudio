﻿Imports Param_ItemNamespace.Views
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace Services
    ' For instructions on testing this service see https://github.com/Microsoft/WindowsTemplateStudio/tree/master/docs/features/whats-new-prompt.md
    Shared shown As Boolean = false
    Public Module WhatsNewDisplayService
        Friend Async Function ShowIfAppropriateAsync() As Task
            If SystemInformation.IsAppUpdated And Not shown Then
                shown = true
                Dim dialog = New WhatsNewDialog()
                Await dialog.ShowAsync()
            End If
        End Function
    End Module
End Namespace
