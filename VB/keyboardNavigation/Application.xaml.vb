﻿' Developer Express Code Central Example:
' DXGrid Keyboard Navigation integration with AutoCompleteBox
' 
' Keyboard navigation support with custom editors in the DXGrid may be
' accomplished by setting a custom attached behavior -- available from the
' System.Windows.Interactivity library -- on the TableView to assign the a handler
' to the GridViewBase.EditorShowing
' (http://documentation.devexpress.com/#WPF/DevExpressXpfGridGridViewBase_ShowingEditortopic)
' event. There, you may handle the focus logic for the custom control.
' In the
' following example, we've handled the focus logic for the standard Slider control
' and the custom AutoCompleteBox from the WPF ToolKit -- WPF Toolkit - February
' 2010 Release (http://wpf.codeplex.com/releases/view/40535).
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E20032

Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Windows

Namespace attachedBehaviorPolicy
    ''' <summary>
    ''' Interaction logic for App.xaml
    ''' </summary>
    Partial Public Class App
        Inherits Application

    End Class
End Namespace
